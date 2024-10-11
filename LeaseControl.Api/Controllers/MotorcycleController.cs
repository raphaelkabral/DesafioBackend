using LeaseControl.Application.Services;
using LeaseControl.Domain;
using LeaseControl.Infrastructure.Mensageria;
using Microsoft.AspNetCore.Mvc;
namespace LeaseControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcycleController : ControllerBase
    {
        private readonly MotorcycleService _motoService;
        private readonly MotorcycleNotifier _motoNotifier;
        public MotorcycleController(MotorcycleService motoService, MotorcycleNotifier motorcycleNotifier)
        {
            _motoService = motoService;
            _motoNotifier = motorcycleNotifier;
        }

        /// <summary>
        /// Cadastrar uma novo moto
        /// </summary>
        /// <param name="moto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMotorcycle([FromBody] Motorcycle moto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newMoto = await _motoService.AddMotorcycle(moto);
            await _motoNotifier.NotifyMotorcyle(newMoto);
            return CreatedAtAction(nameof(AddMotorcycle), new { id = moto.Id }, moto);
        }

        /// <summary>
        /// Consultar motos exitentes
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GatAll(string plate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motos = await _motoService.GetMotorcycles(plate);
            return Ok(motos);
        }

        /// <summary>
        /// Modifica placa de uma moto
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        [HttpPut("{id}/plate")]
        public async Task<IActionResult> UpdateMotorocycle(string id, [FromBody] string plate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Guid guid = Guid.Parse(id);
            var moto = await _motoService.UpdateMotorcycle(guid, plate);
            return Ok(moto);
        }

        /// <summary>
        /// Consulta motos existentes por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetByIdMotorocycle(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Guid guid = Guid.Parse(id);
            var moto = await _motoService.GetByIdMotorcycle(guid);
            return Ok(moto);
        }

        /// <summary>
        /// Remover uma moto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMotorocycle(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Guid guid = Guid.Parse(id);
            await _motoService.RemoveMotorcycle(guid);
            return NoContent();
        }




    }
}
