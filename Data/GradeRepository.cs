using Microsoft.EntityFrameworkCore;
using Student.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student.Web.Api.Data
{
    public class GradeRepository : IGradeRepository, IRepository<Grade>
    {
        private readonly StudentDataContext _dbContext;

        public GradeRepository(StudentDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        // IGradeRepository implementations
        public async Task<List<Grade>> GetAllGradesAsync()
        {
            return await _dbContext.Grades.ToListAsync();
        }

        public async Task<Grade?> GetGradeByIdAsync(int id)
        {
            return await _dbContext.Grades.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddGradeAsync(Grade grade)
        {
            await _dbContext.AddAsync(grade);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateGradeAsync(Grade grade)
        {
            _dbContext.Grades.Update(grade);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteGradeAsync(int id)
        {
            var grade = await _dbContext.Grades.FindAsync(id);
            if (grade != null)
            {
                _dbContext.Grades.Remove(grade);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Grade>> GetAllByPupilIdAsync(string pupilId)
        {
            return await _dbContext.Grades
                .Where(x => x.PupilId == pupilId).ToListAsync();
        }

         public async Task<IEnumerable<Grade>> GetAllBySubjectIdAsync(int subjectId)
        {
            return await _dbContext.Grades
                               .Where(g => g.SubjectId == subjectId)
                               .ToListAsync();
        }

        // IRepository<Grade> implementations
        public void Add(Grade grade)
        {
            _dbContext.Grades.Add(grade);
        }

        public void Delete(Grade grade)
        {
            _dbContext.Grades.Remove(grade);
        }

        public async Task<Grade?> GetById<K>(K id)
        {
            return await _dbContext.Grades.FindAsync(id);
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
