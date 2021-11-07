using System;
using System.ComponentModel.DataAnnotations;

namespace AccountsData.Models.DataModels
{
    public class Board
    {
        public Board(string name)
        {
            Name = name;
        }

        [Required]
        [Key]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}