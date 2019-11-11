using Newtonsoft.Json;
using StudentManagement.Models;

namespace StudentManagement.Results
{
    public class ShopResult
    {
        [JsonProperty(PropertyName = "id", Order = 0)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name", Order = 1)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", Order = 2)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "category", Order = 3)]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "status", Order = 4)]
        public ShopStatus Status { get; set; } = ShopStatus.Draft;

        [JsonProperty(PropertyName = "accountId", Order = 5)]
        public int AccountId { get; set; }

        public ShopResult()
        { }

        public ShopResult(Shop shop)
        {
            Id = shop.Id;
            Name = shop.Name;
            Description = shop.Description;
            Category = shop.Category;
            Status = shop.Status;
            AccountId = shop.AccountId;
        }
    }
}
