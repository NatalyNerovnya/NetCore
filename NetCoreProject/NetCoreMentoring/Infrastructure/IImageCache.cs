using System.IO;
using System.Threading.Tasks;

namespace NetCoreMentoring.Infrastructure
{
    public interface IImageCache
    {
        Task AddAsync(MemoryStream imageStream, string key);
        Task<Stream> GetAsync(string key);
    }
}
