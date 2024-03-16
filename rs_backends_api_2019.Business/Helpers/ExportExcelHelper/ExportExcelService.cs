using rs_backends_api_2019.Business.Helpers.ExportExcelHelper.Interfaces;
using rs_backends_api_2019.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ExportExcelHelper
{
    public class ExportExcelService : IExportExcelService
    {
        public IExportExcelProvider excelHelper(IUnitOfWork unitOfWork, object parameter = null)
        {
            IExportExcelProvider provider = null;

            if (parameter == null)
            {
                provider = new ExportExcelProvider(unitOfWork);
            }
            else
            {
                provider = new ExportExcelProvider(unitOfWork, parameter);
            }

            return provider;
        }
    }
}
