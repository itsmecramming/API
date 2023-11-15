using Student.Web.Api.Models;

namespace Student.Web.Api.Data
{
    public interface IGradeRepository : IRepository<Grade>
{
    Task<List<Grade>> GetAllGradesAsync();
    Task<Grade?> GetGradeByIdAsync(int id);
    Task<List<Grade>> GetAllByPupilIdAsync(string pupilId);
    Task<IEnumerable<Grade>> GetAllBySubjectIdAsync(int subjectId); // add

    Task AddGradeAsync(Grade grade);
    Task UpdateGradeAsync(Grade grade);
    Task DeleteGradeAsync(int id);
}

 
    }
