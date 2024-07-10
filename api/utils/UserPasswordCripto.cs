using System.Security.Cryptography;
using System.Text;
using api.utils.Interfaces;

namespace api.utils;

public class UserPasswordCripto : IUserPasswordCripto
{
    public string ReturnMD5(string password)
    {
        using (MD5 md5Hash = MD5.Create())
        {
            return ReturnHash(md5Hash, password);
        }
    }

    public bool CompareHash(string userPassword, string DbPassword)
    {
        string hashPassword = ReturnMD5(userPassword);
        if (CheckHash(DbPassword, hashPassword))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static string ReturnHash(MD5 md5Hash, string password)
    {
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            stringBuilder.Append(data[i].ToString("x2"));
        }
        return stringBuilder.ToString();
    }

    private static bool CheckHash(string DbPassword, string hash)
    {
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;
        if (comparer.Compare(DbPassword, hash) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
