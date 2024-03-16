using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Models.Defaults.BaseModel.Responses
{
    public class PutResponse
    {
        public List<PutIdData> ids { get; set; } = new List<PutIdData>();
        public string message { get; set; }
    }

    public class PutIdData
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
