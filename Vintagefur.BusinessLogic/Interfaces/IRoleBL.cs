using System.Collections.Generic;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Interfaces
{
    public interface IRoleBL
    {
        Role GetRoleById(int roleId);
        Role GetRoleByName(string roleName);
        List<Role> GetAllRoles();
        bool CreateRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRole(int roleId);
    }
} 