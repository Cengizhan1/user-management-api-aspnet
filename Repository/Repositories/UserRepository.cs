using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Repositories;

namespace Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context)
        {
        }
        public Task<IEnumerable<User>> GetActiveUser()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUserGetUsersByCreatedDateBetween(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
