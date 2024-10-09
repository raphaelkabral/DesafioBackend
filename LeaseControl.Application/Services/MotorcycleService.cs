using LeaseControl.Application.IServices;
using LeaseControl.Domain;
using LeaseControl.Domain.InterfaceRepository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motoRepository;
        private readonly IModel _rabbitMqChannel;

        public MotorcycleService(IMotorcycleRepository motoRepository, IModel rabbitMqChannel)
        {
            _motoRepository = motoRepository;
            _rabbitMqChannel = rabbitMqChannel;
        }


        public async Task AddMotorcycle(Motorcycle motorcycle)
        {
            await _motoRepository.AddAsync(motorcycle);
            var mensagem = JsonConvert.SerializeObject(_motoRepository);
            _rabbitMqChannel.BasicPublish("", "moto_cadastrada", null, Encoding.UTF8.GetBytes(mensagem));
        }       

        public async Task RemoveMotorcycle(int id)
        {
            await _motoRepository.RemoveAsync(id);
            var mensagem = JsonConvert.SerializeObject(_motoRepository);
            _rabbitMqChannel.BasicPublish("", "moto_removida", null, Encoding.UTF8.GetBytes(mensagem));
        }

        public async Task UpdateMotorcycle(string plate)
        {
            var motorcycle = _motoRepository.GetByPlateAsync(plate).Result;
            await _motoRepository.UpdateAsync(motorcycle);
            var mensagem = JsonConvert.SerializeObject(_motoRepository);
            _rabbitMqChannel.BasicPublish("", "moto_atualizada", null, Encoding.UTF8.GetBytes(mensagem));
        }

        public async Task<IEnumerable<Motorcycle>> GetAllMotorcycle()
        {
            var result = await _motoRepository.GetAllAsync();
            var mensagem = JsonConvert.SerializeObject(_motoRepository);
            _rabbitMqChannel.BasicPublish("", "moto_todasmotos", null, Encoding.UTF8.GetBytes(mensagem));
            return result;
        }

        public async Task GetByIdMotorcycle(int id)
        {
            await _motoRepository.GetByIdAsync(id);
            var mensagem = JsonConvert.SerializeObject(_motoRepository);
            _rabbitMqChannel.BasicPublish("", "moto_buscamoto", null, Encoding.UTF8.GetBytes(mensagem));
        }

     
    }
}
