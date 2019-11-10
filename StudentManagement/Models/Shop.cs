using StudentManagement.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class Shop
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public ShopStatus Status { get; set; }

        public List<Product> Products { get; set; }

        public Shop()
        { }

        public Shop(ShopParameter shopParameter)
        {
            Name = shopParameter.Name;
            Description = shopParameter.Description;
            Category = shopParameter.Category;
            Status = shopParameter.Status;
        }
    }
}
