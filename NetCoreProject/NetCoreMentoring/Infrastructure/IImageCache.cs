using System.IO;
namespace NetCoreMentoring.Infrastructure
{
    public interface IImageCache
    {
        void Add(MemoryStream imageStream, string key);
        Stream Get(string key);
    }
}
