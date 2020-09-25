using System;
using Catalog.Domain.Model;

namespace Catalog.Application.Dto.Common
{
    public class ProductDto
    {
        public ProductDto(ProductId id)
        {
            Id = id;
        }

        public ProductId Id { get; }
    }
}