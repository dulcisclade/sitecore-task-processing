using System;
using System.Threading.Tasks;
using BlogAPI.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogAPI.Middleware
{
    public class BlogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IBlogService _blog;
        public BlogMiddleware(RequestDelegate next, IBlogService blog)
        {
            this._next = next;
            _blog = blog;
        }

        public async Task InvokeAsync(HttpContext context)
        {
           
            context.Request.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(_blog.GetList()));
            //await _next(context);
        }
    }
}