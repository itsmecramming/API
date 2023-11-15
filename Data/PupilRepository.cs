using Microsoft.EntityFrameworkCore;
using Student.Web.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student.Web.Api.Data
{
    public class PupilRepository : IPupilRepository
    {
        private readonly StudentDataContext _studentContext;

        public PupilRepository(StudentDataContext studentContext)
        {
            _studentContext = studentContext;
        }

        public void Add(Pupil pupil)
        {
            _studentContext.Add(pupil);
        }

        public void Delete(Pupil pupil)
        {
            _studentContext.Remove(pupil);
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _studentContext.SaveChangesAsync() > 0;
        }

        public async Task Update<K>(K id, Pupil input)
        {
            if (id == null) return; //Handling null 'id'
            
            var stringId = id.ToString();
            var thePupil = await _studentContext.Students.FindAsync(stringId);
            if (thePupil != null)
            {
                thePupil.LastName = input.LastName;
                thePupil.FirsName = input.FirsName;
                thePupil.MiddleName = input.MiddleName;
                _studentContext.Update(thePupil);
                await _studentContext.SaveChangesAsync();
            }
        }

        public async Task<List<Pupil>> GetAllAsync()
        {
            return await _studentContext.Students.ToListAsync();
        }

        public async Task<Pupil?> GetById<K>(K id)
        {
            if (id == null) return null; //handling null 'id'
            
            var stringId = id.ToString();
            return await _studentContext.Students.FirstOrDefaultAsync(x => x.StudentId == stringId);
        }
    }
}
