using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Interfaces
{
    public interface IExportExcelSupport
    {
        /// <summary>
        /// คิวรี่ข้อมูลเพื่อใช้แสดงผลชีท
        /// </summary>
        void query();

        /// <summary>
        /// เช็ทข้อมูลไปให้กับ data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        JObject collection<T>(T data = null) where T : class;

        /// <summary>
        /// เขียนส่วนหัว
        /// </summary>
        void renderHeader();

        /// <summary>
        /// เขียนส่วนตัว
        /// </summary>
        void renderBody();

        /// <summary>
        /// เช็ทข้อมูล Resource
        /// </summary>
        /// <param name="resource"></param>
        void setResources(dynamic resource);

        /// <summary>
        /// เรียกข้อมูล Resource
        /// </summary>
        /// <returns></returns>
        dynamic getResources();

        /// <summary>
        /// เรียกพารามิเตอร์ตามคลาส
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T getParameter<T>() where T : class;

        /// <summary>
        /// เรียกใช้งานพารามิเตอร์จากคีย์
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        dynamic param(string key);

        /// <summary>
        /// ดึงค่าตำแหน่งชีท เพื่อหาว่ากำลังประมวลผลอยู่ตำแหน่งชีทที่เท่าไหร่
        /// </summary>
        /// <returns></returns>
        int getSheetIndex();

        /// <summary>
        /// กำหนดค่าตำแหน่งชีท
        /// </summary>
        /// <param name="index"></param>
        void setSheetIndex(int index);
    }
}
