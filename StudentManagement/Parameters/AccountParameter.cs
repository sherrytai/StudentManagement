using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagement.Exceptions;
using StudentManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentManagement.Parameters
{
    public class AccountParameter
    {
        [JsonProperty("username", Order = 0, Required = Required.Always)]
        public string Username { get; set; }

        [JsonProperty("email", Order = 1, Required = Required.Always)]
        public string Email { get; set; }

        [JsonProperty("password", Order = 2, Required = Required.Always)]
        public string Password { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                throw new InvalidParameterException("Invalid username.");
            }

            if (string.IsNullOrWhiteSpace(Email) || !IsValidEmail(Email))
            {
                throw new InvalidParameterException("Invalid email.");
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                throw new InvalidParameterException("Invalid password.");
            }

            if (Password.Length < 6)
            {
                throw new InvalidParameterException("The length of password should not less than 6.");
            }
        }

        public string GetCryptedPassword()
        {
            return Md5Hash.GetHash(Password);
        }

        private bool IsValidEmail(string email)
        {
            var regex = new Regex("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}$");
            var result = regex.Match(email);
            return result.Success;
        }
    }
}
