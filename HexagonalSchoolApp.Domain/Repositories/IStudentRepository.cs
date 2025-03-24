
using HexagonalSchoolApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HexagonalSchoolApp.Domain.Entities;

namespace HexagonalSchoolApp.Domain.Repositories
{
    public interface IStudentRepository
    {
        Task SaveAsync(Student student);
        Task<Student?> GetByIdAsync(Guid id);
        Task<IEnumerable<Student>> GetAllAsync();
    }
}
