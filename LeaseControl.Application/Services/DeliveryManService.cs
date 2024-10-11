using LeaseControl.Application.IServices;
using LeaseControl.Domain;
using LeaseControl.Domain.InterfaceRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Application.Services
{
    public class DeliveryManService : IDeliveryManService
    {
        private readonly IDeliveryManRepository _deliveryManRepository;
        private const string StoragePath = "C:\\StorageCnh\\"; // Caminho do diretório local

        public DeliveryManService(IDeliveryManRepository deliveryManRepository)
        {
            _deliveryManRepository = deliveryManRepository;
        }

        public async Task<DeliveryMan> AddDeliveryMan(DeliveryMan deliveryMan)
        {
            // Verificar unicidade do CNPJ e CNH
            if (_deliveryManRepository.GetByCNPJ(deliveryMan.CNPJ) != null)
                throw new Exception("CNPJ já cadastrado.");
            if (_deliveryManRepository.GetByCNH(deliveryMan.CNH) != null)
                throw new Exception("Número da CNH já cadastrado.");

            await _deliveryManRepository.AddDeliveryMan(deliveryMan);
            return deliveryMan;
        }

        public async Task UpdateCNHAsync(Guid id, Stream cnhStream, string fileName)
        {
            var entregador = await _deliveryManRepository.GetByIdAsync(id);
            if (entregador == null) throw new KeyNotFoundException("Entregador não encontrado.");

            var validExtensions = new[] { ".png", ".bmp" };
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            if (!validExtensions.Contains(extension)) throw new InvalidDataException("Formato de arquivo inválido.");

            var filePath = Path.Combine(StoragePath, $"{entregador.CNH}{extension}");
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await cnhStream.CopyToAsync(fileStream);
            }

            entregador.CNH = filePath;
            await _deliveryManRepository.UpdateAsync(entregador);
        }


    }
}
