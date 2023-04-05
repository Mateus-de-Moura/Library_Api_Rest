using System.Security.Cryptography;
using System.Text;

namespace Biblioteca_Api.Services
{
    public static class Criptografia
    {     
        public static string Encript(string text)
        {
            try
            {
                byte[] secretkeyByte = Encoding.UTF8.GetBytes(Settings.secretkey);
                byte[] publickeybyte = Encoding.UTF8.GetBytes(Settings.publickey);

                byte[] inputbyteArray = Encoding.UTF8.GetBytes(text);
                using (var des = new DESCryptoServiceProvider())
                {
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    text = Convert.ToBase64String(ms.ToArray());
                }
                return text;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string Decript(string texto)
        {
            try
            {
                byte[] privatekeyByte = Encoding.UTF8.GetBytes(Settings.secretkey);
                byte[] publickeybyte = Encoding.UTF8.GetBytes(Settings.publickey);

                byte[] inputbyteArray = new byte[texto.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(texto.Replace(" ", "+"));
                using (var des = new DESCryptoServiceProvider())
                {
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    texto = encoding.GetString(ms.ToArray());
                }
                return texto;
            }
            catch (Exception ae)
            {
                throw new Exception(ae.Message, ae.InnerException);
            }
        }
    }

}

