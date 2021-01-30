﻿using System;
using System.Threading.Tasks;
using Catalog.Core.Model;
using LanguageExt;

namespace Catalog.Core.Services.Import.Abstractions
{
    public interface IProductImportStatus
    {
        Product Product { get; }
    }

    public record ProductImported(Product Product ) : IProductImportStatus;

    public record ProductImportFailed(Product Product, Exception Exception) : IProductImportStatus;
    
    public interface IProductsImportStatusNotifier
    {
        Task Notify(Either<ProductImportFailed, ProductImported> importStatus);
    }
}