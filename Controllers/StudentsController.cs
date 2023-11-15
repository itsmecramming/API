using Microsoft.AspNetCore.Mvc;
using Student.Web.Api.Data;
using Student.Web.Api.Dto;
using Student.Web.Api.Models;
//Creational Design Pattern because it is Abstract Factory Pattern.
namespace Student.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController: ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IPupilRepository _pupilRepository;
        private readonly StudentNotifier _notifier;


        public StudentsController(
            ILogger<StudentsController> logger,
            IPupilRepository pupilRepository,
            StudentNotifier notifier)
        {
            _logger = logger;
            _pupilRepository = pupilRepository;
            _notifier = notifier;
        }

        [HttpGet()]
        public async Task<IActionResult> GetList()
        {
            var pupils = await _pupilRepository.GetAllAsync();
            _logger.LogInformation("Getting all list");
            return Ok(pupils);
        }

        [HttpPost()]
        public async Task<IActionResult> Post(PupilDto input)
        {
            var newPupil = new Pupil(input.StudentId);
            newPupil.LastName = input.LastName;
            newPupil.FirsName = input.FirsName;
            newPupil.MiddleName = input.MiddleName;

            _pupilRepository.Add(newPupil);

            if ( await _pupilRepository.SaveAllChangesAsync())
            {
                _notifier.NotifyNewStudentRegistration(newPupil);
                return Ok(input);
            }

            return BadRequest("May Error");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(PupilDto input)
        {
            var pupil = await _pupilRepository.GetById(input.StudentId);
            pupil.LastName = input.LastName;
            pupil.FirsName = input.FirsName;
            pupil.MiddleName = input.MiddleName;
            
            if ( await _pupilRepository.SaveAllChangesAsync())
            {
                return Ok("Updated Na!");
            }

            return BadRequest("May Error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var pupil = await _pupilRepository.GetById(id);
            
            if (pupil != null) 
            {
                _pupilRepository.Delete(pupil);
                if ( await _pupilRepository.SaveAllChangesAsync())
                {
                    return Ok("Finis Na!");
                }
            }


            return BadRequest("May Error");
        }


    }
}