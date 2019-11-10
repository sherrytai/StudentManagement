using Microsoft.EntityFrameworkCore;
using StudentManagement.Parameters;

namespace StudentManagement.Models
{
    public class Shop
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public ShopStatus Status { get; set; }

        public DbSet<Product> Products { get; set; }

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
