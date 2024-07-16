namespace api.Services.Interfaces;

public interface IUserPasswordCriptoService
{
    string ReturnMD5(string password);

    bool CompareHash(string DbPassword, string userPassword);

}