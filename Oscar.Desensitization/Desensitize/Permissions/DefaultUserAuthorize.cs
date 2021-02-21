using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oscar.Desensitization.Desensitize.Permissions
{
    public class DefaultUserAuthorize
    {
        public bool DisplayOutIDNumber { get; set; }
        public bool DisplayIDNumber { get; set; }
        public bool DisplayProvinceName { get; set; }
        public bool DisplayCityName { get; set; }
        public bool DisplayAccountNumber { get; set; }
        public bool DisplayPhone { get; set; }
        public bool DisplayOtherContact { get; set; }
        public bool DisplayIDCardAddress { get; set; }
    }
}