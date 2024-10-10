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
    }
}
