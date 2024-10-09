using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Domain.InterfaceRepository
{
    public  interface IMotorcycleRepository
    {
        Task AddAsync(Motorcycle moto);
        Task RemoveAsync(int id);
        Task UpdateAsync(Motorcycle moto);
        Task<Motorcycle> GetByIdAsync(int id);

        Task<Motorcycle> GetByPlateAsync(string plate);
        Task<IEnumerable<Motorcycle>> GetAllAsync();
    }
}
