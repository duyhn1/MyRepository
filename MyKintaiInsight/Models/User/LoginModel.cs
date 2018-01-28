using System;
namespace MyKintaiInsight.Models.User
{
    public class LoginModel
    {
        public LoginModel()
        {
        }
        public string username { get; set; }
        public string password { get; set; }
        public bool remember { get; set; }
    }
}
