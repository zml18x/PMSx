using Microsoft.EntityFrameworkCore;
using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Data.Context;

namespace PMS.Infrastructure.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PmsDbContext _context;



        public PropertyRepository(PmsDbContext context)
        {
            _context = context;
        }



        public async Task<Property> GetByIdAsync(Guid propertyId)
            => await Task.FromResult(await _context.Properties.FirstOrDefaultAsync(p => p.Id == propertyId));

        public async Task<Property> GetByNameAsync(Guid userId, string name)
            => await Task.FromResult(await _context.Properties.FirstOrDefaultAsync(p => p.UserId == userId && p.Name == name));

        public async Task<IEnumerable<Property>> GetAllAsync(Guid userId)
            => await Task.FromResult(_context.Properties.Where(p => p.UserId == userId).AsEnumerable());

        public async Task CreateAsync(Property property)
        {
            if (property == null) 
                throw new ArgumentNullException(nameof(property), "Property cannot be null");

            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Property property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property), "Property cannot be null");

            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Property property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property), "Property cannot be null");

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
        }
    }
}