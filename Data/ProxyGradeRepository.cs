using Student.Web.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Student.Web.Api.Data
{
// Proxy pattern  
    public class ProxyGradeRepository : IGradeRepository, IRepository<Grade>
    {
        private readonly GradeRepository _realGradeRepository;
        private List<Grade>? _cachedGrades;

        public ProxyGradeRepository(GradeRepository realGradeRepository)
        {
            _realGradeRepository = realGradeRepository;
            _cachedGrades = null; 
        }

        // Implementations for IGradeRepository
        public async Task<List<Grade>> GetAllGradesAsync()
        {
            if (_cachedGrades == null)
            {
                _cachedGrades = await _realGradeRepository.GetAllGradesAsync();
            }
            return _cachedGrades;
        }

        public async Task<Grade?> GetGradeByIdAsync(int id)
        {
            return await _realGradeRepository.GetGradeByIdAsync(id);
        }

        public async Task AddGradeAsync(Grade grade)
        {
            await _realGradeRepository.AddGradeAsync(grade);
        }

        public async Task UpdateGradeAsync(Grade grade)
        {
            await _realGradeRepository.UpdateGradeAsync(grade);
        }

        public async Task DeleteGradeAsync(int id)
        {
            await _realGradeRepository.DeleteGradeAsync(id);
        }

        public async Task<List<Grade>> GetAllByPupilIdAsync(string pupilId)
        {
            return await _realGradeRepository.GetAllByPupilIdAsync(pupilId);
        }
        public async Task<IEnumerable<Grade>> GetAllBySubjectIdAsync(int subjectId)
         {
        return await _realGradeRepository.GetAllBySubjectIdAsync(subjectId);
        }
        // Implementations for IRepository<Grade>
        public void Add(Grade grade)
        {
            _realGradeRepository.Add(grade);
        }

        public void Delete(Grade grade)
        {
            _realGradeRepository.Delete(grade);
        }

        public async Task<Grade?> GetById<K>(K id)
        {
            return await _realGradeRepository.GetById(id);
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _realGradeRepository.SaveAllChangesAsync();
        }
    }
}
