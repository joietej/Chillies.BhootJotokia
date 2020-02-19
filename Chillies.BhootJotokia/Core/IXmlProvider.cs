using System.Threading.Tasks;

namespace Chillies.BhootJotokia.Core
{
    public interface IXmlProvider
    {
        Task<T> LoadXmlAsAsync<T>(string path, string? root = null);
    }
}