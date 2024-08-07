using Core.Dtos;
using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class UserController : CustomBaseController
    {
        private readonly IUserService service;
        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ApiResponse(await service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return ApiResponse(await service.GetByIdAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Save(UserCreateRequest request)
        {
            return ApiResponse();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            await service.UpdateAsync(request);
            return ApiResponse();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.RemoveAsync(id);
            return ApiResponse();
        }

        [HttpGet("getActiveUsers")]
        public async Task<IActionResult> GetActiveUsers()
        {
            return ApiResponse(await service.GetActiveUser());
        }

        [HttpGet("getUsersByCreatedDateBetween")]
        public async Task<IActionResult> GetUsersByCreatedDateBetween(
            DateTime startDate, 
            DateTime endDate)
        {
            return ApiResponse(await service.GetUserGetUsersByCreatedDateBetween(startDate, endDate));
        }

    }
}
