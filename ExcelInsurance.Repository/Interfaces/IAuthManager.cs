namespace ExcelInsurance.Repository.Interfaces
{
    public interface IAuthManager
    {
        bool ValidateUserLogin(string txt_username, string txt_password);
    }
}
