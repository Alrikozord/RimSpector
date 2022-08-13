using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RimSpectorApi.Contracts;

namespace RimSpectorApi.Pages
{
    public class MetaModel : PageModel
    {
        private readonly ILogger<MetaModel> _logger;
        private readonly Service _service;

        public WorldPayload World { get; private set; }
        public StorytellerPayload Storyteller { get; private set; }
        public IEnumerable<MapPayload> Maps { get; private set; }
        public IEnumerable<ModPayload> Mods { get; private set; }

        public Guid ClientId { get; private set; }

        public MetaModel(ILogger<MetaModel> logger, Service service)
        {
            _logger = logger;
            _service = service;
        }

        public void OnGet(Guid clientId)
        {
            ClientId = clientId;

            _service.TryGetWorld(clientId, out var world);
            World = world;

            _service.TryGetStoryteller(clientId, out var storyteller);
            Storyteller = storyteller;

            _service.TryGetMaps(clientId, out var maps);
            Maps = maps;

            _service.TryGetMods(clientId, out var mods);
            Mods = mods;
        }

        public string GetPercent(float? value) => value.HasValue ? GetPercent(value.Value) : "-";
        public string GetPercent(float value) => value.ToString("P0");
        public string GetEnabledLabel(bool? value) => value.HasValue ? GetEnabledLabel(value.Value) : "-";
        public string GetEnabledLabel(bool value) => value ?"enabled" : "disabled";
    }
}