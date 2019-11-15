using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using StudentManagement.Results;

namespace StudentManagement.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductsController : BaseController
    {

        public ProductsController(Repositories.Repositories repositories) : base(repositories)
        {
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<ProductResult> Get(int offset = 0, int limit = 10)
        {
            return productRepository.GetProducts(offset, limit).Select(x => new ProductResult(x));
        }
    }
}