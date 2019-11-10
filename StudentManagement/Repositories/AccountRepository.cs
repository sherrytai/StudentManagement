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

        public bool ContainsByUsername(string username)
        {
            Validator.RequiredNotNull(username);
            var account = db.Accounts.FirstOrDefault(x => x.Name == username);
            return account != null;
        }

        public bool ContainsByEmail(string email)
        {
            Validator.RequiredNotNull(email);
            var account = db.Accounts.FirstOrDefault(x => x.Email == email);
            return account != null;
        }

        public Account GetAccountByEmail(string email)
        {
            Validator.RequiredNotNull(email);
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

            return db.Accounts.Skip(offset).Take(limit);
        }

        public void Delete(int id)
        {
            var account = GetAccountById(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
        }
    }
}
