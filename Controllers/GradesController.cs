using Microsoft.AspNetCore.Mvc;
using Student.Web.Api.Data;
using Student.Web.Api.Dto;
using Student.Web.Api.Models;
//Creational Design Pattern because it is Abstract Factory Pattern.
namespace Student.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradesController: ControllerBase
    {
        private readonly ILogger<GradesController> _logger;

        private readonly IGradeRepository _gradeRepository;
        private readonly IPupilRepository _pupilRepository;
        private readonly ISubjectRepository _subjectRepository;
         private readonly StudentNotifier _notifier; 


        public GradesController(
            ILogger<GradesController> logger,
            IGradeRepository gradeRepository,
            IPupilRepository pupilRepository,
            ISubjectRepository subjectRepository,
            StudentNotifier notifier)
        {
            this._logger = logger;

            this._gradeRepository = gradeRepository;
            _pupilRepository = pupilRepository;
            this._subjectRepository = subjectRepository;
            _notifier = notifier;

        }
        [HttpGet("by-subject/{subjectId}")]
    public async Task<IActionResult> GetListBySubject(int subjectId)
        {
    var subject = await _subjectRepository.GetById(subjectId);
    if (subject == null)
    {
        return NotFound("Subject not found");
    }

    var grades = await _gradeRepository.GetAllBySubjectIdAsync(subjectId);

    // convert to DTO
    var subjectToReturn = new SubjectDto
    {
        Code = subject.Code,
        Id = subject.Id,
        Title = subject.Title,
        Grades = new List<GradeDto>()
    };

    foreach (var item in grades ?? Enumerable.Empty<Grade>()) // Handling potential null value in grades
{
    var newGrade = new GradeDto
    {
        PupilId = item.PupilId,
        Pupil = $"{item.Pupil?.LastName}, {item.Pupil?.FirsName}", // Handling null Pupil
        SubjectId = item.SubjectId,
        MidTerm = item.MidTerm ?? string.Empty, // Handling null with fallback to empty string
        FinalTerm = item.FinalTerm ?? string.Empty, // Same as above
        FinalGrade = item.FinalGrade ?? string.Empty, // Same as above
        Remarks = item.Remarks ?? string.Empty // Same as above
    };

    subjectToReturn.Grades.Add(newGrade);
}

    return Ok(subjectToReturn);
}


        [HttpPost()]
        public async Task<IActionResult> Post(GradeDto input)
        {
            var newGrade = new Grade();
            newGrade.PupilId = input.PupilId;
            newGrade.SubjectId = input.SubjectId;
            newGrade.MidTerm = input.MidTerm;
            newGrade.FinalTerm = input.FinalTerm;
            newGrade.FinalGrade = input.FinalGrade;
            newGrade.Remarks = input.Remarks;


            _gradeRepository.Add(newGrade);

            if ( await _gradeRepository.SaveAllChangesAsync())
            { 
                var student = await _pupilRepository.GetById(newGrade.PupilId);
                if (student != null)
            {
                _notifier.NotifyGradeUpdate(student, newGrade);
            }
                return Ok(input);
            }

            return BadRequest("May Error");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(GradeDto input)
        {
            var grade = await _gradeRepository.GetById(input.Id);
            if (grade == null)
    {
            return NotFound("Grade not found");
    }
            grade.PupilId = input.PupilId;
            grade.SubjectId = input.SubjectId;
            grade.MidTerm = input.MidTerm;
            grade.FinalTerm = input.FinalTerm;
            grade.FinalGrade = input.FinalGrade;
            grade.Remarks = input.Remarks;
            
            if ( await _gradeRepository.SaveAllChangesAsync())
            {
                return Ok("Updated Na!");
            }

            return BadRequest("May Error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var grade = await _gradeRepository.GetById(id);
            
            if (grade != null) 
            {
                _gradeRepository.Delete(grade);
                if ( await _gradeRepository.SaveAllChangesAsync())
                {
                    return Ok("Finis Na!");
                }
            }


            return BadRequest("May Error");
        }

    }
}