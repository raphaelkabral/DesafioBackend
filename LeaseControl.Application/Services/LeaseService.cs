using LeaseControl.Application.IServices;
using LeaseControl.Domain;
using LeaseControl.Domain.InterfaceRepository;
using LeaseControl.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Application.Services
{
    public  class LeaseService : ILeaseService
    {
        private readonly ILeaserRepository _leaserRepository;
        private readonly IDeliveryManRepository _deliveryManRepository;
        

        public LeaseService(ILeaserRepository leaserRepository, IDeliveryManRepository deliveryManRepository)
        {
            _leaserRepository = leaserRepository;
            _deliveryManRepository = deliveryManRepository;
        }

        public async Task<Lease> Addlease(Lease lease)
        {
            // Validações antes de criar a locação
            if (lease.StartDate < DateTime.Now.AddDays(1))
                throw new Exception("A data de início deve ser pelo menos um dia após a criação.");
            
            var deliveryMan = await _deliveryManRepository.GetByIdAsync(lease.DeliveryMan.Id);
            if (!deliveryMan.TypeCnh.Contains("A"))
                throw new Exception("CNH não compativel para permissão de locação.");

            await _leaserRepository.AddLease(lease);
            return lease;
        }

        public async Task<Lease> GetbyIdlease(Guid id)
        {
            return await _leaserRepository.GetByIdLease(id);
        }


        public async Task<decimal> CalculateCost(Guid leaseId, DateTime actualReturnDate)
        {        
            var rental = await _leaserRepository.GetByIdLease(leaseId);
            return rental.CalculateTotalCost(actualReturnDate);
        }

    }
}
