using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class Product
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
        public double Price { get; set; }

        [Required]
        public int Amount { get; set; }

        public ProductStatus Status { get; set; }

        public int ShopId { get; set; }

        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }
    }
}
