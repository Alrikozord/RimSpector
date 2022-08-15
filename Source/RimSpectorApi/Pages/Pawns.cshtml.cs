using Microsoft.AspNetCore.Mvc.RazorPages;
using RimSpectorApi.Contracts;

namespace RimSpectorApi.Pages
{
    public class PawnsModel : PageModel
    {
        private readonly ILogger<PawnsModel> _logger;
        private readonly Service _service;
        public Guid ClientId { get; set; }
        public string SelectedPawnId { get; set; } = string.Empty;

        public PawnPayload SelectedPawn { get; private set; }
        public IEnumerable<SkillPayload> DisabledSkills => SelectedPawn.Skills.Where(s => s.Disabled ?? false);
        public IEnumerable<IGrouping<string?, string?>> GroupedHealth
            => SelectedPawn.Health
                .Select<HealthPayload, (string? Part, string? Name)>(h =>
                {
                    if (string.IsNullOrWhiteSpace(h.Part))
                        return (Part: "Whole Body", h.Name);

                    return (h.Part, h.Name);
                })
                .GroupBy(h => h.Part, h => h.Name);
        public string RoleLabel
            => (SelectedPawn.Role is null) || (SelectedPawn.Role.Role is null && SelectedPawn.Role.Name is null)
            ? "Member"
            : $"{SelectedPawn.Role.Name} ({SelectedPawn.Role.Role})";
        public string IdeoName
        {
            get
            {
                if (!(SelectedPawn.Ideo is null) && _service.TryGetIdeo(ClientId, SelectedPawn.Ideo.Value, out var ideo))
                    return ideo!.Name!;

                return string.Empty;
            }
        }

        public string SkillBackgroundFill(SkillPayload skill)
        {
            if (skill.MaxLevel.HasValue && skill.Level.HasValue)
            {
                double skillPercent = (skill.Level.Value / (double)skill.MaxLevel.Value) * 100;
                skillPercent = Math.Ceiling(skillPercent);
                skillPercent = Math.Max(skillPercent, 1);

                return $"background: linear-gradient(90deg, #2d3034 {skillPercent}%, #15191d 0)";
            }
            return string.Empty;
        }

        public string SkillValueText(SkillPayload skill)
        {
            if (skill.Level.HasValue)
                return skill.Level?.ToString() ?? "0";

            else if (skill.Disabled ?? false)
                return "-";

            return "0";
        }

        public string IdeoUrl => $"/{ClientId}/Ideos/{SelectedPawn.Ideo}";

        public PawnsModel(ILogger<PawnsModel> logger, Service service)
        {
            _logger = logger;
            _service = service;
        }

        public void OnGet(Guid clientId, string? pawnId)
        {
            ClientId = clientId;
            if (string.IsNullOrEmpty(pawnId))
            {
                if (_service.TryGetPawns(clientId, out var pawns))
                    SelectedPawnId = pawns.FirstOrDefault()?.Id ?? string.Empty;
                else
                    SelectedPawnId = string.Empty;
            }
            else
            {
                SelectedPawnId = pawnId!;
            }

            if (!string.IsNullOrWhiteSpace(SelectedPawnId))
            {
                SelectedPawn = _service.GetPawn(ClientId, SelectedPawnId);
            }
        }

        public IEnumerable<PawnPayload> GetPawns()
        {
            if (_service.TryGetPawns(ClientId, out var pawns))
                return pawns;

            return Enumerable.Empty<PawnPayload>();
        }
    }
}