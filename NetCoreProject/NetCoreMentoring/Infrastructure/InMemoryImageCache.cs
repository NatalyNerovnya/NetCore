using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace NetCoreMentoring.Infrastructure
{
    public class InMemoryImageCache : IImageCache
    {
        private readonly string _cacheImagePath;
        private readonly int _maxCacheNumber;
        private readonly int _cacheValidTimeMilliseconds;
        private static readonly object LockReadObject = new object();
        private static readonly object LockWriteObject = new object();

        public InMemoryImageCache(IConfiguration configuration)
        {
            _cacheImagePath = configuration.GetValue<string>("CacheImageFolder");
            _maxCacheNumber = configuration.GetValue<int>("MaxCacheNumber");
            _cacheValidTimeMilliseconds = configuration.GetValue<int>("CacheExpirationTime");
            
            if (!Directory.Exists(_cacheImagePath))
            {
                Directory.CreateDirectory(_cacheImagePath);
            }
        }

        public void Add(MemoryStream imageStream, string key)
        {
            var filesNumber = Directory.EnumerateFiles(_cacheImagePath).Count();
            if (filesNumber < _maxCacheNumber)
            {
                var filePath = GetFilePath(key);

                lock (LockWriteObject)
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        fileStream.Write(imageStream.ToArray());
                    } 
                }
            }
        }

        public Stream Get(string key)
        {
            var filePath = GetFilePath(key);
            if (!File.Exists(filePath))
            {
                return null;
            }

            if (!IsCachedFileValid(filePath))
            {
                RemoveFile(filePath);
                return null;
            }

            var stream = new MemoryStream();

            lock (LockReadObject)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fileStream.CopyTo(stream);
                }
            }

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        private string GetFilePath(string key)
        {
            return Path.Combine(_cacheImagePath, key) + ".bmp";
        }

        private bool IsCachedFileValid(string filePath)
        {
            return (DateTime.UtcNow - File.GetCreationTimeUtc(filePath)).TotalMilliseconds < _cacheValidTimeMilliseconds;
        }

        private void RemoveFile(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            fileInfo.Delete();
        }
    }
}
