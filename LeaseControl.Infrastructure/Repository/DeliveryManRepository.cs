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
    public class DeliveryManRepository : IDeliveryManRepository
    {
        private readonly ApplicationDbContext _context;

        public DeliveryManRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDeliveryMan(DeliveryMan deliveryMan)
        {
            await _context.Deliverymens.AddAsync(deliveryMan);
            await _context.SaveChangesAsync();
        }

        public async Task<DeliveryMan> GetByCNPJ(string cnpj)
        {
            return await _context.Deliverymens.Where(p => p.CNPJ.Contains(cnpj)).FirstOrDefaultAsync();
        }

        public async Task<DeliveryMan> GetByCNH(string cnh)
        {
            return await _context.Deliverymens.Where(p => p.CNH.Contains(cnh)).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(DeliveryMan deliveryMan)
        {
            _context.Deliverymens.Update(deliveryMan);
            await _context.SaveChangesAsync();
        }

        public async Task<DeliveryMan> GetByIdAsync(Guid id)
        {
            return await _context.Deliverymens.FindAsync(id);
        }
    }
}
