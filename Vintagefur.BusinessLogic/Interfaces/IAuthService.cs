using Vintagefur.Domain.DTO;

namespace Vintagefur.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        AuthResultDto Login(string email, string password);
        SignOutResultDto Logout();
        bool Register(string email, string password, string firstName, string lastName);
    }
} 