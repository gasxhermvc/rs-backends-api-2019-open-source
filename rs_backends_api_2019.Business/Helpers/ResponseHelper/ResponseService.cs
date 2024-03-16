using Microsoft.AspNetCore.Mvc;
using rs_backends_api_2019.Business.Helpers.ResponseHelper.Interfaces;
using rs_backends_api_2019.Business.Models.Defaults.BaseModel.Responses;
using rs_backends_api_2019.DAL.Interfaces;
using rs_backends_api_2019.DAL.Models.Defaults.RepositoryModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.ResponseHelper
{
    public class ResponseService : IResponseService
    {
        private IBaseResponse BuildResponse(IBaseResponse model)
        {
            IBaseResponse response = null;

            var responseModel = model as ResponseModel;

            switch (responseModel.httpStatus)
            {
                case HttpStatusCode.OK:

                    response = new BaseResponse()
                    {
                        data = responseModel.dataResponse
                    };

                    break;

                case HttpStatusCode.BadRequest:
                    response = new ErrorResponse
                    {
                        error = new ErrorBody()
                        {
                            code = (int)responseModel.httpStatus,
                            message = responseModel.message
                        }
                    };

                    break;

                case HttpStatusCode.InternalServerError:
                    response = new ErrorResponse
                    {
                        error = new ErrorBody()
                        {
                            code = (int)responseModel.httpStatus,
                            message = responseModel.message
                        }
                    };

                    break;

                case HttpStatusCode.NoContent:
                    response = new BaseResponse()
                    {
                        data = responseModel.dataResponse
                    };

                    break;
            }

            return response;
        }

        public ActionResult Response(ControllerBase controller, IBaseResponse model)
        {
            IBaseResponse response = BuildResponse(model);

            if (response is BaseResponse)
            {
                return controller.Ok(response);
            }

            return controller.BadRequest(response);
        }
    }
}
