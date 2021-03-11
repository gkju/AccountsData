using System;
using System.ComponentModel.DataAnnotations;
using AccountsData.Models.DataModels.Implementations.RoleScope;

namespace AccountsData.Models.DataModels
{
    public class Board
    {
        public Board(string name)
        {
            ID = Guid.NewGuid();
            Name = name;
            scope = new BoardScope(this);
        }

        [Required] 
        readonly Guid ID;
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public BoardScope scope;

        //default properties every user should get
        [Required]
        public Properties defaultProperties { get; set; }
        
        //thread will use these properties if set to
        [Required]
        public Properties defaultTheadProperties { get; set; }
    }
}