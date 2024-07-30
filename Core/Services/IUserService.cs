using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Services
{
    public interface IUserService
    {
        Task<CustomResponseDto<UserDto>> AddAsync(UserCreateRequest dto);

        Task<CustomResponseDto<NoContentDto>> UpdateAsync(UserUpdateRequest dto);

        Task<CustomResponseDto<UserDto>> GetByIdAsync(int id);

        Task<CustomResponseDto<IEnumerable<UserDto>>> GetAllAsync();

        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);

        Task<CustomResponseDto<IEnumerable<UserDto>>> GetActiveUser();
        Task<CustomResponseDto<IEnumerable<UserDto>>> GetUserGetUsersByCreatedDateBetween(
            DateTime startDate,
            DateTime endDate);

    }
}
