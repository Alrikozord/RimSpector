using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RimSpector.Pages
{
    public class IdeologyModel : PageModel
    {
        private readonly ILogger<IdeologyModel> _logger;

        public IdeologyModel(ILogger<IdeologyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}