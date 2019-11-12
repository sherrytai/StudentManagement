using Microsoft.EntityFrameworkCore;
using StudentManagement.Parameters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        [MaxLength(128)]
        public string Category { get; set; }

        [Required]
        public ShopStatus Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public Shop()
        {
        }

        public Shop(ShopParameter shopParameter) : this()
        {
            Name = shopParameter.Name;
            Description = shopParameter.Description;
            Category = shopParameter.Category;
            if (shopParameter.Status != null && shopParameter.Status.HasValue)
            {
                Status = shopParameter.Status.Value;
            }

            AccountId = shopParameter.AccountId;
        }
    }
}
