using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Dtos.BaseControllerResponse;
using Core.Entities;

namespace Core.Services
{
    public interface IUserService
    {
        Task<CustomDataResponse<UserDto>> AddAsync(UserCreateRequest dto);

        Task<CustomApiResponse> UpdateAsync(UserUpdateRequest dto);

        Task<CustomDataResponse<UserDto>> GetByIdAsync(int id);

        Task<CustomDataResponse<UserDto>> GetAllAsync();

        Task<CustomApiResponse> RemoveAsync(int id);

        Task<CustomDataResponse<UserDto>> GetActiveUser();
        Task<CustomDataResponse<UserDto>> GetUserGetUsersByCreatedDateBetween(
            DateTime startDate,
            DateTime endDate);

    }
}
