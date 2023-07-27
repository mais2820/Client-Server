using API.Contracts;
using API.Data;
using API.DTOs.AccountDto;
using API.Models;
using API.Utilities.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingDbContext context) : base(context) { }

        public bool IsNotExist(string value)
        {
            return _context.Set<Employee>()
                            .SingleOrDefault(e => e.Email.Contains(value) 
                            || e.PhoneNumber.Contains(value)) is null;
        }

        public string GetLastNik()
        {
            return _context.Set<Employee>().ToList().LastOrDefault()?.NIK;
        }

        public Employee GetByEmail(string email)
        {
            return _context.Set<Employee>().SingleOrDefault(e => e.Email.Contains(email));
        }

        public Employee? CheckEmail(string email)
        {
            return _context.Set<Employee>().FirstOrDefault(e => e.Email == email);
        }

        public Guid GetLastEmployeeGuid()
        {
            return _context.Set<Employee>().ToList().LastOrDefault().Guid;
        }

    }
}
