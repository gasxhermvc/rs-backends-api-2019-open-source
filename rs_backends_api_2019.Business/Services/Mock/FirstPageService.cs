using Newtonsoft.Json;
using rs_backends_api_2019.Business.Interfaces.Mock;
using rs_backends_api_2019.Business.Models.Mock.Parameters.FirstPage;
using rs_backends_api_2019.DAL.Models.Defaults.RepositoryModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace rs_backends_api_2019.Business.Services.Mock
{
    public class FirstPageService : IFirstPageService
    {
        public ResponseModel responseModel { get; set; } = new ResponseModel();

        public ResponseModel InterestingCategorys(InterestingCategorysParameter param)
        {
            object json = new object();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string mockDataPath = Path.Combine(basePath, "MockData",
                "FirstPage",
                param.lang,
                "1_1_InterestingCategorys.json");

            if (System.IO.File.Exists(mockDataPath))
            {
                json = JsonConvert.DeserializeObject<object>(File.ReadAllText(mockDataPath,Encoding.UTF8));

                this.responseModel.action = "get";
                this.responseModel.httpStatus = HttpStatusCode.OK;
                this.responseModel.dataResponse = json;
                this.responseModel.message = "ดึงข้อมูลหมวดหมูที่น่าสนใจสำเร็จ";
            }
            else
            {
                this.responseModel.action = "get";
                this.responseModel.httpStatus = HttpStatusCode.NoContent;
                this.responseModel.dataResponse = null;
                this.responseModel.message = "ดึงข้อมูลหมวดหมูที่น่าสนใจไม่สำเร็จ";
            }

            return this.responseModel;
        }

        public ResponseModel InterestingLandmark(InterestingLandmarkParameter param)
        {
            object json = new object();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string mockDataPath = Path.Combine(basePath, "MockData",
                "FirstPage",
                param.lang,
                "1_2_InterestingLandmark.json");

            if (System.IO.File.Exists(mockDataPath))
            {
                json = JsonConvert.DeserializeObject<object>(File.ReadAllText(mockDataPath, Encoding.UTF8));

                this.responseModel.action = "get";
                this.responseModel.httpStatus = HttpStatusCode.OK;
                this.responseModel.dataResponse = json;
                this.responseModel.message = "ดึงข้อมูลแหล่งท่องเที่ยวยอดนิยมสำเร็จ";
            }
            else
            {
                this.responseModel.action = "get";
                this.responseModel.httpStatus = HttpStatusCode.NoContent;
                this.responseModel.dataResponse = null;
                this.responseModel.message = "ดึงข้อมูลแหล่งท่องเที่ยวยอดนิยมไม่สำเร็จ";
            }

            return this.responseModel;
        }

        public ResponseModel NearbyAttactions(NearbyAttactionsParameter param)
        {
            object json = new object();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string mockDataPath = Path.Combine(basePath, "MockData",
                "FirstPage",
                param.lang,
                "1_3_NearbyAttactions.json");

            if (System.IO.File.Exists(mockDataPath))
            {
                json = JsonConvert.DeserializeObject<object>(File.ReadAllText(mockDataPath, Encoding.UTF8));

                this.responseModel.action = "get";
                this.responseModel.httpStatus = HttpStatusCode.OK;
                this.responseModel.dataResponse = json;
                this.responseModel.message = "ดึงข้อมูลแหล่งท่องเที่ยวใกล้ผู้ใช้งานสำเร็จ";
            }
            else
            {
                this.responseModel.action = "get";
                this.responseModel.httpStatus = HttpStatusCode.NoContent;
                this.responseModel.dataResponse = null;
                this.responseModel.message = "ดึงข้อมูลแหล่งท่องเที่ยวใกล้ผู้ใช้งานไม่สำเร็จ";
            }

            return this.responseModel;
        }

        public ResponseModel InterestingEvents(InterestingEventsParameter param)
        {
            object json = new object();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string mockDataPath = Path.Combine(basePath, "MockData",
                "FirstPage",
                param.lang,
                "1_4_InterestingEvents.json");

            if (System.IO.File.Exists(mockDataPath))
            {
                json = JsonConvert.DeserializeObject<object>(File.ReadAllText(mockDataPath, Encoding.UTF8));

                this.responseModel.action = "get";
                this.responseModel.httpStatus = HttpStatusCode.OK;
                this.responseModel.dataResponse = json;
                this.responseModel.message = "ดึงข้อมูลกิจกรรมที่น่าสนใจสำเร็จ";
            }
            else
            {
                this.responseModel.action = "get";
                this.responseModel.httpStatus = HttpStatusCode.NoContent;
                this.responseModel.dataResponse = null;
                this.responseModel.message = "ดึงข้อมูลกิจกรรมที่น่าสนใจไม่สำเร็จ";
            }

            return this.responseModel;
        }

        public ResponseModel InterestingTips(InterestingTipsParameter param)
        {
            object json = new object();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string mockDataPath = Path.Combine(basePath, "MockData",
                "FirstPage",
                param.lang,
                "1_5_InterestingTips.json");

            if (System.IO.File.Exists(mockDataPath))
            {
                json = JsonConvert.DeserializeObject<object>(File.ReadAllText(mockDataPath, Encoding.UTF8));

                this.responseModel.action = "get";
                this.responseModel.httpStatus = HttpStatusCode.OK;
                this.responseModel.dataResponse = json;
                this.responseModel.message = "ดึงข้อมูลทริปที่น่าสนใจสำเร็จ";
            }
            else
            {
                this.responseModel.action = "get";
                this.responseModel.httpStatus = HttpStatusCode.NoContent;
                this.responseModel.dataResponse = null;
                this.responseModel.message = "ดึงข้อมูลทริปที่น่าสนใจไม่สำเร็จ";
            }

            return this.responseModel;
        }
    }
}
