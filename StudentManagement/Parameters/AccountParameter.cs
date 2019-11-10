using Newtonsoft.Json;
using StudentManagement.Exceptions;
using StudentManagement.Utils;
using System.Text.RegularExpressions;

namespace StudentManagement.Parameters
{
    public class AccountParameter
    {
        [JsonProperty(PropertyName = "username", Order = 0)]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "email", Order = 1)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password", Order = 2)]
        public string Password { get; set; }

        public void Validate()
        {
            ValidateUsername();
            ValidateEmail();
            ValidatePassword();
        }

        public void ValidateUsername()
        {
            Validator.ValidateString("username", Username);
        }

        public void ValidateEmail()
        {
            Validator.ValidateString("email", Email);
        }

        public void ValidatePassword()
        {
            Validator.ValidateString("password", Password);

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
