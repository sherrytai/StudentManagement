using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        protected SchoolContext db;

        public BaseController(SchoolContext schoolContext)
        {
            db = schoolContext;
            db.Database.EnsureCreated();
        }
    }
}
