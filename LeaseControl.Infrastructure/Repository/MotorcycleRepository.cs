using LeaseControl.Domain;
using LeaseControl.Domain.InterfaceRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Infrastructure.Repository
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly ApplicationDbContext _context;

        public MotorcycleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Motorcycle moto)
        {
            await _context.Motorcycles.AddAsync(moto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var plate = await _context.Motorcycles.AsQueryable().Where(p => p.Id == id).FirstOrDefaultAsync();
            _context.Motorcycles.Remove(plate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Motorcycle moto)
        {
            _context.Motorcycles.Update(moto);
            await _context.SaveChangesAsync();
        }

        public async Task<Motorcycle> GetByIdAsync(Guid id)
        {
            return await _context.Motorcycles.FindAsync(id);
        }

        public async Task<Motorcycle> GetByPlateAsync(string plate)
        {
            return _context.Motorcycles.Where(p => p.Plate == plate).FirstOrDefault();
        }

        public async Task<IEnumerable<Motorcycle>> GetAllAsync()
        {
            return await _context.Motorcycles.ToListAsync();
        }
               

    }

}
