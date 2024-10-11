using LeaseControl.Application.IServices;
using LeaseControl.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LeaseControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryManContoller : ControllerBase
    {
        private readonly IDeliveryManService _deliveryManService;

        public DeliveryManContoller(IDeliveryManService deliveryManService)
        {
            _deliveryManService = deliveryManService;
        }

        /// <summary>
        /// Cadastrar entregador
        /// </summary>
        /// <param name="deliveryMan"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEntregador([FromBody] DeliveryMan deliveryMan)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deleivery = await _deliveryManService.AddDeliveryMan(deliveryMan);
            return Ok(deleivery);
        }

        /// <summary>
        /// Enviar foto da CNH
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPut("{id}/cnh")]
        public async Task<IActionResult> UpdateCNH(string id, IFormFile file)
        {
            if (file.Length == 0)
                return BadRequest("Arquivo não pode estar vazio.");

            await using var stream = file.OpenReadStream();
            Guid guid = Guid.Parse(id);
            await _deliveryManService.UpdateCNHAsync(guid, stream, file.FileName);
            return CreatedAtAction(nameof(UpdateCNH), id);
        }

    }
}
