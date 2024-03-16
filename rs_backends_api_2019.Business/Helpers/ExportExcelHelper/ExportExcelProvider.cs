using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using rs_backends_api_2019.Business.Extensions;
using rs_backends_api_2019.Business.Global;
using rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Core;
using rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Interfaces;
using rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Models;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper;
using rs_backends_api_2019.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ExportExcelHelper
{
    public class ExportExcelProvider : RawFile, IExportExcelProvider
    {

        private readonly IUnitOfWork _unitOfWork;

        private IConfiguration _configuration { get; set; }

        private JObject _parameter { get; set; }

        public ExportExcelProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ExportExcelProvider(IUnitOfWork unitOfWork, object parameter)
        {
            _unitOfWork = unitOfWork;
            this._parameter = JObject.FromObject(parameter);
        }

        private ExcelPackage _excelPackage { get; set; }

        private JObject styles { get; set; }

        private int sheetIndex { get; set; } = 0;

        private string _title { get; set; }

        private string _company { get; set; }

        private string _author { get; set; }

        private DateTime? _created { get; set; } = null;


        #region Properties Excel
        public void title(string title)
        {
            this._title = title.Trim();
            this._excelPackage.Workbook.Properties.Title = this._title;
        }

        public void company(string company)
        {
            this._company = company.Trim();
            this._excelPackage.Workbook.Properties.Company = this._company;
        }

        public void author(string author)
        {
            this._author = author.Trim();
            this._excelPackage.Workbook.Properties.Author = this._author;
        }

        public void created(DateTime created)
        {
            this._created = created;
            this._excelPackage.Workbook.Properties.Created = this._created.Value;
        }

        #endregion Properties Excel

        #region Export Excel

        public ExportExcelResult export(string filename, Action action)
        {
            ExportExcelResult result = new ExportExcelResult();

            this._excelPackage = new ExcelPackage();

            try
            {
                this.autoload();

                action();

                result.success = true;
                result.file.fileName = filename;
                result.file.rawBytes = this._excelPackage.GetAsByteArray();
                result.exception = null;
            }
            catch (Exception e)
            {
                result.success = false;
                result.exception = e;
                result.file = null;
            }
            finally
            {
                this.clearObject();
            }


            return result;
        }

        #endregion Export Excel

        #region Manage Excel

        public void makeWorkSheet(string sheetName, IExportExcel cls)
        {
            ExcelWorksheet sheet = this._excelPackage.Workbook.Worksheets.Add(sheetName.Trim());
            cls.setUnitOfWork(_unitOfWork); //<==Set UnitOfWork
            cls.setParameter(this._parameter); //<==Set Parameter
            cls.setStyles(this.styles); //<==Set default styles

            // Created
            cls.make(sheet);
            cls.render();
            sheet = cls.getWorkSheet();
        }

        public void makeWorkSheets(Func<dynamic, object> sheetName, IExportExcel cls)
        {
            IExportExcel getCls = cls as ExportExcelSupport;
            getCls.setUnitOfWork(_unitOfWork);
            getCls.setParameter(this._parameter); //<==Set Parameter
            getCls.setStyles(this.styles); //<==Set default styles
            getCls.query();
            var resources = getCls.getResources() as IEnumerable<dynamic>;

            if (resources != null && resources.Count() >= 1)
            {
                foreach (var item in resources)
                {
                    Type type = cls.GetType();
                    IExportExcel instance = (IExportExcel)Activator.CreateInstance(type);
                    instance.setSheetIndex(this.sheetIndex); // instance indexOf
                    getCls.setSheetIndex(this.sheetIndex); // instance sheet run or total sheet

                    var _sheetName = (sheetName(item)).ToString();
                    this.makeWorkSheet(_sheetName, instance, item);

                    this.sheetIndex++;
                }
            }
            else
            {
                //=>Default , no data
                Type type = cls.GetType();
                IExportExcel instance = (IExportExcel)Activator.CreateInstance(type);
                instance.setSheetIndex(this.sheetIndex); // instance indexOf
                getCls.setSheetIndex(this.sheetIndex); // instance sheet run or total sheet

                var _sheetName = "Sheet1";
                this.makeWorkSheet(_sheetName, instance, "null");

                this.sheetIndex++;
            }

        }

        private void makeWorkSheet<T>(string sheetName, IExportExcel cls, T data) where T : class
        {
            ExcelWorksheet sheet = this._excelPackage.Workbook.Worksheets.Add(sheetName.Trim());

            cls.setUnitOfWork(_unitOfWork);
            cls.setParameter(this._parameter);
            cls.setStyles(this.styles); //<==Set default styles

            // Created
            cls.make(sheet, data);
            cls.render();
            sheet = cls.getWorkSheet();
        }
        #endregion Manage Excel

        #region Support
        public void autoload()
        {
            this._configuration = GlobalInjections.getConfiguration();

            //=>Set properties
            string title = this._configuration["Exports:Props:Title"];
            string company = this._configuration["Exports:Props:Company"];
            string author = this._configuration["Exports:Props:Author"];

            if (!string.IsNullOrEmpty(title))
            {
                this.title(title);
            }

            if (!string.IsNullOrEmpty(company))
            {
                this.company(company);
            }

            if (!string.IsNullOrEmpty(author))
            {
                this.author(author);
            }

            this.created(DateTime.Now);

            //=>Set styles
            this.styles = new JObject();
            dynamic _styles = this.styles;

            string fontFamily = this._configuration["Exports:Styles:Font"];
            string fontSize = !string.IsNullOrEmpty(this._configuration["Exports:Styles:FontSize"]) ? this._configuration["Exports:Styles:FontSize"] : "16";

            if (!string.IsNullOrEmpty(fontFamily))
            {
                _styles.Font = fontFamily;
            }

            if (!string.IsNullOrEmpty(fontSize))
            {
                _styles.FontSize = fontSize;
            }
        }

        public string fileNameGenerate(string extension = ".xlsx", string style = "")
        {
            style = style.ToLower();

            if (style == "")
            {
                return this.fileNameStandard(string.Empty);
            }
            else if (style == "a")
            {
                return string.Format("Report_{0}_{1}.{2}",
                    DateTime.Now.ToString("yyyyMMddHHmmssfff"), StringExtension.strRandom(7), extension);
            }
            else if (style == "b")
            {
                return string.Format("Report_{0}_{1}.{2}",
                    DateTime.Now.ToString("yyyyMMdd"), StringExtension.strRandom(7), extension);
            }
            else if (style == "c")
            {
                string hash = HashExtension.HashSha1(string.Format("Report_{0}_{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), StringExtension.strRandom(7)));

                return string.Format("{0}.{1}",
                    hash, extension);
            }
            else if (style == "d")
            {
                string hash = HashExtension.HashSha1(string.Format("Report_{0}_{1}", DateTime.Now.ToString("yyyyMMdd"), StringExtension.strRandom(7)));
                return string.Format("{0}.{1}",
                    hash, extension);
            }
            else
            {
                return this.fileNameStandard(string.Empty);
            }
        }

        public string fileNameStandard(string fileName)
        {
            string str = string.Empty;
            string extension = ".xlsx";

            if (string.IsNullOrEmpty(fileName))
            {
                str = "Report_{0}_{1}.{2}";
                extension = ".xlsx";

                return string.Format(str,
                   DateTime.Now.ToString("yyyyMMdd"), StringExtension.strRandom(7), extension);
            }
            else
            {
                str = "Report_{0}_{1}_{2}.{3}";
                extension = System.IO.Path.GetExtension(fileName);

                if (extension == string.Empty)
                {
                    extension = ".xlsx";
                }

                fileName = fileName.Replace(extension, string.Empty);

                return string.Format(str,
                  fileName, DateTime.Now.ToString("yyyyMMdd"), StringExtension.strRandom(7), extension);
            }
        }

        public void clearObject()
        {
            if (this._excelPackage != null)
            {
                this._excelPackage.Dispose();
                this._excelPackage = null;
            }

            //=>Clear properties
            this._title = string.Empty;
            this._company = string.Empty;
            this._author = string.Empty;
            this._created = null;
        }
        #endregion Support
    }
}
