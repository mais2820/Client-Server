using API.Models;
using API.Utilities.Enums;

namespace API.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<University>
    {
        Guid GetLastUniversityGuid();

        University? GetByCode(string code);
    }
}
