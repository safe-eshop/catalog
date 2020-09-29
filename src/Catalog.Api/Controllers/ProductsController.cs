﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Framework.Requests;
using Catalog.Api.Framework.Responses;
using Catalog.Api.Framework.Responses.Mappers;
using Catalog.Application.Queries.Search;
using Catalog.Application.UseCases.Catalog;
using Catalog.Application.UseCases.Search;
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
            return result.Match<ActionResult<ProductResponse>>(res => Ok(res.ToResponse()), () => NotFound());
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductByIds([FromQuery]IEnumerable<int> ids, [FromQuery] int? shopId,
            [FromServices] GetCatalogByIdUseCase getCatalogById)
        {
            var result = await getCatalogById.Execute(ids.Select(x => new ProductId(x)).ToList(), ShopId.Create(shopId)).ToListAsync();
            
            if (result.Any())
            {
                return Ok(result.Select(x => x.ToResponse()).ToList());
            }

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<ProductListResponse>> SearchProducts([FromBody] FilterProductsRequest request,
            [FromServices] BrowseProductsUseCase useCase)
        {
            var result = await useCase.Execute(new FilterProductsQuery(request.ShopNumber, request.PageSize,
                request.Page,
                request.CategoryId, request.MaxPrice, request.MinPrice, request.MinRating, request.SortOrder));
            return result.Match<ActionResult<ProductListResponse>>(res => Ok(res), () => NoContent());
        }
    }
}