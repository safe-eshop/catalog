using System;
using System.Threading.Tasks;
using Catalog.Domain.Repository;
using Open.ChannelExtensions;

namespace Catalog.Application.UseCases.Import
{
    public class FullImportProductsTomorrowUseCase
    {
        private IProductsImportSource _source;
        private IProductsImportWriteRepository _importWriteRepository;

        public FullImportProductsTomorrowUseCase(IProductsImportSource source,
            IProductsImportWriteRepository importWriteRepository)
        {
            _source = source;
            _importWriteRepository = importWriteRepository;
        }

        public async Task Execute()
        {
            var res = _source
                .GetProductsToImport();

            await _importWriteRepository.Store(res);
        }
    }
}