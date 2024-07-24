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
using Service.Exceptions;

namespace Service.Services
{
    public class UserService :  Service<User, UserDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRabbitMqService _rabbitMqService;


        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository,
            IRabbitMqService rabbitMqService) 
            : base(repository,unitOfWork,mapper)
        {
            _userRepository = userRepository;
            _rabbitMqService = rabbitMqService;
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

        public async Task<CustomResponseDto<UserDto>> AddAsync(UserCreateRequest dto)
        {
            var newUser = _mapper.Map<User>(dto);
            await _userRepository.AddAsync(newUser);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<UserDto>(newUser);
            _rabbitMqService.SendMessage("User created: " + UserDtoToString(newUser));
            return CustomResponseDto<UserDto>.Success(200, newDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(UserUpdateRequest dto)
        {
            var user = await FindEntityById(dto.Id);
            _userRepository.Update(user);

            await _unitOfWork.CommitAsync();

            _rabbitMqService.SendMessage("User Updated: " + UserDtoToString(user));

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        private string UserDtoToString(User user)
        {
            return "Name :   " + user.Name + " Surname :   " + user.Surname + " Age :   " + user.Age;
        }

        private async Task<User> FindEntityById(int id)
        {
            var entity = await _userRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }
            return entity;
        }

    }
}
