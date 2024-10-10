using LeaseControl.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Application.IServices
{
    public interface IMotorcycleService
    {
        Task<Motorcycle> AddMotorcycle(Motorcycle motorcycle);
        Task<Motorcycle> UpdateMotorcycle(Guid id, string novaPlaca);
        Task<bool>RemoveMotorcycle(Guid id);
        Task<Motorcycle> GetByIdMotorcycle(Guid plate);
        Task<IEnumerable<Motorcycle>> GetMotorcycles(string? plate);



    }
}
