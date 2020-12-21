using System.Collections.Generic;
using Catalog.Core.Model;

namespace Catalog.Core.Repository
{
    public record SearchParameters(string Query);
    
    public interface IProductSearcher
    {
        IAsyncEnumerable<Product> Search(SearchParameters parameters);
    }
}