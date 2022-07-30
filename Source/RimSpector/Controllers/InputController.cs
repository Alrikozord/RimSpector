using Microsoft.AspNetCore.Mvc;
using RimSpectorApi.Contract;

namespace RimSpectorApi.Controllers
{
    [Controller]
    public class InputController : Controller
    {
        private readonly Cache _cache;

        public InputController(Cache cache)
        {
            _cache = cache;
        }

        [HttpPost("{id}")]
        public IActionResult Post(Guid Id, [FromBody] Payload payload)
        {
            if (Id != payload.Id)
                return BadRequest("Id missmatch");

            _cache.Add(payload);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid Id)
        {
            if (_cache.TryGet(Id, out var payload))
                return Ok(payload);

            return NoContent();
        }
    }
}
