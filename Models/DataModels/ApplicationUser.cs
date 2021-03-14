using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AccountsData.Models.DataModels.Abstracts;
using AccountsData.Models.DataModels.Helpers;
using AccountsData.Models.DataModels.Implementations.Properties;
using AccountsData.Models.DataModels.Implementations.Roles;
using Microsoft.AspNetCore.Identity;

namespace AccountsData.Models.DataModels
{
    public class UserRole {
        public Role role { get; set; }
        public DateTime expiresAt { get; set; }
        public bool expire { get; set; }
    }
    public partial class ApplicationUser : IdentityUser
    {

        private List<UserRole> UserRoles { get; set; } = new List<UserRole>();
        
        public List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            for(int i = 0; i < UserRoles.Count; ++i)
            {
                var userRole = UserRoles[i];
                if (userRole.expire && DateTime.Now >= userRole.expiresAt)
                {
                    UserRoles.RemoveAt(i);
                }
                else
                {
                    roles.Add(userRole.role);
                }
            }

            return roles;
        }
        
        public List<Role> GetRoles(Scope scope)
        {
            List<Scope> scopes = scope.GetAllParentScopesIncludingSelf();
            List<Role> scopedRoles = GetRoles();
            scopedRoles = scopedRoles.Where(role => scopes.Contains(role.scope)).ToList();

            return scopedRoles;
        }

        public void AddRole(UserRole role)
        {
            for(int i = 0; i < UserRoles.Count; ++i)
            {
                var userRole = UserRoles[i];
                if (userRole.role == role.role)
                {
                    UserRoles.RemoveAt(i);
                }
            }
            
            UserRoles.Add(role);
        }

        public void RemoveRoles(UserRole role)
        {
            for(int i = 0; i < UserRoles.Count; ++i)
            {
                var userRole = UserRoles[i];
                if (userRole.role == role.role)
                {
                    UserRoles.RemoveAt(i);
                }
            }
        }

        public Properties GetProperties(Scope scope)
        {
            List<Role> recievedRoles = new List<Role>(GetRoles(scope));
            Properties properties = new Properties(recievedRoles);
            return properties;
        }

        public int GetAuthority(Scope scope)
        {
            try
            {
                return ((AuthorityProperty) GetProperties(scope)[AuthorityProperty.Name]).GetValue();
            }
            catch
            {
                return 0;
            }
        }

        public bool MayManageRole(Role role)
        {
            //use != true for better readability
            if (role.UserManageable != true)
            {
                return false;
            }
            
            try
            {
                var properties = GetProperties(role.scope);

                var userIsAdmin = ContainsAdmin(properties);

                if (ContainsAdmin(role.properties) && !userIsAdmin)
                {
                    return false;
                }

                if ((MayManageRolesProperty) properties[MayManageRolesProperty.Name] != true)
                {
                    return false;
                }

                if ((AuthorityProperty) properties[AuthorityProperty.Name] <=
                    (AuthorityProperty) role.properties[AuthorityProperty.Name])
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static bool ContainsAdmin(Properties properties)
        {
            try
            {
                return (AdminProperty) properties[AdminProperty.Name];
            }
            catch
            {
                return false;
            }
            
        }

        public bool HasAuthorityOverUser(ApplicationUser user2, Scope scope)
        {
            var ownProps = GetProperties(scope);
            var foreignProps = user2.GetProperties(scope);
            var amAdmin = ContainsAdmin(ownProps);
            var foreignIsAdmin = ContainsAdmin(foreignProps);

            if (!amAdmin && foreignIsAdmin)
            {
                return false;
            }

            return (AuthorityProperty) ownProps[AuthorityProperty.Name] > (AuthorityProperty) foreignProps[AuthorityProperty.Name];
        }

        public bool MayBan(ApplicationUser user2, Scope scope)
        {
            if (!HasAuthorityOverUser(user2, scope))
            {
                return false;
            }

            var ownProps = GetProperties(scope);
            if (!(SimpleBoolProperty) ownProps[BanUsersProperty.Name])
            {
                return false;
            }

            return true;
        }

        public bool MayWrite(Scope scope)
        {
            var ownProps = GetProperties(scope);
            return (SimpleBoolProperty) ownProps[WriteProperty.Name];
        }
        
        public bool MayRead(Scope scope)
        {
            var ownProps = GetProperties(scope);
            return (SimpleBoolProperty) ownProps[ReadProperty.Name];
        }

        public bool MayEditOrDelete(Scope scope)
        {
            var ownProps = GetProperties(scope);
            return (SimpleBoolProperty) ownProps[EditOrDeleteProperty.Name];
        }

        public void BanUser(ApplicationUser user2, Scope scope, bool expires = false, DateTime expireAt = new DateTime())
        {
            if (!MayBan(user2, scope))
            {
                return;
            }
            
            user2.AddRole(new UserRole {
               role = new BannedRole(scope),
               expire = expires,
               expiresAt = expireAt
            });
        }

        public void UnBanUser(ApplicationUser user2, Scope scope, bool expires = false,
            DateTime expireAt = new DateTime())
        {
            if (!MayBan(user2, scope))
            {
                return;
            }
            
            for(int i = 0; i < user2.UserRoles.Count; ++i)
            {
                var userRole = user2.UserRoles[i];
                if (userRole.role.GetType() == typeof(BannedRole) && userRole.role.scope == scope)
                {
                    user2.UserRoles.RemoveAt(i);
                }
            }
        }

        //duplicate
        public bool HasTrueProperty(Property property, Scope scope)
        {
            try
            {
                return (SimpleBoolProperty) GetProperties(scope)[Properties.GetNameFromInstance(property)];
            }
            catch
            {
                return false;
            }
        }
    }
}