﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Parameters;
using StudentManagement.Results;
using StudentManagement.Utils;
using StudentManagement.Models;
using StudentManagement.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagement.Controllers
{
    [Route("api/v1/[controller]")]
    public class AccountsController : BaseController
    {
        public AccountsController(Repositories.Repositories repositories) : base(repositories)
        {
        }

        // TODO need Admin permission
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<AccountResult> Get(int offset = 0, int limit = 10)
        {
            var a = accountRepository.GetAccounts(offset, limit);
            var b = a.Select(x => new AccountResult(x));
            var c = b.ToList();
            return c;

            //accountRepository.GetAccounts(offset, limit).Select(x=> new AccountResult(x));
        }

        // TODO check permission
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public AccountResult Get(int id)
        {
            var account = accountRepository.GetAccountById(id);

            return new AccountResult(account);
        }

        [HttpGet("{accountId}/shops")]
        public IEnumerable<ShopResult> GetAccountShops(int accountId, int offset = 0, int limit = 10)
        {
            var shops = accountRepository.GetAccountShops(accountId, offset, limit);

            return shops.Select(x => new ShopResult(x));
        }

        // POST api/<controller>
        [HttpPost]
        public CreatedResult Post([FromBody]AccountParameter accountParameter)
        {
            var account = accountRepository.Add(accountParameter);

            var accountResult = new AccountResult(account);
            return Created($"api/v1/accounts/{accountResult.Id}", accountResult);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]AccountParameter accountParameter)
        {
            accountRepository.Update(id, accountParameter);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            accountRepository.Delete(id);
        }

        [HttpPost("login")]
        public AccountResult Login([FromBody]AccountParameter accountParameter)
        {
            Validator.RequiredNotNull(accountParameter);
            var key = !string.IsNullOrWhiteSpace(accountParameter.Email) ? accountParameter.Email : accountParameter.Username;

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidParameterException("Invalid username or email.");
            }

            var account = accountRepository.GetAccountByEmailOrUsername(key);

            if (account == null)
            {
                throw new InvalidParameterException("Invalid username or email.");
            }

            accountParameter.ValidatePassword();
            if (accountParameter.GetCryptedPassword() != account.CryptedPassword)
            {
                throw new InvalidParameterException("Wrong password.");
            }

            return new AccountResult(account);
        }
    }
}
