using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace NetCoreMentoring.Middleware
{
    public class CacheImageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        private readonly Timer _timer;
        private readonly string _cacheImagePath;
        private readonly int _maxCacheNumber;
        private const string ValidImageType = "image/bmp";

        public CacheImageMiddleware(RequestDelegate requestDelegate, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _next = requestDelegate;
            _configuration = configuration;
            _cacheImagePath = configuration.GetValue<string>("CacheImageFolder")
                ?? Path.Combine(hostingEnvironment.WebRootPath, "cache");
            _maxCacheNumber = configuration.GetValue<int>("MaxCacheNumber");

            _timer = new Timer()
            {
                Interval = configuration.GetValue<int>("CacheExpirationTime"),
                AutoReset = false
            };
            _timer.Elapsed += (s, e) => OnTimerElapsed();

            if (!Directory.Exists(_cacheImagePath))
            {
                Directory.CreateDirectory(_cacheImagePath);
            }
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var responseStream = httpContext.Response.Body;
            using(var stream = new MemoryStream())
            {
                httpContext.Response.Body = stream;
                await _next(httpContext);

                var isValidImageType = httpContext.Response.ContentType?.Contains(ValidImageType) ?? false;
                if (isValidImageType)
                {
                    ResetTimer();

                    var fileName = BuildBmpFileName(httpContext.Request.Path);
                    var filePath = Path.Combine(_cacheImagePath, fileName);

                    if (File.Exists(filePath))
                    {
                        await CopyCachedImageAsync(filePath, stream);                        
                    }
                    else
                    {
                        var filesNumber = Directory.EnumerateFiles(_cacheImagePath).Count();
                        if (filesNumber < _maxCacheNumber)
                        {
                            await AddImageToCacheAsync(filePath, stream);
                        }                        
                    }                    
                }

                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(responseStream);
            }
        }

        private string BuildBmpFileName(PathString path)
        {
            var name = path.ToString().Replace("/", string.Empty);
            return name + ".bmp";
        }

        private async Task CopyCachedImageAsync(string filePath, Stream stream)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                stream.Seek(0, SeekOrigin.Begin);
                await fileStream.CopyToAsync(stream);
            }
        }

        private async Task AddImageToCacheAsync(string filePath, Stream stream)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(fileStream);
            }
        }

        private void OnTimerElapsed()
        {
            foreach (var filePath in Directory.EnumerateFiles(_cacheImagePath))
            {
                var fileInfo = new FileInfo(filePath);
                fileInfo.Delete();
            }
        }

        private void ResetTimer()
        {
            _timer.Stop();
            _timer.Start();
        }
    }
}
