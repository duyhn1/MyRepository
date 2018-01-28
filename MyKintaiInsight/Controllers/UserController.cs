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
        private readonly string loginUrl = "https://employee.fjpservice.com/api/login";
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
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var respone = client.PostAsJsonAsync(loginUrl, data).Result;
            var result = respone.Content.ReadAsStringAsync().Result;
            var loginSuccess = JsonConvert.DeserializeObject<LoginSuccessModel>(result);
            if(string.IsNullOrEmpty(loginSuccess.access_token)) 
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }
    }
}
