using System;
using System.Linq;
using System.Web.Security;
using Vintagefur.BusinessLogic;
using Vintagefur.BusinessLogic.BLogic;

namespace Vintagefur.Web.Infrastructure
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly UserBL _userBL;
        private readonly RoleBL _roleBL;

        public CustomRoleProvider()
        {
            var factory = BusinessLogicFactory.Instance;
            _userBL = factory.GetUserBL() as UserBL;
            _roleBL = factory.GetRoleBL() as RoleBL;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = _userBL.GetUserByEmail(username);
            if (user != null && user.Role != null)
            {
                return string.Equals(user.Role.Name, roleName, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = _userBL.GetUserByEmail(username);
            if (user != null && user.Role != null)
            {
                return new[] { user.Role.Name };
            }
            return new string[] { };
        }

        public override string ApplicationName { get; set; }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            var roles = _roleBL.GetAllRoles();
            return roles.Select(r => r.Name).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            var roles = _roleBL.GetAllRoles();
            return roles.Any(r => string.Equals(r.Name, roleName, StringComparison.OrdinalIgnoreCase));
        }
    }
} 