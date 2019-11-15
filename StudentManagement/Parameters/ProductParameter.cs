using Newtonsoft.Json;
using StudentManagement.Models;
using StudentManagement.Utils;

namespace StudentManagement.Parameters
{
    public class ProductParameter : ProductUpdateParameter
    {
        [JsonProperty(PropertyName = "shopId", Order = 6)]
        public int ShopId { get; set; }

        public override void Validate()
        {
            ValidateId();
            base.Validate();
        }

        public void ValidateId()
        {
            Validator.ValidateId("shopId", ShopId);
        }
    }
}
