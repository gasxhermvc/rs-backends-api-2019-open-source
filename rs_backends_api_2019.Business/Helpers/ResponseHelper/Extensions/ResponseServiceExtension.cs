using Microsoft.AspNetCore.Mvc;
using rs_backends_api_2019.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ResponseHelper.Extensions
{
    public static class ResponseServiceExtension
    {
        public static ActionResult ResponseResult(this ControllerBase controller, IBaseResponse model)
        {
            return new ResponseService()
                .Response(controller, model);
        }
    }
}
