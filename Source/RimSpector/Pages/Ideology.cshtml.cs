using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RimSpectorApi.Contracts;

namespace RimSpectorApi.Pages
{
    public class IdeologyModel : PageModel
    {
        private readonly ILogger<IdeologyModel> _logger;
        private readonly Service _service;

        public Guid ClientId { get; private set; }
        public int SelectedIdeoId { get; private set; }
        public IdeoPayload SelectedIdeo { get; private set; }
        public IEnumerable<PreceptPayload> PreceptsHighImpact => SelectedIdeo.Precepts.Where(p => p.Impact == "High");
        public IEnumerable<PreceptPayload> PreceptsMediumImpact => SelectedIdeo.Precepts.Where(p => p.Impact == "Medium");
        public IEnumerable<PreceptPayload> PreceptsLowImpact => SelectedIdeo.Precepts.Where(p => p.Impact == "Low");

        public IdeologyModel(ILogger<IdeologyModel> logger, Service service)
        {
            _logger = logger;
            _service = service;
        }

        public void OnGet(Guid clientId, int? ideoId)
        {
            ClientId = clientId;
            if (ideoId.HasValue)
            {
                    SelectedIdeoId = ideoId.Value;
            }
            else
            {
                if (_service.TryGetIdeos(clientId, out var ideos))
                    SelectedIdeoId = ideos.FirstOrDefault()?.Id ?? -1;
                else
                    SelectedIdeoId = -1;
            }

            if (SelectedIdeoId != -1)
            {
                SelectedIdeo = _service.GetIdeo(ClientId, SelectedIdeoId);
            }
        }

        public IEnumerable<IdeoPayload> GetIdeos()
        {
            if (_service.TryGetIdeos(ClientId, out var ideos))
                return ideos;

            return Enumerable.Empty<IdeoPayload>();
        }

        public string GetRitualSymbol(RitualPreceptPayload ritual)
        {
            if (ritual.IsAnytime)
            {
                return "⟳";
            }
            else if (!string.IsNullOrWhiteSpace(ritual.DateTrigger))
            {
                return "📅";
            }
            else
            {
                return "!";
            }
        }
    }
}