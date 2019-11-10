using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Parameters;
using StudentManagement.Models;
using StudentManagement.Exceptions;
using StudentManagement.Results;
using StudentManagement.Repositories;
using StudentManagement.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagement.Controllers
{
    [Route("api/v0/[controller]")]
    public class AccountsController : BaseController
    {
        private AccountRepository accountRepository;
        private SchoolContext db;

        public AccountsController(
                    AccountRepository accountRepository,
                    SchoolContext schoolContext)
        {
            this.accountRepository = accountRepository;
            this.db = schoolContext;
        }

        // TODO need Admin permission
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<AccountResult> Get(int offset = 0, int limit = 10)
        {
            return accountRepository.GetAccounts(offset, limit).Select(x => new AccountResult(x));
        }

        // TODO check permission
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public AccountResult Get(int id)
        {
            var account = accountRepository.GetAccountById(id);

            return new AccountResult(account);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]AccountParameter accountParameter)
        {
            var account = accountRepository.Add(accountParameter);

            var accountResult = new AccountResult(accountRepository.GetAccountByEmail(account.Email));
            return new CreatedResult($"api/accounts/{accountResult.Id}", accountResult);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]AccountParameter account)
        {
            Validator.RequiredNotNull(account);
            var localAccount = accountRepository.GetAccountById(id);
            var hasModified = false;
            if (!string.IsNullOrWhiteSpace(account.Username) && account.Username != localAccount.Name)
            {
                account.ValidateUsername();
                if (accountRepository.ContainsByUsername(account.Username))
                {
                    throw new ConflictException("username conflicts.");
                }

                localAccount.Name = account.Username;
                hasModified = true;
            }

            if (!string.IsNullOrWhiteSpace(account.Email) && account.Email != localAccount.Email)
            {
                account.ValidateEmail();
                if (accountRepository.ContainsByEmail(account.Email))
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
            accountRepository.Delete(id);
        }

       
    }
}
