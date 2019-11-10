using Microsoft.EntityFrameworkCore;
using StudentManagement.Parameters;

namespace StudentManagement.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public DbSet<Shop> Shops { get; set; }

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
