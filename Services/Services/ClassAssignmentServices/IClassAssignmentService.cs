using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.ClassAssignmentServices
{
    public interface IClassAssignmentService
    {
        Task<bool> AssignUserToClassAsync(Guid userId, Guid classId);
        Task<bool> RemoveUserFromClassAsync(Guid userId, Guid classId);
    }
}
