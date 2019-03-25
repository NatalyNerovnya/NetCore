using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Configuration;

namespace NetCoreMentoring.Infrastructure
{
    public class InMemoryImageCache : IImageCache
    {
        private readonly Timer _timer;
        private readonly string _cacheImagePath;
        private readonly int _maxCacheNumber;

        public InMemoryImageCache(IConfiguration configuration)
        {
            _cacheImagePath = configuration.GetValue<string>("CacheImageFolder");
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

        public async Task AddAsync(MemoryStream imageStream, string key)
        {
            ResetTimer();

            var filesNumber = Directory.EnumerateFiles(_cacheImagePath).Count();
            if (filesNumber < _maxCacheNumber)
            {
                var filePath = GetFilePath(key);
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await fileStream.WriteAsync(imageStream.ToArray());
                }
            }
        }

        public async Task<Stream> GetAsync(string key)
        {
            ResetTimer();

            var filePath = GetFilePath(key);
            if (!File.Exists(filePath))
            {
                return null;
            }

            var stream = new MemoryStream();

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await fileStream.CopyToAsync(stream);
            }

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        private string GetFilePath(string key)
        {
            return Path.Combine(_cacheImagePath, key) + ".bmp";
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
