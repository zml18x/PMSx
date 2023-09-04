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
            => await Task.FromResult(await _context.Properties.FirstOrDefaultAsync(p => p.UserId == userId && p.Name.ToLower() == name.ToLower()));

        public async Task<Property> GetWithRoomsByPropertyId(Guid propertyId)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(p => p.Id == propertyId);

            if (property == null)
                throw new ArgumentNullException(nameof(property),$"Property with ID '{propertyId}' does not exist");

            var rooms = _context.Rooms.Where(r => r.PropertyId == propertyId);

            foreach (var room in rooms)
                property!.AddRoom(room);

            return property;
        }


        public async Task<IEnumerable<Property>> GetAllAsync(Guid userId)
        {
            return await _context.Properties
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

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

        public async Task AddRoomsAsync(List<Room> rooms)
        {
            if (rooms == null)
                throw new ArgumentNullException(nameof(rooms), "Romms cannot be null");

            if(rooms.Count > 0)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    foreach (var room in rooms)
                        await _context.Rooms.AddAsync(room);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ArgumentException(ex.Message);
                }
            }    
        }

        public async Task<Room> GetRoomAsync(Guid propertyId, Guid roomId)
            => await Task.FromResult(await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId && r.PropertyId == propertyId));

        public async Task AddAdditionalServicesAsync(List<PropertyAdditionalService> additionalServices)
        {
            if (additionalServices == null)
                throw new ArgumentNullException(nameof(additionalServices), "AdditionalServices cannot be null");


            if(additionalServices.Count > 0)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    foreach (var additionalService in additionalServices)
                        await _context.PropertyAdditionalServices.AddAsync(additionalService);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ArgumentException(ex.Message);
                }
            } 
        }

        public async Task AddAdditionalServicesAsync(List<RoomAdditionalService> additionalServices)
        {
            if (additionalServices == null)
                throw new ArgumentNullException(nameof(additionalServices), "AdditionalServices cannot be null");


            if (additionalServices.Count > 0)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    foreach (var additionalService in additionalServices)
                        await _context.RoomAdditionalServices.AddAsync(additionalService);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ArgumentException(ex.Message);
                }
            }
        }
    }
}