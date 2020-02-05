using System;
using System.Security.Cryptography;
using System.Text;

namespace GymSite
{
    public static class Hashing
    {
        public static string GetHash(string source)
        {
            byte[] data = Encoding.Default.GetBytes(source);
            var res = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(res).Replace("-", "");
        }
    }
}