using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Domain.InterfaceRepository
{
    public interface IDeliveryManRepository
    {
        Task AddDeliveryMan(DeliveryMan deliveryMan);
        Task<DeliveryMan> GetByCNPJ(string cnpj);
        Task<DeliveryMan> GetByCNH(string cnh);
        Task UpdateAsync(DeliveryMan deliveryMan);
        Task<DeliveryMan> GetByIdAsync(Guid id);
    }
}
