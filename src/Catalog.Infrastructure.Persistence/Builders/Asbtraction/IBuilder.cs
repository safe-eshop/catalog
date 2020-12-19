namespace Catalog.Infrastructure.Persistence.Builders.Asbtraction
{
    public interface IBuilder<T>
    {
        T Build();
    }
}