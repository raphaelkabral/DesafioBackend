using LeaseControl.Domain.InterfaceRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Infrastructure.Repository
{
    public class LeaserRepository : ILeaserRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaserRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> ExistsLoacation(Guid id) 
        {
            return await _context.Leasess.AnyAsync(l => l.Motorcycle.Id == id);
        }

    }
}
