using System.Threading;
using System.Threading.Channels;
using Catalog.Core.Model;
using LanguageExt;

namespace Catalog.Core.Services.Import.Abstractions
{
    public interface IProductWriter
    {
        ChannelReader<Either<ProductImported, ProductImportFailed>> Write(ChannelReader<Product> products,
            CancellationToken cancellationToken = default);
    }
}