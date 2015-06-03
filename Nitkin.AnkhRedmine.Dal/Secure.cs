using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Nitkin.AnkhRedmine.Dal
{
    class Secure
    {
        static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Salt Salt Salt and Salter");

        public static string EncryptString(string input)
        {
            byte[] encryptedData = ProtectedData.Protect(
                System.Text.Encoding.UTF8.GetBytes(input),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static string DecryptString(string encryptedData)
        {
            
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return System.Text.Encoding.UTF8.GetString(decryptedData);// ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
           
        }

        //public static SecureString ToSecureString(string input)
        //{
        //    SecureString secure = new SecureString();
        //    foreach (char c in input)
        //    {
        //        secure.AppendChar(c);
        //    }
        //    secure.MakeReadOnly();
        //    return secure;
        //}

        //public static string ToInsecureString(SecureString input)
        //{
        //    string returnValue = string.Empty;
        //    IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
        //    try
        //    {
        //        returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
        //    }
        //    finally
        //    {
        //        System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
        //    }
        //    return returnValue;
        //}
    }
}
