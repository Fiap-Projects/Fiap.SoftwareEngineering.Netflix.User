namespace Fiap.SoftwareEngineering.Netflix.Api.Routing
{
    public class RoutePattern
    {
        public const string DefaultRoute = @"[controller]";
        public const string VersionedRoute = @"v{version:apiVersion}/[controller]";
    }
}
