
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapi;
using webapi.Application;
using static WebApi.Model.DataModel;

namespace WebApi.Controllers
{       
    public class AuthenticateController : Controller
    {
        private readonly DapperContext _context;
        public AuthenticateController(DapperContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/getappdata")]
        public async Task<string> GetAppData()
        {
            /*var query = "SELECT * FROM AppBasicInfo";
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var appData = await connection.QueryAsync<AppData>(query);
                    return appData.ToBaseModel("GetAppData","User Authenticated Successfully!", true);
                }
                catch (Exception e)
                {
                    return "".ToBaseModel("GetAppData", "Unable To Authenticate!", false);
                }
            }*/

            return GetData().ToBaseModel("GetAppData", "User Authenticated Successfully!", true);
        }

        [HttpGet]
        [Route("api/getappdata{id:int}")]
        public async Task<ActionResult<BaseModel>> Get(int id)
        {
            var appData = GetAppData();

            BaseModel baseModel = new BaseModel();
            baseModel.Status = true;
            baseModel.Msg = "User Authorized";
            baseModel.Data = appData;
            return baseModel;
        }

        [HttpPost]
        [Route("api/authenticate")]
        public async Task<string> Authenticate(string data)
        {
            DeviceInfo deviceInfo = JsonConvert.DeserializeObject<DeviceInfo>(data);
            var query = $"INSERT INTO UserDeviceInfo (Device,DeviceType,DeviceModel,Os,OsFamily,Width,Height,AppVer,ProductName,BuildGuid,Genuine,Installer,Platform)" +
                $" VALUES({deviceInfo.Device}, {deviceInfo.DeviceType}, {deviceInfo.DeviceModel},{deviceInfo.Os}, {deviceInfo.OsFamily}, {deviceInfo.Width}, {deviceInfo.Height}, {deviceInfo.AppVer}, {deviceInfo.ProductName}, {deviceInfo.BuildGuid},{deviceInfo.IsGenuine}, {deviceInfo.Installer},{deviceInfo.Platform})";
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var appData = await connection.QueryAsync(query);
                    return appData.ToBaseModel("GetAppData", "User Authenticated Successfully!", true);
                }
                catch (Exception e)
                {
                    return "".ToBaseModel("GetAppData", "Unable To Authenticate!", false);
                }
            }
        }


        AppData GetData()
        {
            AppData app = new AppData();
            app.BrandName = "Skynet";
            app.WhatsappSupport = "+919999999999";
            app.EmailSupport = "test@gmail.com";
            app.TelegramSupport = "+919999999999";
            app.TermsConditions = "I am 18 years";
            app.ScaleRatio = "0.5f";
            return app;
        }
    }
}
