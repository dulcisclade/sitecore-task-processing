namespace Foundation.DependencyInjection.Infrastructure
{
    public interface ITypeProvider<T>
    {
        T Get();
    }
}