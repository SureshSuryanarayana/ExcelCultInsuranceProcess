using ExcelInsurance.Repository.Interfaces;
using System.Configuration;

namespace ExcelInsurance.Repository.Implementations
{
    public class AuthManager : IAuthManager
    {
        public bool ValidateUserLogin(string txt_username, string txt_password)
        {
            string app_username = ConfigurationManager.AppSettings["username"].ToString();
            string app_password = ConfigurationManager.AppSettings["password"].ToString();

            return (app_username == txt_username && app_password == txt_password) ? true : false;
        }
    }
}
