using System.Collections.Generic;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Repositories;

namespace Vintagefur.BusinessLogic.BLogic
{
    public class RoleBL : IRoleBL
    {
        private readonly RoleRepository _roleRepository;

        public RoleBL()
        {
            _roleRepository = new RoleRepository();
        }

        public Role GetRoleById(int roleId)
        {
            return _roleRepository.GetById(roleId);
        }

        public Role GetRoleByName(string roleName)
        {
            return _roleRepository.GetByName(roleName);
        }

        public List<Role> GetAllRoles()
        {
            return _roleRepository.GetAll();
        }

        public bool CreateRole(Role role)
        {
            return _roleRepository.Create(role);
        }

        public bool UpdateRole(Role role)
        {
            return _roleRepository.Update(role);
        }

        public bool DeleteRole(int roleId)
        {
            return _roleRepository.Delete(roleId);
        }
    }
} 