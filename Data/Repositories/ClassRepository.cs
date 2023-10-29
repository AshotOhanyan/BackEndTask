using Data.Data;
using Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ClassRepository : IRepository<Class>
    {
        private readonly DBContext _dbContext;

        public ClassRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Class> AddDbObjectAsync(Class newObject)
        {
            Class newClass = new Class()
            {
                Name = newObject.Name,
                Users = new List<User>()
            };

            newClass = (await _dbContext.Classes.AddAsync(newClass)).Entity;
            await _dbContext.SaveChangesAsync();

            return newClass;
        }

        public async Task<IEnumerable<Class>> GetAllDbObjectsAsync()
        {
            return await _dbContext.Classes.Include(x => x.Users).ToListAsync();
        }

        public async Task<Class> GetDbObjectByIdAsync(Guid id)
        {
            return await _dbContext.Classes.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Class> GetDbObjectsByFilter(Class filter)
        {
            IQueryable<Class> classes = _dbContext.Classes.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                classes = classes.Where(u => u.Name == filter.Name);
            }

            return classes;
        }

        public async Task RemoveDbObjectAsync(Guid id)
        {
            Class? @class = await _dbContext.Classes.FirstOrDefaultAsync(x => x.Id == id);

            if (@class != null)
            {
                _dbContext.Classes.Remove(@class);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Class> UpdateDbObjectAsync(Class updatedObject)
        {
            Class updatedClass = _dbContext.Classes.Update(updatedObject).Entity;
            await _dbContext.SaveChangesAsync();

            return updatedClass;
        }
    }
}
