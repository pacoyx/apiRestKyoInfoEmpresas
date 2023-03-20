using apiRestKyoInfoEmpresas.Models.DTO;

namespace apiRestKyoInfoEmpresas.Services
{
    public interface ILoginService
    {
        Task<LoginResponseDto> Login(string email, string pwd);
    }
}
