using Serilog;

namespace Fiap.SoftwareEngineering.Netflix.Logging.Abstractions
{
    public interface ILoggerConfiguration
    {
        ILogger SetUp(string environmentVariable);
    }
}
