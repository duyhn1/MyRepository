using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyKintaiInsight.Models.User;
using Newtonsoft.Json;

namespace MyKintaiInsight.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string loginUrl = "https://employee.fjpservice.com/api/login";
        private readonly string getWorkplaceUrl = "https://api.fjpservice.com/api/dakoku/workplace";
        private readonly string checkInOutUrl = "https://api.fjpservice.com/api/dakoku";
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var data = new Dictionary<string, string>
            {
                { "userId", model.username },
                { "password", model.password }
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsJsonAsync(loginUrl, data).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var loginSuccess = JsonConvert.DeserializeObject<LoginSuccessModel>(result);
            if(!string.IsNullOrEmpty(loginSuccess.access_token)) 
            {
                Session.Add("access_token", loginSuccess.access_token);
                Session.Add("expires", loginSuccess.expires_in);

                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public ActionResult Dashboard() 
        {
            var access_token = Session["access_token"] as string;
            if (string.IsNullOrEmpty(access_token))
            {
                RedirectToAction("Login");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(getWorkplaceUrl).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var workplace = JsonConvert.DeserializeObject<WorkplaceModel>(result);
            return View(workplace);
        }
        [HttpPost]
        public JsonResult CheckOut(decimal lat, decimal lng)
        {
            var access_token = Session["access_token"] as string;
            if (string.IsNullOrEmpty(access_token))
            {
                RedirectToAction("Login");
            }
            var data = new Dictionary<string, object>
            {
                { "canModify", false },
                { "checkinLatitude", 0 },
                { "checkinLocation", "" },
                { "checkinLongitute", 0 },
                { "checkinType", 0 },
                { "checkoutLatitude", lat },
                { "checkoutLocation", "" },
                { "checkoutLongitute", lng },
                { "checkoutType", 1 },
                { "displayEditEndWorkingTime", "" },
                { "displayEditStartWorkingTime", "" },
                { "displayEndWorkingTime", "" },
                { "displayFixEndWorkingTime", "" },
                { "displayFixStartWorkingTime", "" },
                { "displayOrgEditEndWorkingTime", "" },
                { "displayOrgEditStartWorkingTime", "" },
                { "displayStartWorkingTime", "" },
                { "errorClass", "" },
                { "reason", "" }
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsJsonAsync(checkInOutUrl, data).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return Json(new { result = result });
        }
        [HttpPost]
        public JsonResult CheckIn(decimal lat, decimal lng)
        {
            var access_token = Session["access_token"] as string;
            if (string.IsNullOrEmpty(access_token))
            {
                RedirectToAction("Login");
            }
            var data = new Dictionary<string, object>
            {
                { "canModify", false },
                { "checkinLatitude", lat },
                { "checkinLocation", "" },
                { "checkinLongitute", lng },
                { "checkinType", 1 },
                { "checkoutLatitude", 0 },
                { "checkoutLocation", "" },
                { "checkoutLongitute", 0 },
                { "checkoutType", 0 },
                { "displayEditEndWorkingTime", "" },
                { "displayEditStartWorkingTime", "" },
                { "displayEndWorkingTime", "" },
                { "displayFixEndWorkingTime", "" },
                { "displayFixStartWorkingTime", "" },
                { "displayOrgEditEndWorkingTime", "" },
                { "displayOrgEditStartWorkingTime", "" },
                { "displayStartWorkingTime", "" },
                { "errorClass", "" },
                { "reason", "" }
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsJsonAsync(checkInOutUrl, data).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return Json(new { result = result });
        }
    }
}
