using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.EmployeeDto
{
    public class NewEmployeeDto
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public static implicit operator Employee(NewEmployeeDto newEmployeeDto)
        {
            return new Employee
            {
                Guid = new Guid(),
                NIK = newEmployeeDto.NIK,
                FirstName = newEmployeeDto.FirstName,
                LastName = newEmployeeDto.LastName,
                BirthDate = DateTime.Now,
                Gender = newEmployeeDto.Gender,
                HiringDate = DateTime.Now,
                Email = newEmployeeDto.Email,
                PhoneNumber = newEmployeeDto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewEmployeeDto(Employee employee)
        {
            return new NewEmployeeDto
            {
                NIK = employee.NIK,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = DateTime.Now,
                Gender = employee.Gender,
                HiringDate = DateTime.Now,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber
            };
        }
    }
}
