namespace api.utils.Interfaces;

public interface IUserPasswordCripto
{
    string ReturnMD5(string password);

    bool CompareHash(string DbPassword, string userPassword);

}