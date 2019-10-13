using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Service
{
    public static class HashProvider
    {
        public static string BasicCrypt(this string data)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(data)));
        }
    }
}
