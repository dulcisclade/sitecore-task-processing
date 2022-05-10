using Sitecore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.DependencyInjection
{
    public static class DependencyInjector
    {
        public static T GetImplementation<T>()
        {
            return ServiceLocator.ServiceProvider.GetService<T>();
        }
    }
}