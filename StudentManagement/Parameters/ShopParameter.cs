using Newtonsoft.Json;
using StudentManagement.Models;
using StudentManagement.Utils;

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

        public void Validate()
        {
            ValidateName();
            ValidateDescription();
            ValidateCategory();
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
