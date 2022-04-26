using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class DataModel
    {
        public class BaseModel
        {
            public bool Status { get; set; }    
            public string Msg { get; set; }
            public string Method { get; set; }
            public object Data { get; set; }
        }
        public class AppData
        {
            public string BrandName { get; set; }
            public string ScaleRatio { get; set; }
            public string TermsConditions { get; set; }
            public string WhatsappSupport { get; set; }
            public string TelegramSupport { get; set; }
            public string EmailSupport { get; set; }
        }
        public class DeviceInfo
        {
            public string Device { get; set; }
            public string DeviceType { get; set; }
            public string DeviceModel { get; set; }
            public string Os { get; set; }
            public string OsFamily { get; set; }
            public string Width { get; set; }
            public string Height { get; set; }
            public string AppVer { get; set; }
            public string ProductName { get; set; }
            public string BuildGuid { get; set; }
            public bool IsGenuine { get; set; }
            public string Installer { get; set; }
            public string Platform { get; set; }

        }
    }
}
