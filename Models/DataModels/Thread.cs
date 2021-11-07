using System;
using System.ComponentModel.DataAnnotations;

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
    }
}