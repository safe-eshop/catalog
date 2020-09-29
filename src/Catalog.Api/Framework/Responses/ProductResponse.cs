using System;
using System.Collections.Generic;
using Catalog.Application.Dto.Common;

namespace Catalog.Api.Framework.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public ProductDescriptionDto? Description { get; set; }
        public ProductDetailsDto? Details { get; set; }
        public PriceDto? Price { get; set; }
        public IEnumerable<string>? Tags { get; set; }
    }
}