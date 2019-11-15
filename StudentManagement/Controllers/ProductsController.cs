using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Parameters;
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

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ProductResult Get(int id)
        {
            return new ProductResult(productRepository.GetProductById(id));
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]ProductParameter productParameter)
        {
            var product = productRepository.Add(productParameter);
            return Created($"api/v1/products/{product.Id}", new ProductResult(product));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ProductUpdateParameter productParameter)
        {
            productRepository.Update(id, productParameter);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            productRepository.Delete(id);
        }

    }
}