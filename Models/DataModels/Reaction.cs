using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AccountsData.Models.DataModels;

public enum ReactionType
{
    Like,
    Dislike,
    Kerfus,
    Saul,
    Love,
}

public class Reaction : IEquatable<Reaction>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [JsonIgnore]
    public ApplicationUser User { get; set; }
    
    public DateTime Created = DateTime.Now;
    public ReactionType ReactionType { get; set; }
    
    public override bool Equals(object? obj)
    {
        return obj is Reaction && Equals(obj);
    }
    
    public bool Equals(Reaction? b)
    {
        if (b is null)
        {
            return false;
        }
        // Adjust according to requirements.
        return StringComparer.InvariantCultureIgnoreCase
            .Equals(User?.Id, b?.User?.Id);

    }
    
    public static bool operator==(Reaction a, Reaction b)
    {
        return a.Equals(b);
    }
    
    public static bool operator!=(Reaction a, Reaction b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        if(User is null)
        {
            return 0;
        }
        
        return StringComparer.InvariantCultureIgnoreCase
            .GetHashCode(User?.Id);

    }
}