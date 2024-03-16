using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Extensions;
using rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Core
{
    public abstract class ExportExcelSupport : ExportExcelAttributes, IExportExcel
    {
        protected ExcelWorksheet worksheet { get; set; }

        protected JObject data { get; set; }

        protected bool requireQuery { get; set; }

        public virtual void make(ExcelWorksheet worksheet)
        {
            this.worksheet = worksheet;
            this.resource = null;
            this.requireQuery = true;
        }

        public virtual void make<T>(ExcelWorksheet worksheet, T data = null) where T : class
        {
            this.worksheet = worksheet;

            if (data != null)
            {
                this.resource = data;
                this.requireQuery = false;
            }
        }

        public virtual ExcelWorksheet getWorkSheet()
        {
            return this.worksheet;
        }

        public virtual void render()
        {
            if (this._styles != null)
            {
                dynamic styles = this._styles;

                this.worksheet.defaultFontSize((float)styles.FontSize);
                this.worksheet.defaultFontFamily((string)styles.Font);
            }

            if (this.requireQuery)
            {
                this.query();

                this.data = this.collection(this.resource);
            }
            else
            {
                if (this.resource != null && this.resource.ToString() != "null")
                {
                    this.data = this.collection(this.resource);
                }
                else
                {
                    this.data = this.collection((object)null);
                }
            }

            this.renderHeader();
            this.renderBody();
        }

        #region Abstraction
        public abstract void query();

        public abstract JObject collection<T>(T data = null) where T : class;

        public abstract void renderHeader();

        public abstract void renderBody();
        #endregion Abstraction
    }
}
