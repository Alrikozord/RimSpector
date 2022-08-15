using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RimSpectorApi.Pages
{
    public class HomeModel : PageModel
    {
        private readonly Cache _cache;

        public int CachedPayloadCount => _cache.PayloadCount;
        public Guid? SomePayloadId => _cache.LastAddedPayload;

        public HomeModel(Cache cache)
        {
            _cache = cache;
        }

        public void OnGet()
        {
        }
    }
}
