using System.Linq.Expressions;
using Core.Dtos;
using Core.Entities;

namespace Core.Services
{
    public interface IService<Entity, Dto> where Entity : BaseEntity where Dto : class
    {
        Task<CustomResponseDto<Dto>> GetByIdAsync(int id);

        Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync();

        Task<CustomResponseDto<Dto>> AddAsync(Dto dto);

        Task<CustomResponseDto<NoContentDto>> UpdateAsync(Dto dto);

        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);
    }
}
