
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using HexagonalSchoolApp.Domain.Entities;
using HexagonalSchoolApp.Domain.Repositories;


namespace HexagonalSchoolApp.Infrastructure.Persistence
{
    public class InMemoryStudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new();

        public Task SaveAsync(Student student)
        {
            _students.Add(student);
            return Task.CompletedTask;
        }

        public Task<Student?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_students.FirstOrDefault(s => s.Id == id));
        }

        public Task<IEnumerable<Student>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Student>>(_students);
        }
    }
}
