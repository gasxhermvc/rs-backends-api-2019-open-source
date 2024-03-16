using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Models.Defaults.BaseModel.Parameters
{
    public class BaseParameter
    {
        public string sort { get; set; }

        public int page { get; set; }

        public int pageSize { get; set; } = 5;
    }
}
