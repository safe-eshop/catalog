using System.Collections;
using System.Collections.Generic;
using Catalog.Application.Dto.Common;

namespace Catalog.Application.Dto.Filter
{
    public class PagedProductListDto
    {
        public IList<ProductDto> Products { get; }
        public int TotalItems { get; }
        public int TotalPages { get; }

        public PagedProductListDto(IList<ProductDto> products, int totalItems, int totalPages)
        {
            Products = products;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }
    }
}