
using HexagonalSchoolApp.Application.UseCases;
using HexagonalSchoolApp.Domain.Repositories;
using HexagonalSchoolApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HexagonalSchoolApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repo;
        private readonly CreateStudent _useCase;

        public StudentController()
        {
            _repo = new InMemoryStudentRepository();
            _useCase = new CreateStudent(_repo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
        {
            await _useCase.ExecuteAsync(request.Name, request.SchoolId);
            return Ok(new { message = "Student created!" });
        }

        public class CreateStudentRequest
        {
            public string Name { get; set; } = "";
            public Guid SchoolId { get; set; }
        }
    }
}
