using Sitecore.Mvc.Presentation;

namespace Foundation.SitecoreExtensions.Repositories
{
    public interface IRenderingPropertiesRepository
  {
    T Get<T>(Rendering rendering);
  }
}