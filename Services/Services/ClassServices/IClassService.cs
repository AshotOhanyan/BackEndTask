using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.ClassServices
{
    public interface IClassService
    {
        public Task<List<ClassModel>> GetAllClasss();
        public Task<ClassModel> GetClassById(Guid id);
        public List<ClassModel> GetClasssByFilter(ClassModel filter);
        public Task<ClassModel> AddClass(ClassModel model);
        public Task<ClassModel> UpdateClass(ClassModel model);
        public Task RemoveClass(Guid id);
    }
}
