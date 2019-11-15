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
    public class ProductUpdateParameter
    {

        [JsonProperty(PropertyName = "name", Order = 1)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", Order = 2)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "status", Order = 3)]
        public ProductStatus? Status { get; set; }

        [JsonProperty(PropertyName = "price", Order = 4)]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "amount", Order = 5)]
        public int Amount { get; set; }


        public virtual void Validate()
        {
            ValidateName();
            ValidateDescription();
            ValidateStatus();
            ValidatePrice();
            ValidateAmount();
        }

        public void ValidateName()
        {
            Validator.ValidateName("name", Name);
        }

        public void ValidateDescription()
        {
            Validator.ValidateString("description", Description);
        }

        public void ValidatePrice()
        {
            Validator.RequriedGreaterThanZero("price", Price);
        }

        public void ValidateAmount()
        {
            Validator.RequriedNotLessThanZero("amount", Amount);
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
