using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Models.Defaults.BaseModel.Responses
{
    public class PostResponse
    {
        public List<PostIdData> ids { get; set; }
        public string message { get; set; }
    }

    public class PostIdData
    {
        public string name { get; set; }
        public int value { get; set; }
    }

}
