using StudentManagement.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public List<Shop> Shops { get; set; }

        public Account()
        { }

        public Account(AccountParameter account)
        {
            Name = account.Username;
            Email = account.Email;
            Password = account.GetCryptedPassword();
        }
    }
}
