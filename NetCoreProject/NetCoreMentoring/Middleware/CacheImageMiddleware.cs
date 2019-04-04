using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using NetCoreMentoring.Infrastructure;

namespace NetCoreMentoring.Middleware
{
    public class CacheImageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IImageCache _cache;
        private const string ValidImageType = "image/bmp";

        public CacheImageMiddleware(RequestDelegate requestDelegate, IImageCache cache)
        {
            _next = requestDelegate;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var responseStream = httpContext.Response.Body;
            var imageKey = BuildBmpFileName(httpContext.Request.Path);
            //even if request doesn't contain image I'm still trying to grab the image from cache :(
            var cachedImage = _cache.Get(imageKey);

            if (cachedImage != null)
            {
                await cachedImage.CopyToAsync(responseStream);

                return;
            }

            using(var stream = new MemoryStream())
            {
                httpContext.Response.Body = stream;

                await _next(httpContext);

                var isValidImageType = httpContext.Response.ContentType?.Contains(ValidImageType) ?? false;
                if (isValidImageType)
                {
                    _cache.Add(stream, imageKey);
                }

                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(responseStream);
            }
        }

        private string BuildBmpFileName(PathString path)
        {
            return path.ToString().Replace("/", string.Empty);
        }
    }
}
