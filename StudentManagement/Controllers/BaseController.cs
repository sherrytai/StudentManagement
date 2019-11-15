using Microsoft.AspNetCore.Mvc;
using StudentManagement.Exceptions;
using StudentManagement.Models;
using StudentManagement.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        protected Repositories.Repositories repositories;
        protected AccountRepository accountRepository;
        protected ShopRepository shopRepository;
        protected ProductRepository productRepository;

        public BaseController(Repositories.Repositories repositories)
        {
            this.repositories = repositories;
            accountRepository = repositories.AccountRepository;
            shopRepository = repositories.ShopRepository;
            productRepository = repositories.ProductRepository;
        }
    }
}
