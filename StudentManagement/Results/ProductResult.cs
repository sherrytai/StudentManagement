using System;
using Newtonsoft.Json;
using StudentManagement.Models;

namespace StudentManagement.Results
{
    public class ProductResult
    {
        [JsonProperty(PropertyName = "id", Order = 0)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name", Order = 1)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", Order = 2)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "status", Order = 3)]
        public ProductStatus Status { get; set; } = ProductStatus.Draft;

        [JsonProperty(PropertyName = "price", Order = 4)]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "amount", Order = 5)]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "shopId", Order = 6)]
        public int ShopId { get; set; }

        [JsonProperty(PropertyName = "shopName", Order = 7)]
        public string ShopName { get; set; }

        public ProductResult()
        { }

        public ProductResult(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Status = product.Status;
            Price = product.Price;
            Amount = product.Amount;
            ShopId = product.ShopId;
            ShopName = product.Shop.Name;
        }
    }
}
