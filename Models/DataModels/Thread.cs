using System.ComponentModel.DataAnnotations;

namespace AccountsData.Models.DataModels
{
    public class Thread
    {
        [Required]
        private Board Parent { get; set; }
        
        [Required]
        private string Name { get; set; }
        
        [Required]
        private string Desc { get; set; }
        
        [Required]
        private bool inheritProperties { get; set; }
    }
}