using System;
using System.ComponentModel.DataAnnotations;
using AccountsData.Models.DataModels.Helpers;
using AccountsData.Models.DataModels.Implementations.Properties;
using AccountsData.Models.DataModels.Implementations.RoleScope;

namespace AccountsData.Models.DataModels
{
    public class Board
    {
        public Board(string name)
        {
            Name = name;
            scope = new BoardScope(this);
        }

        [Required]
        [Key]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public BoardScope scope;

        //default properties every user should get
        [Required]
        public Properties defaultProperties { get; set; }
        
        [Required]
        public Properties memberProperties { get; set; }

        public Properties GetProperties(ApplicationUser user)
        {
            Properties userProperties = user.GetProperties(this.scope);
            if (userProperties.ContainsPropertyCalled(MemberProperty.Name) && (SimpleBoolProperty) userProperties[MemberProperty.Name])
            {
                foreach (var property in memberProperties)
                {
                    userProperties.InsertOrMerge(property);
                }
            }
            else
            {
                foreach (var property in defaultProperties)
                {
                    userProperties.InsertOrMerge(property);
                }
            }

            return userProperties;
        }
    }
}