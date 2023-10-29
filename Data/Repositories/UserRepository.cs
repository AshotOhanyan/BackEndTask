using Data.Data;
using Data.DbModels;
using Data.OtherMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly DBContext _dbContext;

        public UserRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddDbObjectAsync(User newObject)
        {
            User newUser = new User()
            {
                FirstName = newObject.FirstName,
                LastName = newObject.LastName,
                Email = newObject.Email,
                Address= StaticMethods.FormatAddress(newObject.Address),
                DateOfBirth= newObject.DateOfBirth,
                EmailConfirmationToken= newObject.EmailConfirmationToken,
                TokenExpirationDate= newObject.TokenExpirationDate,
                IsEmailConfirmed= newObject.IsEmailConfirmed,
                PasswordHash= newObject.PasswordHash,
                PhoneNumber= newObject.PhoneNumber,
                RefreshToken= newObject.RefreshToken,
                Salt= newObject.Salt,
                Classes = new List<Class>()
            };


            newUser = (await _dbContext.Users.AddAsync(newUser)).Entity;
            await _dbContext.SaveChangesAsync();

            return newUser;
        }

        public async Task<IEnumerable<User>> GetAllDbObjectsAsync()
        {
            return await _dbContext.Users.Include(x => x.Classes).ToListAsync();
        }

        public async Task<User> GetDbObjectByIdAsync(Guid id)
        {
            return await _dbContext.Users.Include(x => x.Classes).FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<User> GetDbObjectsByFilter(User filter)
        {
            IQueryable<User> users = _dbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                users = users.Where(u => u.FirstName == filter.FirstName);
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                users = users.Where(u => u.LastName == filter.LastName);
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                users = users.Where(u => u.Email == filter.Email);
            }

            if (filter.DateOfBirth != default(DateTime))
            {
                users = users.Where(u => u.DateOfBirth == filter.DateOfBirth);
            }

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
            {
                users = users.Where(u => u.PhoneNumber == filter.PhoneNumber);
            }

            if (!string.IsNullOrEmpty(filter.Address))
            {
                users = users.Where(u => u.Address == filter.Address);
            }            
            
            if (!string.IsNullOrEmpty(filter.PasswordHash))
            {
                users = users.Where(u => u.PasswordHash == filter.PasswordHash);
            }

            if (!string.IsNullOrEmpty(filter.EmailConfirmationToken))
            {
                users = users.Where(u => u.EmailConfirmationToken == filter.EmailConfirmationToken);
            }

            if (!string.IsNullOrEmpty(filter.RefreshToken))
            {
                users = users.Where(u => u.RefreshToken == filter.RefreshToken);
            }

            if (!string.IsNullOrEmpty(filter.Salt))
            {
                users = users.Where(u => u.Salt == filter.Salt);
            }

            if (filter.IsEmailConfirmed != null)
            {
                users = users.Where(u => u.IsEmailConfirmed == filter.IsEmailConfirmed);
            }

            return users;
        }

        public async Task RemoveDbObjectAsync(Guid id)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<User> UpdateDbObjectAsync(User updatedObject)
        {
             User user = _dbContext.Users.Update(updatedObject).Entity;
             await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
