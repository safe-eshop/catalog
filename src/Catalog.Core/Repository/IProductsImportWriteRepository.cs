using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Catalog.Core.Model;
using Catalog.Core.Services.Import;
using Catalog.Core.Services.Import.Abstractions;
using LanguageExt;

namespace Catalog.Core.Repository
{
    public interface IProductsImportWriteRepository
    {
        Task<Either<ProductImportFailed, ProductImported>> Store(Product product,
            CancellationToken cancellationToken = default);
    }
}