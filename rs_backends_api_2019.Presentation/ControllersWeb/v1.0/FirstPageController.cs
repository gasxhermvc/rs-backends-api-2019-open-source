using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rs_backends_api_2019.Business.Helpers.ResponseHelper.Extensions;
using rs_backends_api_2019.Business.Interfaces.Mock;
using rs_backends_api_2019.Business.Models.Mock.Parameters.FirstPage;
using rs_backends_api_2019.DAL.Models.Defaults.RepositoryModel;

namespace rs_backends_api_2019.Presentation.ControllersWeb.v1._0
{
    [ApiVersion("1.0")]
    [Route("api/web/v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class FirstPageController : ControllerBase
    {
        private readonly IFirstPageService _firstPageService;

        private ResponseModel response { get; set; } = new ResponseModel();

        public FirstPageController(IFirstPageService firstPageService)
        {
            _firstPageService = firstPageService;
        }

        [HttpGet]
        [Route("InterestingCategorys")]
        public IActionResult InterestingCategorys(InterestingCategorysParameter param)
        {
            response = _firstPageService.InterestingCategorys(param);

            return this.ResponseResult(response);
        }

        [HttpGet]
        [Route("InterestingLandmark")]
        public IActionResult InterestingLandmark(InterestingLandmarkParameter param)
        {
            response = _firstPageService.InterestingLandmark(param);

            return this.ResponseResult(response);
        }

        [HttpGet]
        [Route("NearbyAttactions")]
        public IActionResult NearbyAttactions(NearbyAttactionsParameter param)
        {
            response = _firstPageService.NearbyAttactions(param);

            return this.ResponseResult(response);
        }

        [HttpGet]
        [Route("InterestingEvents")]
        public IActionResult InterestingEvents(InterestingEventsParameter param)
        {
            response = _firstPageService.InterestingEvents(param);

            return this.ResponseResult(response);
        }

        [HttpGet]
        [Route("InterestingTips")]
        public IActionResult InterestingTips(InterestingTipsParameter param)
        {
            response = _firstPageService.InterestingTips(param);

            return this.ResponseResult(response);
        }
    }
}