using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Catalog.Core.Model;
using LanguageExt;

namespace Catalog.Core.Repository
{
    public interface IProductsImportWriteRepository
    {
        Task<Either<Exception, Unit>> Store(ChannelReader<Product> products);
    }
}