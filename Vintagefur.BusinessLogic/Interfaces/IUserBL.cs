using System.Collections.Generic;
using Vintagefur.Domain.DTO;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Interfaces
{
    public interface IUserBL
    {
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        List<User> GetAllUsers();
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        AuthResultDto AuthenticateUser(string email, string password);
        UserProfileDto GetUserProfile(int userId);
        SignOutResultDto SignOut();
    }
} 