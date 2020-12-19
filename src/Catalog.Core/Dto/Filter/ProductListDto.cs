using System.Collections.Generic;
using Catalog.Core.Dto.Common;
using Catalog.Core.Model;

namespace Catalog.Core.Dto.Filter
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
    
    public class FilteredProductList
    {
        public IList<ProductId> ProductsIds { get; }
        public int TotalItems { get; }
        public int TotalPages { get; }

        public FilteredProductList(IList<ProductId> products, int totalItems, int totalPages)
        {
            ProductsIds = products;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }
    }
}