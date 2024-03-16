using Microsoft.AspNetCore.Mvc;
using rs_backends_api_2019.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ResponseHelper.Interfaces
{
    public interface IResponseService
    {
        //IBaseResponse BuildResponse(IBaseResponse model);

        ActionResult Response(ControllerBase controller, IBaseResponse response);
    }
}
