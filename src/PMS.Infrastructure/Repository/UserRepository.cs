using Microsoft.EntityFrameworkCore;
using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Data.Context;

namespace PMS.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly PmsDbContext _context;


        public UserRepository(PmsDbContext context)
        {
            _context = context;
        }



        public async Task<User> GetAsyncById(Guid userId)
            => await Task.FromResult(await _context.Users.FirstOrDefaultAsync(u => u.Id == userId));

        public async Task<User> GetAsyncByEmail(string email)
            => await Task.FromResult(await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower()));

        public async Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}