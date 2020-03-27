using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeMoenenAPI.Models;

namespace CafeMoenenAPI.Helpers
{
    public static class ExtensionMethods
    {

        public static UserModel WithoutPassword(this UserModel user)
        {
            user.PasswordHash = null;
            return user;
        }

        public static string GenerateRandomString(int length)
        {
            var rand = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
        }
    }
}
