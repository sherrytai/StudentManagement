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
            var account = db.Accounts.FirstOrDefault(x => x.Id == id); // TODO not found
            return new AccountResult(account);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]AccountParameter account)
        {
            account.Validate();

            var conflictedAccount = db.Accounts.FirstOrDefault(x => x.Name == account.Username || x.Email == account.Email);
            if (conflictedAccount != null)
            {
                var message = conflictedAccount.Name == account.Username ? "username" : "email";
                message += "conflicts.";

                throw new ConflictException(message);
            }

            db.Accounts.Add(new Account(account));
            db.SaveChanges();
        }

        // PUT api/<controller>/5
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
