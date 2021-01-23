using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Catalog.Core.Repository;
using Catalog.Core.Services.Import;
using LanguageExt;
using Microsoft.Extensions.Logging;

namespace Catalog.Core.UseCases.Import
{
    public class FullImportProductsTodayUseCase
    {
        private readonly IProductsImportSource _source;
        private readonly IProductWriter _writer;
        private readonly IProductsImportStatusNotifier _notifier;
        private readonly ILogger<FullImportProductsTodayUseCase> _logger;

        public FullImportProductsTodayUseCase(IProductsImportSource source,
            IProductWriter writer,
            ILogger<FullImportProductsTodayUseCase> logger, IProductsImportStatusNotifier notifier)
        {
            _source = source;
            _writer = writer;
            _logger = logger;
            _notifier = notifier;
        }

        public async Task Execute(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Start Import Today");
            
            var res = _source
                .ProduceProducts(cancellationToken);

            var resultsReader = _writer.Write(res, cancellationToken);

            await Notify(resultsReader, cancellationToken);
            
            _logger.LogInformation("Finish Import Today");
        }

        private async Task Notify(ChannelReader<Either<ProductImported, ProductImportFailed>> reader, CancellationToken cancellationToken)
        {
            await foreach (var res in reader.ReadAllAsync(cancellationToken))
            {
                await _notifier.Notify(res);
            }
        }
    }
}