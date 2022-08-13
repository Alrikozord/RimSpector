using RimMod;

namespace RimSpectorMod
{
    internal class EndpointBuilder
    {
        private readonly Settings _settings;

        public string ConfiguredApiEndpoint => $"{_settings._baseUrl}{_settings._apiPath}/{_settings._id}";
        public string ConfiguredSiteEndpoint => $"{_settings._baseUrl}/{_settings._id}";

        public EndpointBuilder(Settings settings)
        {
            _settings = settings;
        }
    }
}
