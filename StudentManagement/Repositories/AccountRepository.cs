using StudentManagement.Exceptions;
using StudentManagement.Models;
using StudentManagement.Parameters;
using StudentManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Repositories
{
    public class AccountRepository : BaseRepository
    {
        public AccountRepository(SchoolContext schoolContext) 
            : base(schoolContext)
        { 
        }

        public Account Add(AccountParameter accountParameter)
        {
            Validator.RequiredNotNull(accountParameter);
            accountParameter.Validate();

            var conflictedAccount = db.Accounts.FirstOrDefault(x => x.Name == accountParameter.Username || x.Email == accountParameter.Email);
            if (conflictedAccount != null)
            {
                var message = conflictedAccount.Name == accountParameter.Username ? "username" : "email";
                message += " conflicts.";

                throw new ConflictException(message);
            }

            var account = db.Accounts.Add(new Account(accountParameter));
            db.SaveChanges();
            return account.Entity;
        }

        public void Update(int id, AccountParameter accountParameter)
        {
            Validator.RequiredNotNull(accountParameter);
            var localAccount = GetAccountById(id);
            var hasModified = false;
            if (!string.IsNullOrWhiteSpace(accountParameter.Username) && accountParameter.Username != localAccount.Name)
            {
                accountParameter.ValidateUsername();
                if (ContainsByUsername(accountParameter.Username))
                {
                    throw new ConflictException("username conflicts.");
                }

                localAccount.Name = accountParameter.Username;
                hasModified = true;
            }

            if (!string.IsNullOrWhiteSpace(accountParameter.Email) && accountParameter.Email != localAccount.Email)
            {
                accountParameter.ValidateEmail();
                if (ContainsByEmail(accountParameter.Email))
                {
                    throw new ConflictException("username conflicts.");
                }

                localAccount.Email = accountParameter.Email;
                hasModified = true;
            }

            if (!string.IsNullOrWhiteSpace(accountParameter.Password) && accountParameter.Password != localAccount.Password)
            {
                accountParameter.ValidatePassword();

                localAccount.Password = accountParameter.GetCryptedPassword();
                hasModified = true;
            }

            if (!hasModified)
            {
                throw new InvalidParameterException("Can't find valid change.");
            }

            db.SaveChanges();
        }

        public bool ContainsByUsername(string username)
        {
            Validator.RequiredNotNull(username);
            return db.Accounts.Any(x => x.Name == username);
        }

        public bool ContainsByEmail(string email)
        {
            Validator.ValidateString(nameof(email), email);
            return db.Accounts.Any(x => x.Email == email);
        }

        public Account GetAccountByUsername(string username)
        {
            Validator.ValidateString(nameof(username), username);
            var account = db.Accounts.FirstOrDefault(x => x.Name == username);
            if (account == null)
            {
                throw new NotFoundException($"Can't find account {username}.");
            }

            return account;
        }

        public Account GetAccountByEmail(string email)
        {
            Validator.ValidateString(nameof(email), email);
            var account = db.Accounts.FirstOrDefault(x => x.Email == email);
            if (account == null)
            {
                throw new NotFoundException($"Can't find account {email}.");
            }

            return account;
        }

        public Account GetAccountById(int id)
        {
            var account = db.Accounts.FirstOrDefault(x => x.Id == id);
            if (account == null)
            {
                throw new NotFoundException($"Can't find account {id}.");
            }

            return account;
        }

        public IEnumerable<Account> GetAccounts(int offset, int limit)
        {
            Validator.ValidateOffsetAndLimit(offset, limit);
            var size = db.Accounts.Count();
            Validator.ValidateOffsetAndLimitWithSize(offset, limit, size);
            return db.Accounts.Skip(offset).Take(limit);
        }

        public IEnumerable<Shop> GetAccountShops(int accountId, int offset, int limit)
        {
            Validator.ValidateOffsetAndLimit(offset, limit);
            var account = GetAccountById(accountId);
            var size = account.Shops.Count();
            Validator.ValidateOffsetAndLimitWithSize(offset, limit, size);

            return account.Shops.Skip(offset).Take(limit);
        }

        public void Delete(int id)
        {
            var account = GetAccountById(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
        }
    }
}
