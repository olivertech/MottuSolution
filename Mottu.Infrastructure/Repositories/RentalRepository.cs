﻿using Microsoft.Extensions.Caching.Distributed;

namespace Mottu.Infrastructure.Repositories
{
    public class RentalRepository : RepositoryBase<Rental>, IRentalRepository
    {
        public RentalRepository([NotNull] AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Rental>?> GetFullAll()
        {
            return await _context!.Rentals
                .Include(b => b.Bike)
                .Include(u => u.User)
                .Include(p => p.Plan)
                .ToListAsync();
        }

        public async Task<Rental?> GetFullById(Guid? id)
        {
            return await _context!.Rentals
                .Include(b => b.Bike)
                .Include(u => u.User)
                .Include(p => p.Plan)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
