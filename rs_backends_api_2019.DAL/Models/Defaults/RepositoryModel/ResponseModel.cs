using rs_backends_api_2019.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace rs_backends_api_2019.DAL.Models.Defaults.RepositoryModel
{
    /// <summary>
    /// Class Model ที่ return ผลลัพท์ของ function
    /// </summary>
    public class ResponseModel : IBaseResponse
    {
        /// <summary>
        /// ประเภทการทำงานของ function ใน file service
        /// action : get, insert, update, delete
        /// </summary>
        public string action { get; set; }

        public HttpStatusCode httpStatus { get; set; }

        public string message { get; set; }

        public bool isSuccess { get; set; } = false;

        public int[] lastID { get; set; }

        public dynamic dataResponse { get; set; }

        public string errMesage { get; set; } = "";
    }
}
