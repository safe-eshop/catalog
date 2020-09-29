using System;
using System.Collections.Generic;
using Catalog.Domain.Model;

namespace Catalog.Application.Dto.Common
{
    public class ProductDto
    {
        public ProductId Id { get; }
        public ShopId ShopId { get;  }
        public string Slug { get; }
        public ProductDescriptionDto Description { get; }
        public ProductDetailsDto Details { get; }
        public PriceDto Price { get; }
        public IEnumerable<string> Tags { get; }

        public ProductDto(ProductId id, ShopId shopId, string slug, ProductDescriptionDto description, ProductDetailsDto details, PriceDto price, IEnumerable<string> tags)
        {
            Id = id;
            ShopId = shopId;
            Slug = slug;
            Description = description;
            Details = details;
            Price = price;
            Tags = tags;
        }
    }

    public class ProductDetailsDto
    {
        public double Weight { get;  }
        public string WeightUnits { get; }
        public string Picture { get;  }
        public string Color { get;  }

        public ProductDetailsDto(double weight, string weightUnits, string picture, string color)
        {
            Weight = weight;
            WeightUnits = weightUnits;
            Picture = picture;
            Color = color;
        }
    }

    public class ProductDescriptionDto
    {
        public string Name { get; }
        public string Brand { get; }
        public string Description { get; }

        public ProductDescriptionDto(string name, string brand, string description)
        {
            Name = name;
            Brand = brand;
            Description = description;
        }
    }

    public class PriceDto
    {
        public decimal Regular { get;}
        public decimal? Promotional { get; }

        public PriceDto(decimal regular, decimal? promotional)
        {
            Regular = regular;
            Promotional = promotional;
        }
    }
}