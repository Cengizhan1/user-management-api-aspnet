using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Dtos.BaseControllerResponse;
using Core.Entities;
using Core.Mapper;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UserService(
            IUserRepository repository,
            IRabbitMqService rabbitMqService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userRepository = repository;
            _rabbitMqService = rabbitMqService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomDataResponse<UserDto>> GetAllAsync()
        {
            var entities = await _userRepository.GetAll().ToListAsync();

            var dtos = _mapper.Map<IEnumerable<UserDto>>(entities);

            return CommonResponseMapper.ToCustomDataListResponse(dtos, true);
        }

        public async Task<CustomDataResponse<UserDto>> AddAsync(UserCreateRequest dto)
        {
            var newUser = _mapper.Map<User>(dto);
            await _userRepository.AddAsync(newUser);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<UserDto>(newUser);
            _rabbitMqService.SendMessage("User created: " + UserDtoToString(newUser));
            return CommonResponseMapper.ToCustomDataResponse(newDto, true);

        }

        public async Task<CustomApiResponse> UpdateAsync(UserUpdateRequest dto)
        {
            var user = await FindEntityById(dto.Id);
            _userRepository.Update(user);

            await _unitOfWork.CommitAsync();

            _rabbitMqService.SendMessage("User Updated: " + UserDtoToString(user));

            return CommonResponseMapper.ToCustomApiResponse(true, HttpStatusCode.Created);
        }
        public async Task<CustomDataResponse<UserDto>> GetByIdAsync(int id)
        {
            var dto = _mapper.Map<UserDto>(await FindEntityById(id));

            return CommonResponseMapper.ToCustomDataResponse(dto, true);
        }

        public async Task<CustomApiResponse> RemoveAsync(int id)
        {
            _userRepository.Remove(await FindEntityById(id));
            await _unitOfWork.CommitAsync();

            return CommonResponseMapper.ToCustomApiResponse(true, HttpStatusCode.OK);
        }

        public async Task<CustomDataResponse<UserDto>> GetActiveUser()
        {
            var users = await _userRepository.GetActiveUser();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return CommonResponseMapper.ToCustomDataListResponse(userDtos, true);

        }

        public async Task<CustomDataResponse<UserDto>> GetUserGetUsersByCreatedDateBetween(
            DateTime startDate,
            DateTime endDate)
        {
            var users = await _userRepository.GetUserGetUsersByCreatedDateBetween(startDate, endDate);
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return CommonResponseMapper.ToCustomDataListResponse(userDtos, true);
        }

        private string UserDtoToString(User user)
        {
            return "Name :   " + user.Name + "  |   " +
                "Surname :   " + user.Surname + "  |   " +
                "Age :   " + user.Age;
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
