using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Foundation.DependencyInjection;
using Learn.Foundation.Blogs.Model;
using Sitecore.Diagnostics;

namespace Learn.Foundation.Blogs.Services
{
    public interface IBlogAPIService
    {
        Task<List<Blog>> GetBlogsAsync(string path);
    }

    [Service(ServiceType = typeof(IBlogAPIService), Lifetime = Lifetime.Scoped)]
    public class BlogAPIService : IBlogAPIService, IDisposable
    {
        private ConcurrentDictionary<string, List<Blog>> _cache = new ConcurrentDictionary<string, List<Blog>>();

        private HttpClient _api { get; set; }

        public BlogAPIService()
        {
            _api = new HttpClient();
        }

        public async Task<List<Blog>> GetBlogsAsync(string url)
        {
            if (_cache.TryGetValue(url, out var blogs))
            {
                return blogs;
            }

            try
            {
                _api.DefaultRequestHeaders.Accept.Clear();
                _api.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await _api.GetAsync(url);
                var body = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    blogs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Blog>>(body);
                    if (!_cache.TryAdd(url, blogs))
                        Log.Warn($"BlogAPIService -> GetBlogsAsync -> cann't add blogs in cache key:{url}", this);
                    return blogs;
                }

                Log.Error($"BlogAPIService -> GetBlogsAsync -> StatusCode:{response.StatusCode} Body:{body}", this);
                return null;
            }
            catch (Exception e)
            {
                Log.Error($"BlogAPIService -> GetBlogsAsync -> {e.Message}", this);
                return null;
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _api?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}