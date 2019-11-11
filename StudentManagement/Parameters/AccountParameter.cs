using Newtonsoft.Json;
using StudentManagement.Exceptions;
using StudentManagement.Utils;

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
            Validator.ValidateName("username", Username); // TODO validate the max length
        }

        public void ValidateEmail()
        {
            Validator.ValidateEmail("email", Email);
        }

        public void ValidatePassword()
        {
            Validator.ValidateString("password", Password);

            if (Password.Contains(" "))
            {
                throw new InvalidParameterException("password can not contain white space.");
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
    }
}
