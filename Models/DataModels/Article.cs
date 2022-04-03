using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountsData.Models.DataModels
{
    public class Article
    {

        public ApplicationUser AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Public {
            get
            {
                if (AutoPublish && DateTime.Now > AutoPublishOn)
                {
                    Public = true;
                    return true;
                }

                return Public;
            }
            set
            {
                try
                {
                    Public = value;
                }
                catch
                {
                    throw new ArgumentException("An attempt to set Article.Public to null has been made");
                }
                
            }
        }

        public List<ApplicationUser> Editors { get; set; } = new List<ApplicationUser>();
        
        public List<ApplicationUser> Reviewers { get; set; } = new List<ApplicationUser>();

        public EditorjsPost PublicContent { get; set; } = new EditorjsPost();

        public bool AutoPublish { get; set; } = false;

        public DateTime AutoPublishOn { get; set; } = new DateTime();

        public List<Draft> Drafts { get; set; } = new List<Draft>();
    }
}