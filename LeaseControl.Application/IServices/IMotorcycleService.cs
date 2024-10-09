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
        Task AddMotorcycle(Motorcycle motorcycle);
        Task UpdateMotorcycle(string plate);
        Task RemoveMotorcycle(int id);
        Task GetByIdMotorcycle(int plate);
        Task<IEnumerable<Motorcycle>> GetAllMotorcycle();



    }
}
