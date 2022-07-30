using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RimSpector.Pages
{
    public class PawnsModel : PageModel
    {
        private readonly ILogger<PawnsModel> _logger;

        public PawnsModel(ILogger<PawnsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}