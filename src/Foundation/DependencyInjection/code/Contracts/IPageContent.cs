using Sitecore.Data;

namespace Foundation.DependencyInjection.Contracts
{
    public interface IPageContent
    {
        ID ID { get; set; }
        ID Title { get; set; }
        ID Body { get; set; }
    }
}