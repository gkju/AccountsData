using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AccountsData.Data;
using AccountsData.Models.DataModels;

namespace AccountsData.Models.DataModels
{
    public class MeiliArticle
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ContentText { get; set; }
        public List<string> Tags { get; set; }
        public string AuthorUserName { get; set; }
        public string AuthorEmail { get; set; }
    }
    
    public class Article
    {
        [Key] public string ArticleId { get; set; } = Guid.NewGuid().ToString();

        public ApplicationUser Author { get; set; }

        public string Title { get; set; } = "";

        public List<Tag> Tags { get; set; } = new ();

        public DateTime ModifiedOn { get; set; }

        public bool IsPublic()
        {
            if (AutoPublish && DateTime.Now > AutoPublishOn)
            {
                Public = true;
                return true;
            }

            return Public;
        }

        public bool Public { get; set; } = false;

        public List<ApplicationUser> Editors { get; set; } = new List<ApplicationUser>();
        
        public List<ApplicationUser> Reviewers { get; set; } = new List<ApplicationUser>();

        public string ContentJson { get; set; } = "";
        public string ContentText { get; set; } = "";
        public List<Revision> Revisions { get; set; } = new ();

        public bool AutoPublish { get; set; } = false;

        public DateTime AutoPublishOn { get; set; } = new ();
        
        public File Picture { get; set; }

        public Article()
        {
            
        }

        public bool UserCanEdit(ApplicationUser user)
        {
            if(Editors.Contains(user) || Author == user)
            {
                return true;
            }
            
            return false;
        }
        
        public bool UserCanReview(ApplicationUser user)
        {
            if(Reviewers.Contains(user) || Author == user)
            {
                return true;
            }
            
            return UserCanEdit(user);
        }

        public bool UserCanView(ApplicationUser user)
        {
            if (Public)
            {
                return true;
            }

            return UserCanReview(user);
        }

        public async Task HandleRevision(string ContentJson, ApplicationUser user, ApplicationDbContext db)
        {
            if (!UserCanEdit(user))
            {
                throw new ArgumentException("User cannot edit this article");
            }

            var newRevision = new Revision
            {
                Author = user,
                ContentJson = ContentJson,
                ModifiedOn = DateTime.Now.ToUniversalTime()
            };

            var oldRevisions = Revisions.Where(r => r.Author == user).ToList();
            if (oldRevisions.Count == 0)
            {
                Revisions.Add(newRevision);
                return;
            }
            var revision = oldRevisions.MaxBy(r => r.ModifiedOn);
            var revisionMin = oldRevisions.MinBy(r => r.ModifiedOn);

            if (revision.ModifiedOn.AddMinutes(10) < DateTime.Now.ToUniversalTime())
            {
                if (oldRevisions.Count > 5)
                {
                    Revisions.Remove(revisionMin);
                }
                Revisions.Add(newRevision);
                await db.SaveChangesAsync();
            }
        }

        public MeiliArticle GetMeiliArticle()
        {
            return new MeiliArticle()
            {
                Id = ArticleId,
                AuthorEmail = Author.Email,
                AuthorUserName = Author.UserName,
                ContentText = ContentText,
                Tags = Tags.Select(t => t.Content).ToList(),
                Title = Title
            };
        }
    }
}

public class Revision
{
    [Key]
    public string RevisionId { get; set; } = Guid.NewGuid().ToString();
    
    public ApplicationUser Author { get; set; }
    public string ContentJson { get; set; }
    public DateTime ModifiedOn { get; set; }
    
    public string ArticleId { get; set; }
    public Article Article { get; set; }
}