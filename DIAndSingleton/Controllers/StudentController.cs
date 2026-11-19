using Microsoft.AspNetCore.Mvc;

namespace DIAndSingleton.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        StudentOperations _handler;

        public StudentController(StudentOperations handler)
        {
            _handler = handler;
        }


        [HttpGet]
        public IActionResult GetStudents([FromQuery] string? album)
        {
            var students = _handler.GetStudents(album);
            if (students.Count == 0)
                return NoContent();

            return Ok(students);

        }



        [HttpPost]
        public IActionResult AddNewStudent([FromBody] CreateNewStudentCommand command)
        {
            var result = _handler.AddNewStudent(command);
            if (result.Success)
                return Ok(result.Data);
            else
            {
                if (result.Error!.Contains("już istnieje"))
                    return Conflict(result.Error);
                else
                    return BadRequest(result.Error);
            }
                

        }
    }
}
