using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<User>> GetActiveUser()
        {
            return await _context.Users.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUserGetUsersByCreatedDateBetween(
            DateTime startDate, 
            DateTime endDate)
        {
            return await _context.Users
                .Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                .ToListAsync();
        }
    }
}
