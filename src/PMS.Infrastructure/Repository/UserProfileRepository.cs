using Microsoft.EntityFrameworkCore;
using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Data.Context;

namespace PMS.Infrastructure.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        public readonly PmsDbContext _context;



        public UserProfileRepository(PmsDbContext context)
        {
            _context = context;
        }



        public async Task<UserProfile> GetByIdAsync(Guid id)
            => await Task.FromResult(await _context.UserProfiles.FirstOrDefaultAsync(u => u.Id == id));

        public async Task CreateAsync(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                throw new ArgumentNullException(nameof(userProfile), "UserProfile cannot be null");
            }

            await _context.UserProfiles.AddAsync(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                throw new ArgumentNullException(nameof(userProfile), "UserProfile cannot be null");
            }

            _context.UserProfiles.Update(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                throw new ArgumentNullException(nameof(userProfile), "UserProfile cannot be null");
            }

            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
        }
    }
}