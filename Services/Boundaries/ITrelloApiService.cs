using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
    public interface ITrelloApiService
    {
        Task<T> GetDataAsync<T>(string url, Dictionary<string, string> headers = null);
        Task DeleteDataAsync(string url, Dictionary<string, string> headers = null);
        Task PostDataAsync(string url, object data = null, Dictionary<string, string> headers = null);
        Task PutDataAsync(string url, object data = null, Dictionary<string, string> headers = null);
    }
}
