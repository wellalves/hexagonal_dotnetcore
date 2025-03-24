using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HexagonalSchoolApp.Domain.Entities;

using HexagonalSchoolApp.Domain.Entities;
using HexagonalSchoolApp.Domain.Repositories;


namespace HexagonalSchoolApp.Application.UseCases
{
    public class CreateStudent
    {
        private readonly IStudentRepository _repository;

        public CreateStudent(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(string name, Guid schoolId)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = name,
                SchoolId = schoolId
            };

            await _repository.SaveAsync(student);
        }
    }
}
