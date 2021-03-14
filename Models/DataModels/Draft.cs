using System.ComponentModel.DataAnnotations;

namespace AccountsData.Models.DataModels
{
    public class Draft
    {
        [Required]
        public Article Article { get; set; }
        [Required]
        public int ArticleId { get; set; }
        [Required]
        public EditorjsPost Contents { get; set; }
        [Required]
        public ApplicationUser Author { get; set; }
    }
}