using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Repositories
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetActiveUser();
        Task<IEnumerable<User>> GetUserGetUsersByCreatedDateBetween(
            DateTime startDate, 
            DateTime endDate);
    }
}
