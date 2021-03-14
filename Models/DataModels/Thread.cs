using System;
using System.ComponentModel.DataAnnotations;
using AccountsData.Models.DataModels.Helpers;
using AccountsData.Models.DataModels.Implementations.Properties;

namespace AccountsData.Models.DataModels
{
    public class Thread
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Board board { get; set; }
        [Required]
        public int boardId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Desc { get; set; }
        
        [Required]
        public bool inheritProperties { get; set; }

        [Required] public Properties defaultProperties { get; set; } = new Properties();

        [Required] public Properties memberProperties { get; set; } = new Properties();
        
        public Properties GetProperties(ApplicationUser user)
        {
            if (inheritProperties)
            {
                return board.GetProperties(user);
            }
            else
            {
                Properties userProperties = user.GetProperties(this.board.scope);
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
}