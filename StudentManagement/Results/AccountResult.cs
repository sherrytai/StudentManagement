using Newtonsoft.Json;
using StudentManagement.Models;

namespace StudentManagement.Results
{
    public class AccountResult
    {
        [JsonProperty("id", Order = 0, Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("username", Order = 1, Required = Required.Always)]
        public string Username { get; set; }

        [JsonProperty("email", Order = 2, Required = Required.Always)]
        public string Email { get; set; }

        public AccountResult()
        { }

        public AccountResult(Account account)
        {
            Id = account.Id;
            Username = account.Name;
            Email = account.Email;
        }
    }
}
