using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RimSpector.Pages
{
    public class HistoryModel : PageModel
    {
        private readonly ILogger<HistoryModel> _logger;

        public HistoryModel(ILogger<HistoryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}