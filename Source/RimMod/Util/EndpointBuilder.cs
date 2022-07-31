using RimMod;

namespace RimSpectorMod
{
    internal class EndpointBuilder
    {
        private readonly Settings _settings;

        public string ConfiguredEndpoint => $"{_settings._baseUrl}/{_settings._id}";

        public EndpointBuilder(Settings settings)
        {
            _settings = settings;
        }
    }
}
