using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Services
{
    public interface IUserService : IService<User,UserDto>
    {
        Task<CustomResponseDto<IEnumerable<UserDto>>> GetActiveUser();
        Task<CustomResponseDto<IEnumerable<UserDto>>> GetUserGetUsersByCreatedDateBetween(DateTime startDate, DateTime endDate);
    }
}
