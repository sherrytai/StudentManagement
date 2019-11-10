using Microsoft.EntityFrameworkCore;
using StudentManagement.Parameters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        [MaxLength(128)]
        public string CryptedPassword { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        public List<Shop> Shops { get; set; }

        public Account()
        {
            if (Shops == null)
            {
                Shops = new List<Shop>();
            }
        }

        public Account(AccountParameter account)
        {
            Name = account.Username;
            Email = account.Email;
            CryptedPassword = account.GetCryptedPassword();

            if (Shops == null)
            {
                Shops = new List<Shop>();
            }
        }
    }
}
