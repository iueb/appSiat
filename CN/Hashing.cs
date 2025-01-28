﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integracionUEB
{
    public static class Hashing
    {
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
               // tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
                //tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

               // return Convert.ToHexString(hashBytes);
               //return Convert.ToString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }
}
