using Data.DbModels;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.ClassAssignmentServices
{
    public class ClassAssignmentService : IClassAssignmentService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Class> _classRepository;

        public ClassAssignmentService(IRepository<User> userRepository, IRepository<Class> classRepository)
        {
            _userRepository = userRepository;
            _classRepository = classRepository;
        }

        public async Task<bool> AssignUserToClassAsync(Guid userId, Guid classId)
        {
            var user = await _userRepository.GetDbObjectByIdAsync(userId);
            var @class = await _classRepository.GetDbObjectByIdAsync(classId);

            if (user == null || @class == null)
            {
                return false;
            }

            if (user.Classes == null)
            {
                user.Classes = new List<Class>();
            }

            if (user.Classes.Any(c => c.Id == classId))
            {
                return false; 
            }

            user.Classes.Add(@class);
            await _userRepository.UpdateDbObjectAsync(user);

            return true;
        }

        public async Task<bool> RemoveUserFromClassAsync(Guid userId, Guid classId)
        {
            var user = await _userRepository.GetDbObjectByIdAsync(userId);

            if (user == null || user.Classes == null)
            {
                return false;
            }

            var @class = user.Classes.FirstOrDefault(c => c.Id == classId);

            if (@class != null)
            {
                user.Classes.Remove(@class);
                await _userRepository.UpdateDbObjectAsync(user);
                return true;
            }

            return false; 
        }
    }
}
