using Microsoft.AspNetCore.Mvc;
using RimSpectorApi.Contracts;

namespace RimSpectorApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RimSpectorDataController : Controller
    {
        private readonly Service _service;

        public RimSpectorDataController(Service service)
        {
            _service = service;
        }

        [HttpPost("{id}")]
        public IActionResult Post(Guid id, [FromBody] Payload payload)
        {
            if (id != payload.Id)
                return BadRequest("Id missmatch");

            _service.Add(payload);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if (_service.TryGetFullPayload(id, out var payload))
                return Ok(payload);

            return NoContent();
        }

        [HttpGet("{id}/Pawns")]
        public IActionResult GetPawns(Guid id)
        {
            if (_service.TryGetPawns(id, out var pawns))
                return Ok(pawns);

            return NoContent();
        }

        [HttpGet("{id}/Pawns/{pawnId}")]
        public IActionResult GetPawn(Guid id, string pawnId)
        {
            if (_service.TryGetPawn(id, pawnId, out var pawn))            
                return Ok(pawn);            

            return NotFound();
        }

        [HttpGet("{id}/Ideos")]
        public IActionResult GetIdeos(Guid id)
        {
            if (_service.TryGetIdeos(id, out var ideos))
                return Ok(ideos);

            return NoContent();
        }

        [HttpGet("{id}/Ideos/{pawnId}")]
        public IActionResult GetIdeos(Guid id, int ideoId)
        {
            if (_service.TryGetIdeo(id, ideoId, out var ideo))
                return Ok(ideo);

            return NoContent();
        }
    }
}
