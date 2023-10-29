using Data.DbModels;
using Data.Repositories;
using Services.Mappings;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.ClassServices
{
    public class ClassService : IClassService
    {
        private readonly IRepository<Class> _ClassRepository;

        public ClassService(IRepository<Class> ClassRepository)
        {
            _ClassRepository = ClassRepository;
        }

        #region CRUD

        public async Task<ClassModel> AddClass(ClassModel newClass)
        {
            return (await _ClassRepository.AddDbObjectAsync(newClass.ToClass())).ToClassModel();
        }

        public async Task<List<ClassModel>> GetAllClasss()
        {
            List<Class> Classs = (await _ClassRepository.GetAllDbObjectsAsync()).ToList();

            return Classs.Select(x => x.ToClassModel()).ToList();
        }

        public async Task<ClassModel> GetClassById(Guid id)
        {
            return (await _ClassRepository.GetDbObjectByIdAsync(id)).ToClassModel();
        }

        public List<ClassModel> GetClasssByFilter(ClassModel filter)
        {
            List<Class> Classs = _ClassRepository.GetDbObjectsByFilter(filter.ToClass()).ToList();

            return Classs.Select(x => x.ToClassModel()).ToList();
        }

        public async Task RemoveClass(Guid id)
        {
            await _ClassRepository.RemoveDbObjectAsync(id);
        }

        public async Task<ClassModel> UpdateClass(ClassModel updatedClass)
        {
            return (await _ClassRepository.UpdateDbObjectAsync(updatedClass.ToClass())).ToClassModel();
        }

        #endregion
    }
}
