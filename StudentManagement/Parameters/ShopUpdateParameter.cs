using Newtonsoft.Json;
using StudentManagement.Exceptions;
using StudentManagement.Models;
using StudentManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Parameters
{
    public class ShopUpdateParameter
    {
        [JsonProperty(PropertyName = "name", Order = 1)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", Order = 2)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "category", Order = 3)]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "status", Order = 4)]
        public ShopStatus? Status { get; set; }

        public virtual void Validate()
        {
            ValidateName();
            ValidateDescription();
            ValidateCategory();
            ValidateStatus();
        }

        public void ValidateName()
        {
            Validator.ValidateName("name", Name);
        }

        public void ValidateDescription()
        {
            Validator.ValidateString("description", Description);
        }

        public void ValidateCategory()
        {
            Validator.ValidateString("category", Category);
        }

        public void ValidateStatus()
        {
            Validator.RequiredNotNull(Status);
            if (!Status.HasValue)
            {
                throw new InvalidParameterException("Invalid status.");
            }
        }
    }
}
