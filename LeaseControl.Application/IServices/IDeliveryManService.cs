using LeaseControl.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Application.IServices
{
    public  interface IDeliveryManService
    {
        Task<DeliveryMan> AddDeliveryMan(DeliveryMan deliveryMan);
    }
}
