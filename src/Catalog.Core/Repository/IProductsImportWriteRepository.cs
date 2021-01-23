using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Catalog.Core.Model;
using Catalog.Core.Services.Import;
using LanguageExt;

namespace Catalog.Core.Repository
{
    public interface IProductsImportWriteRepository
    {
        Task Store(ChannelReader<Product> products, ChannelWriter<Either<ProductImported, ProductImportFailed>> writer,
            CancellationToken cancellationToken = default);
    }
}