using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;

namespace AccountsData.Models.DataModels;

public class Comment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public ApplicationUser Author { get; set; }
    public string Content { get; set; }
    public File? File { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now.ToUniversalTime();
    public DateTime ModifiedOn { get; set; } = DateTime.Now.ToUniversalTime();
    public Comment? InReplyTo { get; set; }

    public List<Reaction> Reactions { get; set; } = new();
}