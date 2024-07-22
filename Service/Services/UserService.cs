using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWorks;

namespace Service.Services
{
    public class UserService :  Service<User, UserDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
 

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository) 
            : base(repository,unitOfWork,mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<CustomResponseDto<IEnumerable<UserDto>>> GetActiveUser()
        {
            var users =await _userRepository.GetActiveUser();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return CustomResponseDto<IEnumerable<UserDto>>.Success(200, userDtos);
        }

        public async Task<CustomResponseDto<IEnumerable<UserDto>>> GetUserGetUsersByCreatedDateBetween(DateTime startDate, DateTime endDate)
        {
            var users = await _userRepository.GetUserGetUsersByCreatedDateBetween(startDate, endDate);
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return CustomResponseDto<IEnumerable<UserDto>>.Success(200, userDtos);
        }
    }
}
