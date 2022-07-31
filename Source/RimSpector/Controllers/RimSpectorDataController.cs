using Microsoft.AspNetCore.Mvc;
using RimSpectorApi.Contracts;

namespace RimSpectorApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RimSpectorDataController : Controller
    {
        private readonly Cache _cache;

        public RimSpectorDataController(Cache cache)
        {
            _cache = cache;
        }

        [HttpPost("{id}")]
        public IActionResult Post(Guid id, [FromBody] Payload payload)
        {
            //if (Guid.Parse(id) != payload.Id)
            if (id != payload.Id)
                return BadRequest("Id missmatch");

            _cache.Add(payload);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if (_cache.TryGet(id, out var payload))
                return Ok(payload);

            return NoContent();
        }
    }
}
