
using System;

namespace HexagonalSchoolApp.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public Guid SchoolId { get; set; }
    }
}
