using LeaseControl.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Application.IServices
{
    public interface ILeaseService
    {
        Task<Lease> Addlease(Lease lease);
        Task<Lease> GetbyIdlease(Guid lease);
        Task<decimal> CalculateCost(Guid leaseId, DateTime actualReturnDate);
    }
}
