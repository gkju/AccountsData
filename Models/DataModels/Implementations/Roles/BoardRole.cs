﻿using System;
using AccountsData.Models.DataModels.Abstracts;
using AccountsData.Models.DataModels.Implementations.RoleScope;
using AccountsData.Models.DataModels.RoleProperties;


namespace AccountsData.Models.DataModels.Implementations.Roles
{

        public class BoardRole : Role
        {
            public string Name { get; set; }

            public BoardRole()
            {
                
            }
            public BoardRole(DataModels.Properties properties, Board board, ApplicationUser author, string roleName)
            {
                Name = roleName;
                Scope = new BoardScope(board);
                Id = Guid.NewGuid();
                int authorAuthority = author.GetAuthority(Scope);
                this.Properties = new GenericRoleProperties(properties, authorAuthority);
            }
            
        }
    
}