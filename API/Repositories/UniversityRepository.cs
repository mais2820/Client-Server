using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace API.Repositories
{
    public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingDbContext context) : base(context) { }

        public Guid GetLastUniversityGuid()
        {
            return _context.Set<University>().ToList().LastOrDefault().Guid;
        }

    }
        
}
