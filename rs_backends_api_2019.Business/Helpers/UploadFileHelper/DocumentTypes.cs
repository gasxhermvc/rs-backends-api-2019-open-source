using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.UploadFileHelper
{
    public enum DocumentTypes
    {
        [Description("all")]
        All = 0,
        [Description("document")]
        Document = 1,
        [Description("image")]
        Image = 2,
        [Description("archive")]
        Archive = 3,
        [Description("audio")]
        Audio = 4,
        [Description("video")]
        Video = 5,
        [Description("none")]
        None = 99,
        [Description("unknow")]
        Unknow = 999
    }
}
