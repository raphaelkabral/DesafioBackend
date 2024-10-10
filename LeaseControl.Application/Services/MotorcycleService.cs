using LeaseControl.Application.IServices;
using LeaseControl.Domain;
using LeaseControl.Domain.InterfaceRepository;
using LeaseControl.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motoRepository;
        private readonly ILeaserRepository _leaserRepository;
        private readonly IModel _rabbitMqChannel;

        public MotorcycleService(IMotorcycleRepository motoRepository, ILeaserRepository leaserRepository, IModel rabbitMqChannel)
        {
            _motoRepository = motoRepository;
            _leaserRepository =leaserRepository;
            _rabbitMqChannel = rabbitMqChannel;
        }


        public async Task<Motorcycle> AddMotorcycle(Motorcycle motorcycle)
        {
            if (motorcycle == null)
                throw new Exception("Faltando Informações...");

            var motos = await _motoRepository.GetAllAsync();
            if (motos.Any(m => m.Plate == motorcycle.Plate))
                throw new Exception("Placa já cadastrada.");
            
            motorcycle.Id = Guid.NewGuid();
            await _motoRepository.AddAsync(motorcycle);
            // Publicar evento
            var mensagem = JsonConvert.SerializeObject(_motoRepository);
            _rabbitMqChannel.BasicPublish("", "moto_removida", null, Encoding.UTF8.GetBytes(mensagem));

            return motorcycle;
        }

        public async Task<bool> RemoveMotorcycle(Guid id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null)
                throw new Exception("Moto não encontrada.");

            // Aqui você deve adicionar lógica para verificar se há locações ativas
            var locExists = await _leaserRepository.ExistsLoacation(id);

            if (locExists)
                return false;

            await _motoRepository.RemoveAsync(id);
            return true;
        }

        public async Task<Motorcycle> UpdateMotorcycle(Guid id, string newPlate)
        {

            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null)
                throw new Exception("Moto não encontrada.");

            var motos = await _motoRepository.GetAllAsync();
            if (motos.Any(m => m.Plate == newPlate))
                throw new Exception("Placa já cadastrada.");

            moto.Plate = newPlate;
            await _motoRepository.UpdateAsync(moto);
            return moto;            
        }

        public async Task<IEnumerable<Motorcycle>> GetMotorcycles(string? plate)
        {

            if (string.IsNullOrWhiteSpace(plate))
                return await _motoRepository.GetAllAsync();

            var motos = await _motoRepository.GetAllAsync();
            return  motos.Where(m => m.Plate.Contains(plate)).ToList();
        }

        public async Task<Motorcycle> GetByIdMotorcycle(Guid id)
        {
            return await _motoRepository.GetByIdAsync(id);
        }


    }
}
