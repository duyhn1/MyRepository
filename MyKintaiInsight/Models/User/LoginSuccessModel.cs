using System;
namespace MyKintaiInsight.Models.User
{
    public class LoginSuccessModel
    {
        public LoginSuccessModel()
        {
        }
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public object error { get; set; }
        public object som { get; set; }
        public object sms { get; set; }
        public bool? isError { get; set; }
    }
}
