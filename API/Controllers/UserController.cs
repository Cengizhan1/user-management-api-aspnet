﻿using Core.Dtos;
using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<CustomResponseDto<IEnumerable<UserDto>>> GetAll()
        {
            return await service.GetAllAsync();
        }


        [HttpPost]
        public async Task<CustomResponseDto<UserDto>> Save(UserDto request)
        {
            return await service.AddAsync(request);
        }

        [HttpPut]
        public async Task<CustomResponseDto<NoContentDto>> Update(UserDto request)
        {
            return await service.UpdateAsync(request);
        }


        [HttpDelete]
        public async Task<CustomResponseDto<NoContentDto>> Delete(int id)
        {
            return await service.RemoveAsync(id);
        }

        [HttpGet]
        public async Task<CustomResponseDto<IEnumerable<UserDto>>> GetActiveUsers()
        {
            return await service.GetActiveUser();
        }

        [HttpGet]
        public async Task<CustomResponseDto<IEnumerable<UserDto>>> GetUserGetUsersByCreatedDateBetween(DateTime startDate, DateTime endDate)
        {
            return await service.GetUserGetUsersByCreatedDateBetween(startDate,endDate);
        }

    }
}
