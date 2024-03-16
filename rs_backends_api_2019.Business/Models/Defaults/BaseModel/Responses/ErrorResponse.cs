using rs_backends_api_2019.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Models.Defaults.BaseModel.Responses
{
    public class ErrorResponse : IBaseResponse
    {
        public ErrorBody error { get; set; } = new ErrorBody();
    }

    public class ErrorBody
    {
        public int code { get; set; }

        public string message { get; set; }
    }
}
