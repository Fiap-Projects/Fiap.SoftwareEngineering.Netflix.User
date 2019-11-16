using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.AspNetCore.Abstractions.Hosting
{
    public interface IHostBuilder
    {
        Task<int> CreateWebHostBuilder<TStartup>(string[] args = default, string title = default)
            where TStartup : class;
    }
}
