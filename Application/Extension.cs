using Newtonsoft.Json;
using WebApi;
using static WebApi.Model.DataModel;
using System.Security.Cryptography;
using System.Text;

namespace webapi.Application
{
    public static class Extension
    {
        public static string ToBaseModel(this object data, string method, string msg, bool status)
        {
            return ToEncrypt(
                JsonConvert.SerializeObject(new BaseModel()
                {
                    Method = method,
                    Data = data,
                    Status = status,
                    Msg = msg
                }), IsEncrypt: false);
        }

        public static string ToEncrypt(this string plainText, bool IsEncrypt = false)
        {
            if (!IsEncrypt)
            {
                return plainText;
            }

            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Constant.cipherKey);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string ToDecrypt(this string cipherText, bool IsDecrypt = false)
        {
            if (!IsDecrypt)
            {
                return cipherText;
            }

            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Constant.cipherKey);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
