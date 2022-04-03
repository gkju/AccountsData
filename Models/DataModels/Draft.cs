using System.ComponentModel.DataAnnotations;

namespace AccountsData.Models.DataModels
{
    public class Draft
    {
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        
        public string ArticleId { get; set; }
        public Article Article { get; set; }
        
        public EditorjsPost ContentsId { get; set; }
        public EditorjsPost Contents { get; set; }
    }
}