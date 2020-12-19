namespace Catalog.Application.Queries.Search
{
    public class FilterProductsQuery
    {
        public int ShopNumber { get; }
        public int PageSize { get; }
        public int Page { get; }
        public int? CategoryId { get; }
        public double? MaxPrice { get; }
        public double? MinPrice { get; }
        public double? MinRating { get; }
        public string SortOrder { get; }

        public FilterProductsQuery(int shopNumber, int pageSize, int page, int? categoryId, double? maxPrice,
            double? minPrice, double? minRating, string sortOrder)
        {
            ShopNumber = shopNumber;
            PageSize = pageSize;
            Page = page;
            CategoryId = categoryId;
            MaxPrice = maxPrice;
            MinPrice = minPrice;
            MinRating = minRating;
            SortOrder = sortOrder;
        }
    }
}