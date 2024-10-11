using LeaseControl.Application.IServices;
using LeaseControl.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LeaseControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaseController : Controller
    {
        private readonly ILeaseService _leaseService;

        public LeaseController(ILeaseService leaseService)
        {
            _leaseService = leaseService;
        }

        /// <summary>
        /// Alugar uma moto
        /// </summary>
        /// <param name="lease"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Addlease([FromBody] Lease lease)
        {
            _leaseService.Addlease(lease);
            return CreatedAtAction(nameof(Addlease), new { id = lease.Id }, lease);
        }

        /// <summary>
        /// Consulta locação por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetByIdLease(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Guid guid = Guid.Parse(id);
            var lease = await _leaseService.GetbyIdlease(guid);
            return Ok(lease);
        }

        /// <summary>
        /// Informar data de devolução e calcular valor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actualReturnDate"></param>
        /// <returns></returns>
        [HttpPut("{id}/return")]
        public IActionResult UpdateLease(string id, [FromQuery] DateTime actualReturnDate)
        {
            Guid guid = Guid.Parse(id);
            var cost = _leaseService.CalculateCost(guid, actualReturnDate);
            return Ok(new { TotalCost = cost });
        }

    }
}
