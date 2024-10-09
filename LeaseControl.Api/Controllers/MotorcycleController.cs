using LeaseControl.Application.Services;
using LeaseControl.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LeaseControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcycleController : ControllerBase
    {
        private readonly MotorcycleService _motoService;

        public MotorcycleController(MotorcycleService motoService)
        {
            _motoService = motoService;
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

            await _motoService.AddMotorcycle(moto);
            return CreatedAtAction(nameof(AddMotorcycle), new { id = moto.Id }, moto);
        }

        /// <summary>
        /// Consultar motos exitentes
        /// </summary>
        /// <param name="motos"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GatAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _motoService.GetAllMotorcycle();
            return CreatedAtAction(nameof(GatAll), null);
        }

        /// <summary>
        /// Modifica placa de uma moto
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateMotorocycle([FromBody] string plate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _motoService.UpdateMotorcycle(plate);
            return CreatedAtAction(nameof(UpdateMotorocycle), plate);
        }

        /// <summary>
        /// Consulta motos existentes por id
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetByIdMotorocycle([FromBody] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _motoService.GetByIdMotorcycle(id);
            return CreatedAtAction(nameof(GetByIdMotorocycle), new { id = id });
        }

        /// <summary>
        /// Remover uma moto
        /// </summary>
        /// <param name="moto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RemoveMotorocycle([FromBody] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _motoService.RemoveMotorcycle(id);
            return CreatedAtAction(nameof(RemoveMotorocycle), new { id = id });
        }




    }
}
