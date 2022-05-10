using System.Linq;
using Foundation.SitecoreExtensions.Extensions;
using Learn.Foundation.Blogs.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.Tasks;

namespace Learn.Foundation.Blogs.Tasks 
{
    public class BlogIngestionsTask
    {
        public void CreateItem(Item[] itemArray, CommandItem command, ScheduleItem schedule)
        {
            if (itemArray.All(x => x.TemplateID != Templates.BlogAPIConfigurations.ID))
            {
                Log.Error("BlogIngestionsTask -> must have blogs api configuration item", this);
                return;
            }

            Item configItem = itemArray.First(x => x.TemplateID == Templates.BlogAPIConfigurations.ID);

            Item template = configItem.TargetItem(Templates.BlogAPIConfigurations.Fields.TemplatePath);
            Item destination = configItem.TargetItem(Templates.BlogAPIConfigurations.Fields.Parent);
            string url = configItem.Fields[Templates.BlogAPIConfigurations.Fields.ConnectingUrl].Value;

            var blogService = ServiceLocator.ServiceProvider.GetService<IBlogAPIService>();

            var blogs = blogService.GetBlogsAsync(url).GetAwaiter().GetResult();
            if (blogs == null)
                return;

            foreach (var blog in blogs)
            {
                destination.AddChild(blog.Name, template, item =>
                {
                    item.Fields[Templates.Blog.Fields.Title].Value = blog.Title;
                    item.Fields[Templates.Blog.Fields.Image].Value = blog.Image;
                    item.Fields[Templates.Blog.Fields.Description].Value = blog.Description;
                    item.Fields[Templates.Blog.Fields.Name].Value = blog.Name;
                    item.Fields[Templates.Blog.Fields.Author].Value = blog.Author;
                });
            }
        }
    }
}