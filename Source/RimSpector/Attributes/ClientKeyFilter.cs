using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace RimSpectorApi.Attributes
{
    public class ClientKeyFilter : IAsyncActionFilter
    {
        public const string ClientKeyName = "CLIENT-KEY";
        private readonly Cache _cache;
        public string ClientIdPathVariableName { get; set; }

        public ClientKeyFilter(Cache cache)
        {
            _cache = cache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Enforce Https
            if (!context.HttpContext.Request.IsHttps)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.HttpContext.Response.WriteAsync("Must use Https when providing CLIENT-KEY.");
                return;
            }

            // Ensure Key is provided
            if (!context.HttpContext.Request.Headers.TryGetValue(ClientKeyName, out var extractedClientKey))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.HttpContext.Response.WriteAsync("Client Key was not provided.");
                return;
            }

            // validate key
            if (!_cache.IsClientKeyValidForPayload(
                    extractedClientKey.Single(),
                    Guid.Parse(context.RouteData.Values[ClientIdPathVariableName]!.ToString()!)))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.HttpContext.Response.WriteAsync("Unauthorized client.");
                return;
            }

            await next();
        }
    }

    /// <summary>
    /// Löst Abhängigkeiten eines ApiKeyFilters auf.
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    internal class ClientKeyAttribute : Attribute, IFilterFactory
    {
        private readonly string _clientIdPathVariableName;

        public bool IsReusable => false;

        public ClientKeyAttribute(string clientIdPathVariableName)
        {
            _clientIdPathVariableName = clientIdPathVariableName;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filter = serviceProvider.GetRequiredService<ClientKeyFilter>();
            filter.ClientIdPathVariableName = _clientIdPathVariableName;
            return filter;
        }
    }
}
