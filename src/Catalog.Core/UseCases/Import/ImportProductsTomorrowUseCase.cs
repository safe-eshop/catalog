﻿using System.Threading;
using System.Threading.Tasks;
using Catalog.Core.Repository;
using Microsoft.Extensions.Logging;

namespace Catalog.Core.UseCases.Import
{
    public class FullImportProductsTodayUseCase
    {
        private IProductsImportSource _source;
        private IProductsImportWriteRepository _importWriteRepository;
        private ILogger<FullImportProductsTodayUseCase> _logger;

        public FullImportProductsTodayUseCase(IProductsImportSource source,
            IProductsImportWriteRepository importWriteRepository, ILogger<FullImportProductsTodayUseCase> logger)
        {
            _source = source;
            _importWriteRepository = importWriteRepository;
            _logger = logger;
        }

        public async Task Execute(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Start Import Today");
            var res = _source
                .ProduceProductsToImport(cancellationToken);

            await _importWriteRepository.Store(res, cancellationToken);
            
            _logger.LogInformation("Finish Import Today");
        }
    }
}