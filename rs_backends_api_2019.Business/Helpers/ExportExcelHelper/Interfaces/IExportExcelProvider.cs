using rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Models;
using rs_backends_api_2019.Business.Helpers.UploadFileHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Interfaces
{
    public interface IExportExcelProvider : IRawFile
    {
        /// <summary>
        /// นำออกเอกสาร excel
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        ExportExcelResult export(string filename, Action action);

        /// <summary>
        /// ตั้งค่าชื่อไฟล์ Excel ภายใน Properties
        /// </summary>
        /// <param name="title"></param>
        void title(string title);

        /// <summary>
        /// ตั้งค่าชื่อบริษัท Excel ภายใน Properties
        /// </summary>
        /// <param name="company"></param>
        void company(string company);

        /// <summary>
        /// ตั้งค่าชื่อผู้ทำ Excel ภายใน Properties
        /// </summary>
        /// <param name="author"></param>
        void author(string author);

        /// <summary>
        /// ระบุเวลาที่สร้างไฟล์ ภายใน Properties
        /// </summary>
        /// <param name="created"></param>
        void created(DateTime created);

        /// <summary>
        /// สร้างชีทภายใน Excel
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="cls"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        void makeWorkSheet(string sheetName, IExportExcel cls);

        /// <summary>
        /// สร้างชีทภายใน Excel ด้วยรูปแบบที่มีอยู่แล้ว
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="cls"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        void makeWorkSheets(Func<dynamic, object> sheetName, IExportExcel cls);

        /// <summary>
        /// สร้างชื่อไฟล์มาตรฐาน Report
        /// Report_{filename}_{date}_{randomStr}.xlsx
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string fileNameStandard(string fileName);

        /// <summary>
        /// สุ่มสร้างชื่อไฟล์
        /// style 
        /// = empty => Report_{date}_{randomStr}.xlsx
        /// = a => Report_{longdate}_{randomStr}.xlsx
        /// = b => Report_{date}_{randomStr}.xlsx
        /// = c => hashSha1(Report_{longdate}_{randomStr}).xlsx
        /// = d => hashSha1(Report_{date}_{randomStr}).xlsx
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        string fileNameGenerate(string extension = ".xlsx", string style = "");

        /// <summary>
        /// โหลดค่า คอนฟิก หรือ ค่าเริ่มต้น
        /// </summary>
        void autoload();

        /// <summary>
        /// ล้างค่า
        /// </summary>
        void clearObject();

    }
}
