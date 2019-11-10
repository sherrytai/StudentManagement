using Newtonsoft.Json;
using StudentManagement.Models;
using StudentManagement.Utils;

namespace StudentManagement.Parameters
{
    public class ShopParameter
    {
        [JsonProperty(PropertyName = "accountId", Order = 0)]
        public int AccountId { get; set; }

        [JsonProperty(PropertyName = "name", Order = 1)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", Order = 2)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "category", Order = 3)]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "status", Order = 4)]
        public ShopStatus Status { get; set; } = ShopStatus.Draft;

        public void Validate()
        {
            ValidateId();
            ValidateName();
            ValidateDescription();
            ValidateCategory();
        }

        public void ValidateId()
        {
            Validator.ValidateId("accountId", AccountId);
        }

        public void ValidateName()
        {
            Validator.ValidateString("name", Name);
        }

        public void ValidateDescription()
        {
            Validator.ValidateString("description", Description);
        }

        public void ValidateCategory()
        {
            Validator.ValidateString("category", Category);
        }
    }
}
