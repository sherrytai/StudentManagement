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
            var shop = account.Shops?.FirstOrDefault(x => x.Name == shopParameter.Name);
            if (shop != null)
            {
                throw new ConflictException("Shop name conflicts.");
            }

            account.Shops.Add(new Shop(shopParameter));
            db.SaveChanges();

            return account.Shops.First(x => x.Name == shopParameter.Name);
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

        public void Delete(int id)
        {
            var shop = GetShopById(id);
            db.Shops.Remove(shop);

            db.SaveChanges();
        }
    }
}
