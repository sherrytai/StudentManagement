using Newtonsoft.Json;
using StudentManagement.Models;

namespace StudentManagement.Parameters
{
    public class ShopParameter
    {

        [JsonProperty(PropertyName = "name", Order = 0)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", Order = 1)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "category", Order = 2)]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "status", Order = 3)]
        public ShopStatus Status { get; set; } = ShopStatus.Draft;
    }
}
