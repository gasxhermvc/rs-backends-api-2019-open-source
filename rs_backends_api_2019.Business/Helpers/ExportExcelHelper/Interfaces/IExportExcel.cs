using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using rs_backends_api_2019.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Interfaces
{
    public interface IExportExcel : IExportExcelSupport
    {
        /// <summary>
        /// Set worksheet instnace default
        /// </summary>
        /// <param name="workSheet"></param>
        void make(ExcelWorksheet workSheet);

        /// <summary>
        /// Set Worksheet instance of Generic type
        /// </summary>
        /// <param name="workSheet"></param>
        /// <param name="data"></param>
        void make<T>(ExcelWorksheet workSheet, T data = null) where T : class;

        /// <summary>
        /// Get Worksheet instance
        /// </summary>
        /// <returns></returns>
        ExcelWorksheet getWorkSheet();

        /// <summary>
        /// เขียนชีท
        /// </summary>
        void render();

        /// <summary>
        /// เช็ท Object เพื่อคิวรี่ข้อมูล
        /// </summary>
        /// <param name="unitOfWork"></param>
        void setUnitOfWork(IUnitOfWork unitOfWork);

        /// <summary>
        /// เช็ทพารามิเตอร์
        /// </summary>
        /// <param name="parameter"></param>
        void setParameter(JObject parameter);

        /// <summary>
        /// เช็ทค่าเริ่มต้นให้กับสไตล์ชีท
        /// </summary>
        /// <param name="styles"></param>
        void setStyles(object styles);
    }
}
