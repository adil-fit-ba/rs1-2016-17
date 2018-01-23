using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CondorExtreme3_UI.Helper
{
    public class UIHelper
    {
        public static string GenerateSalt()
        {
            byte[] array = new byte[16];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetBytes(array);
            return Convert.ToBase64String(array);
        }

        public static string GenerateHash(string Password, string Salt)
        {
            byte[] bytePassword = Encoding.Unicode.GetBytes(Password);
            byte[] byteSalt = Convert.FromBase64String(Salt);
            byte[] forHashing = new byte[bytePassword.Length + byteSalt.Length];

            System.Buffer.BlockCopy(bytePassword, 0, forHashing, 0,bytePassword.Length);
            System.Buffer.BlockCopy(byteSalt, 0, forHashing, bytePassword.Length, byteSalt.Length);

            HashAlgorithm alg = HashAlgorithm.Create("SHA1");
            return Convert.ToBase64String( alg.ComputeHash(forHashing));
        }

    }
}
