using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Parameters;
using StudentManagement.Results;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagement.Controllers
{
    [Route("api/v1/[controller]")]
    public class ShopsController : BaseController
    {
        public ShopsController(Repositories.Repositories repositories) : base(repositories)
        {
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<ShopResult> Get(int offset = 0, int limit = 10)
        {
            return shopRepository.GetShops(offset, limit).Select(x => new ShopResult(x));
        }

        [HttpGet("{shopId}/products")]
        public IEnumerable<ProductResult> GetShopProducts(int shopId, int offset = 0, int limit = 10)
        {
            var products = shopRepository.GetShopProducts(shopId, offset, limit);

            return products.Select(x => new ProductResult(x));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ShopResult Get(int id)
        {
            return new ShopResult(shopRepository.GetShopById(id));
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]ShopParameter shopParameter)
        {
            var shop = shopRepository.Add(shopParameter);
            return Created($"api/v1/shops/{shop.Id}", new ShopResult(shop));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ShopUpdateParameter shopParameter)
        {
            shopRepository.Update(id, shopParameter);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            shopRepository.Delete(id);
        }
    }
}
