using System;
using System.Threading.Tasks;
using Catalog.Api.Framework.Requests;
using Catalog.Api.Framework.Responses;
using Catalog.Application.UseCases.Catalog;
using Catalog.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(int id, [FromQuery] int? shopId,
            [FromServices] GetCatalogByIdUseCase getCatalogById)
        {
            var result = await getCatalogById.Execute(new ProductId(id), ShopId.Create(shopId));
            return result.Match<ActionResult<ProductResponse>>(res => Ok(res), () => NotFound());
        }

        [HttpGet("search")]
        public async Task<ActionResult<ProductListResponse>> SearchProducts([FromBody]FilterProductsRequest request)
        {
            return NoContent();
        }
    }
}