using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsData.Models.DataModels;

public class Tag
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [System.Text.Json.Serialization.JsonIgnore]
    public List<Article> Articles { get; set; }
    
    public ApplicationUser Author { get; set; }
    public string Content { get; set; }
    
    public static string NormalizeTagString(string tagString)
    {
        if (tagString.Length == 0)
        {
            return tagString;
        }

        StringBuilder sb = new StringBuilder();

        if (Char.IsAscii(tagString[0]))
        {
            sb.Append(Char.ToUpper(tagString[0]));
        }

        foreach (char c in tagString[1..])
        {
            if (Char.IsAscii(c))
            {
                sb.Append(char.ToLower(c));
            }
        }

        return sb.ToString();
    }
}