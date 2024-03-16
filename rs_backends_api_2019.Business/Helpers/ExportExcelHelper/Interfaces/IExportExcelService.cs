using rs_backends_api_2019.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Interfaces
{
    public interface IExportExcelService
    {
        /// <summary>
        /// get instance แบบเชื่อมต่อฐานข้อมูล
        /// </summary>
        /// <returns></returns>
        IExportExcelProvider excelHelper(IUnitOfWork unitOfWork, object parameter = null);
    }
}
