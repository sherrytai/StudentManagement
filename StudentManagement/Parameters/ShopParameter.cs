using Newtonsoft.Json;
using StudentManagement.Models;
using StudentManagement.Utils;

namespace StudentManagement.Parameters
{
    public class ShopParameter : ShopUpdateParameter
    {
        [JsonProperty(PropertyName = "accountId", Order = 5)]
        public int AccountId { get; set; }

        public override void Validate()
        {
            ValidateId();
            base.Validate();
        }

        public void ValidateId()
        {
            Validator.ValidateId("accountId", AccountId);
        }
    }
}
