using System.Security.Cryptography;
using System.Text;

namespace api.utils
{
    public class UserPasswordCripto
    {
        public string RetornarMD5(string senha)
        {
            // todas as instancias seja finalizada ao usar o using
            using (MD5 md5Hash =  MD5.Create())
            {
                return RetornarHash(md5Hash, senha);
            }
        }
        public bool CompararMD5(string senha, string senhaBD)
        {
            string novaSenha = RetornarMD5(senha);
            if (VerificarHash(senhaBD, novaSenha))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string RetornarHash(MD5 md5Hash, string input)
        {
            // obter um array de bytes com a string de entrada (input)
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // construir a string final
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++) 
            {
                stringBuilder.Append(data[i].ToString("X2"));
            }

            return stringBuilder.ToString();    
        }
        public bool VerificarHash(string input, string hash)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (comparer.Compare(input, hash) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    }
