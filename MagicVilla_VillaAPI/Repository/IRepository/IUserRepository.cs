using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IUserRepository : IRepository<LocalUser>
    {
        Task<bool> IsUserUnique(string userName);

        Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    }
}
