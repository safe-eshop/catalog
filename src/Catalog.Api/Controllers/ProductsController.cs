using System;
using System.Threading.Tasks;
using Catalog.Api.Framework.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController: ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(Guid id, [FromQuery]int shopId = 1)
        {
            
            return NotFound();
        }
    }
}