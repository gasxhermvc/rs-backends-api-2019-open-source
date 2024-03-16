using rs_backends_api_2019.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Models.Defaults.BaseModel.Responses
{
    public class BaseResponse : IBaseResponse
    {
        public object data { get; set; }
    }
}
