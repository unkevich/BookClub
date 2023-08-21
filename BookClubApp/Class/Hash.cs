﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BookClubApp.Class
{
    internal class Hash
    {
        public static string hashPassword(string password) {
            MD5 md5 = MD5.Create();
            byte[] b = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(b);

            StringBuilder sb = new StringBuilder();
            foreach (var a in hash) { sb.Append(a.ToString("X2")); }
            return Convert.ToString(sb);
        }
    }
}
