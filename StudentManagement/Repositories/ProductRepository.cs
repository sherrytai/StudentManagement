using StudentManagement.Exceptions;
using StudentManagement.Models;
using StudentManagement.Parameters;
using StudentManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace StudentManagement.Repositories
{
    public class ProductRepository : BaseRepository
    {
        ShopRepository shopRepository;

        public ProductRepository(SchoolContext schoolContext, ShopRepository shopRepository)
            : base(schoolContext)
        {
            this.shopRepository = shopRepository;  
        }

        public Product Add(ProductParameter productParameter)
        {
            Validator.RequiredNotNull(productParameter);
            productParameter.Validate();

            var shop = shopRepository.GetShopById(productParameter.ShopId);

            shop.Products.Add(new Product(productParameter));
            db.SaveChanges();

            return db.Products.First(x => x.Name == productParameter.Name);
        }

        public Product GetShopById(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new NotFoundException($"Can't find product {id}.");
            }

            return product;
        }

        public IEnumerable<Product> GetProducts(int offset, int limit)
        {
            Validator.ValidateOffsetAndLimit(offset, limit);
            var size = db.Products.Count();
            Validator.ValidateOffsetAndLimitWithSize(offset, limit, size);

            return db.Products.Skip(offset).Take(limit);
        }

        public bool ContainsByName(string name)
        {
            Validator.ValidateName(nameof(name), name);
            return db.Products.Any(s => s.Name == name);
        }

        public void Update(int id, ProductUpdateParameter productParameter)
        {
            Validator.RequiredNotNull(productParameter);
            var product = GetShopById(id);
            var hasModified = false;
            if (!string.IsNullOrWhiteSpace(productParameter.Name) && product.Name != productParameter.Name)
            {
                productParameter.ValidateName();
                if (ContainsByName(productParameter.Name))
                {
                    throw new ConflictException("product name conflicts.");
                }

                product.Name = productParameter.Name;
                hasModified = true;
            }

            if (!string.IsNullOrWhiteSpace(productParameter.Description) && product.Description != productParameter.Description)
            {
                productParameter.ValidateDescription();
                product.Description = productParameter.Description;
                hasModified = true;
            }

            if (productParameter.Status != null && productParameter.Status.HasValue && product.Status != productParameter.Status.Value)
            {
                product.Status = productParameter.Status.Value;
                hasModified = true;
            }

            if (Math.Abs(product.Price - productParameter.Price) > 0)
            {
                productParameter.ValidatePrice();
                product.Price = productParameter.Price;
                hasModified = true;
            }

            if (product.Amount != productParameter.Amount)
            {
                productParameter.ValidateAmount();
                product.Amount = productParameter.Amount;
                hasModified = true;
            }

            if (!hasModified)
            {
                throw new InvalidParameterException("Can't find valid change.");
            }

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = GetShopById(id);
            db.Products.Remove(product);

            db.SaveChanges();
        }
    }
}
