using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Learn.Foundation.Blogs.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogUnitTest
{
    [TestClass]
    public class BlogAPIServiceTest
    {
        [TestMethod]
        public async Task GetBlogsAsync()
        {
            using (var api = new BlogAPIService())
            {
                var url = ConfigurationManager.AppSettings["BlogsApiUrl"];
                var blogs = await api.GetBlogsAsync(url);
                Assert.IsTrue(blogs.Any(), "Blogs Api return empty result");

                var blogs1 = await api.GetBlogsAsync(url);
                Assert.AreEqual(blogs1, blogs);
            }
        }
    }
}
