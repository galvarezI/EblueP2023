using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaciendaT5K
{
    public class Encryption
    {
        //private System.Security.Cryptography.RNGCryptoServiceProvider cryotiserviceprovioder;
        public static string hashstring(string str)
        {
            return Encryptstring(str);
        }
        public static string Genereatesalt()
        {
            using (System.Security.Cryptography.RNGCryptoServiceProvider cryotiserviceprovioder = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                byte[] data = new byte[] { 4 };
                for (var i = 0; i <= 6; i++)
                {
                    cryotiserviceprovioder.GetBytes(data);
                    string value = BitConverter.ToString(data, 0);
                    sb.Append(value);
                }
                return Encryptstring(sb.ToString());
            }
        }
        private static string Encryptstring(string str)
        {
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(str);
            var hashed = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashed);
        }
    }
}