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
            await _context.Motos.AddAsync(moto);
            await _context.SaveChangesAsync();
        }

        public async Task<Motorcycle> GetByIdAsync(Guid id)
        {
            return await _context.Motos.FindAsync(id);
        }
    }
   
}
