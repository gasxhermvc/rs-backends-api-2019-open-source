using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace rs_backends_api_2019.Business.Enums
{
    public enum EnumFilePath
    {
        [Description("Images/Users"), PathFolder("Images/Users")]
        user = 1,

        [Description("Reports/WebCrawling"), PathFolder("Reports/WebCrawling")]
        crawling = 2,

        [Description("Images/Guides"), PathFolder("Images/Guides")]
        guide = 3,


        [Description("examination"), PathFolder("examination")]
        examination = 4,
    }

    public sealed class PathFolderAttribute : Attribute
    {
        private string _value;

        public PathFolderAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}
