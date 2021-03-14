using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AccountsData.Models.DataModels.Implementations.RoleScope;

namespace AccountsData.Models.DataModels
{
    public class Article
    {
        [Required]
        public ApplicationUser Author { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
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

        public bool UserMayRead(ApplicationUser user = null)
        {
            if (Public)
            {
                return true;
            }

            try
            {
                return Editors.Contains(user) || Reviewers.Contains(user) || user.MayRead(new ArticlesScope());
            }
            catch
            {
                return false;
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