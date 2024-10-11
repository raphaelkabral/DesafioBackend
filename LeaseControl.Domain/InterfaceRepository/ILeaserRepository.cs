using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Domain.InterfaceRepository
{
    public interface ILeaserRepository
    {
        Task<bool> ExistsLoacation(Guid id);
        Task AddLease(Lease lease);
        Task<Lease> GetByIdLease(Guid id);
        Task ReturnLease(Lease lease);

    }
}
