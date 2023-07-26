using API.Models;
using API.Utilities.Enums;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        bool IsNotExist(string value);
        string GetLastNik();
        Employee? GetByEmail(string email);
        Guid GetLastEmployeeGuid();
    } 
}
