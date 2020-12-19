using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Core.Model
{
    public record ProductId(int Value);

    public record ShopId(int Value)
    {
        public static ShopId Create(int? value) => value.HasValue ? new ShopId(value.Value) : new ShopId(1);
    }

    public record ProductDescription (string Name, string Brand, string Description);

    public record ProductDetails (double Weight, string WeightUnits, string Picture, string Color);

    public record Tag(string Value)
    {
        public static implicit operator string(Tag tag) => tag.Value;
    }

    public record Tags(IEnumerable<Tag> Value)
    {
        public IEnumerable<string> GetTags() => Value.Select(tag => tag.Value).ToList();
    }

    public record Price(decimal Regular, decimal? Promotional)
    {
        public static Price Create(decimal regular, decimal? promotional)
        {
            return (regular, promotional) switch
            {
                var (reg, prom) when prom > reg => throw new ArgumentException(
                    "Regular price is smaller than promotional"),
                var (reg, prom) => new Price(reg, prom)
            };
        }
    }

    public record Product(ProductId Id, ShopId ShopId, string Slug, ProductDescription Description, Price Price,
        ProductDetails Details, Tags Tags)
    {
        public static string GenerateSlug(ProductId productId, ShopId shopId) => $"{productId.Value}_{shopId.Value}";
    }
}