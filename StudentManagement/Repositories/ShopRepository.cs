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
    public class ShopRepository : BaseRepository
    {
        AccountRepository accountRepository;

        public ShopRepository(SchoolContext schoolContext, AccountRepository accountRepository)
            : base(schoolContext)
        {
            this.accountRepository = accountRepository;
        }

        public Shop Add(ShopParameter shopParameter)
        {
            Validator.RequiredNotNull(shopParameter);
            shopParameter.Validate();

            var account = accountRepository.GetAccountById(shopParameter.AccountId);
            var hasConflict = db.Shops.Any(x => x.Name == shopParameter.Name);
            if (hasConflict)
            {
                throw new ConflictException("Shop name conflicts.");
            }

            account.Shops.Add(new Shop(shopParameter));
            db.SaveChanges();

            return db.Shops.First(x => x.Name == shopParameter.Name);
        }

        public Shop GetShopById(int id)
        {
            var shop = db.Shops.FirstOrDefault(x => x.Id == id);
            if (shop == null)
            {
                throw new NotFoundException($"Can't find shop {id}.");
            }

            return shop;
        }

        public IEnumerable<Shop> GetShops(int offset, int limit)
        {
            Validator.ValidateOffsetAndLimit(offset, limit);
            var size = db.Shops.Count();
            Validator.ValidateOffsetAndLimitWithSize(offset, limit, size);

            return db.Shops.Skip(offset).Take(limit);
        }

        public bool ContainsByName(string name)
        {
            Validator.ValidateName(nameof(name), name);
            return db.Shops.Any(s => s.Name == name);
        }

        public void Update(int id, ShopUpdateParameter shopParameter)
        {
            Validator.RequiredNotNull(shopParameter);
            var shop = GetShopById(id);
            var hasModified = false;
            if (!string.IsNullOrWhiteSpace(shopParameter.Name) && shop.Name != shopParameter.Name)
            {
                shopParameter.ValidateName();
                if (ContainsByName(shopParameter.Name))
                {
                    throw new ConflictException("shop name conflicts.");
                }

                shop.Name = shopParameter.Name;
                hasModified = true;
            }

            if (!string.IsNullOrWhiteSpace(shopParameter.Description) && shop.Description != shopParameter.Description)
            {
                shopParameter.ValidateDescription();
                shop.Description = shopParameter.Description;
                hasModified = true;
            }

            if (!string.IsNullOrWhiteSpace(shopParameter.Category) && shop.Category != shopParameter.Category)
            {
                shopParameter.ValidateCategory();
                shop.Category = shopParameter.Category;
                hasModified = true;
            }

            if (shopParameter.Status != null && shopParameter.Status.HasValue && shop.Status != shopParameter.Status.Value)
            {
                shop.Status = shopParameter.Status.Value;
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
            var shop = GetShopById(id);
            db.Shops.Remove(shop);

            db.SaveChanges();
        }
    }
}
