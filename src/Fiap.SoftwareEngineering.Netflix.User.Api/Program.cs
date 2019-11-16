using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.AspNetCore.Hosting;

namespace Fiap.SoftwareEngineering.Netflix.User.Api
{
    public class Program
    {
        private static async Task<int> Main(string[] args) => await HostBuilder.BuildWebHost<Startup>(args);
    }
}
