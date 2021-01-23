using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Core.Repository;
using Microsoft.Extensions.Logging;

namespace Catalog.Core.UseCases.Import
{
    public class FullImportProductsTodayUseCase
    {
        private IProductsImportSource _source;
        private IEnumerable<IProductsImportWriteRepository> _importWriteRepositories;
        private ILogger<FullImportProductsTodayUseCase> _logger;

        public FullImportProductsTodayUseCase(IProductsImportSource source,
            IEnumerable<IProductsImportWriteRepository> importWriteRepository,
            ILogger<FullImportProductsTodayUseCase> logger)
        {
            _source = source;
            _importWriteRepositories = importWriteRepository;
            _logger = logger;
        }

        public async Task Execute(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Start Import Today");
            var res = _source
                .ProduceProductsToImport(cancellationToken);

            var consumers = _importWriteRepositories.Select(async repo => await repo.Store(res, cancellationToken));

            await Task.WhenAll(consumers);

            _logger.LogInformation("Finish Import Today");
        }
    }
}