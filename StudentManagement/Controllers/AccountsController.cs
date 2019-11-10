using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Parameters;
using StudentManagement.Models;
using StudentManagement.Exceptions;
using StudentManagement.Results;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : BaseController
    {
        public AccountsController(SchoolContext schoolContext) : base(schoolContext)
        {
        }

        // TODO need Admin permission
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<AccountResult> Get()
        {
            return db.Accounts.Select(x => new AccountResult(x));
        }

        // TODO check permission
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public AccountResult Get(int id)
        {
            var account = GetAccountById(id);

            return new AccountResult(account);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]AccountParameter account)
        {
            RequiredNotNull(account);
            account.Validate();

            var conflictedAccount = db.Accounts.FirstOrDefault(x => x.Name == account.Username || x.Email == account.Email);
            if (conflictedAccount != null)
            {
                var message = conflictedAccount.Name == account.Username ? "username" : "email";
                message += " conflicts.";

                throw new ConflictException(message);
            }

            db.Accounts.Add(new Account(account));
            db.SaveChanges();

            var accountResult = new AccountResult(GetAccountByEmail(account.Email));
            return new CreatedResult($"api/accounts/{accountResult.Id}", accountResult);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]AccountParameter account)
        {
            RequiredNotNull(account);
            var localAccount = GetAccountById(id);
            var hasModified = false;
            if (!string.IsNullOrWhiteSpace(account.Username) && account.Username != localAccount.Name)
            {
                account.ValidateUsername();
                if (ContainsByUsername(account.Username))
                {
                    throw new ConflictException("username conflicts.");
                }

                localAccount.Name = account.Username;
                hasModified = true;
            }

            if (!string.IsNullOrWhiteSpace(account.Email) && account.Email != localAccount.Email)
            {
                account.ValidateEmail();
                if (ContainsByEmail(account.Email))
                {
                    throw new ConflictException("username conflicts.");
                }

                localAccount.Email = account.Email;
                hasModified = true;
            }

            if (!string.IsNullOrWhiteSpace(account.Password) && account.Password != localAccount.Password)
            {
                account.ValidatePassword();

                localAccount.Password = account.GetCryptedPassword();
                hasModified = true;
            }

            if (!hasModified)
            {
                throw new InvalidParameterException("Can't find valid change.");
            }

            db.SaveChanges();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var account = GetAccountById(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
        }

        private Account GetAccountById(int id)
        {
            var account = db.Accounts.FirstOrDefault(x => x.Id == id);
            if (account == null)
            {
                throw new NotFoundException($"Can't find account {id}.");
            }

            return account;
        }

        private bool ContainsByUsername(string username)
        {
            RequiredNotNull(username);
            var account = db.Accounts.FirstOrDefault(x => x.Name == username);
            return account != null;
        }

        private bool ContainsByEmail(string email)
        {
            RequiredNotNull(email);
            var account = db.Accounts.FirstOrDefault(x => x.Email == email);
            return account != null;
        }

        private Account GetAccountByEmail(string email)
        {
            RequiredNotNull(email);
            var account = db.Accounts.FirstOrDefault(x => x.Email == email);
            if (account == null)
            {
                throw new NotFoundException($"Can't find account {email}.");
            }

            return account;
        }
    }
}
