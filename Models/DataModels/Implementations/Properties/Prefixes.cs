using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AccountsData.DataModels.Helpers;
using AccountsData.Models.DataModels.Abstracts;
using AccountsData.Models.DataModels.Implementations.Roles;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class Prefix
    {
        [Key]
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Role role { get; set; }
        public string Name { get; set; }
        public string HexColor { get; set; }

        public Prefix(string name = "", string hexColor = "")
        {
            Name = name;
            HexColor = hexColor;
            Id = new Guid();
        }
        
        public Prefix(Role role, string name = "", string hexColor = "")
        {
            Name = name;
            HexColor = hexColor;
            this.role = role;
            RoleId = role.Id;
            Id = new Guid();
        }
    }
    
    public class Prefixes : Property
    {
        public static string Name { get; set; } = "Prefixes";
        public HashSet<Prefix> Data { get; set; } = new ();

        public Prefixes()
        {
            Id = new Guid();
        }

        public Prefixes(Prefixes prefixes)
        {
            Data = prefixes.Data;
            Id = new Guid();
        }

        public Prefixes(Prefix prefix)
        {
            Data.Add(prefix);
            Id = new Guid();
        }

        public override Property MakeACombination(Property parentProperty)
        {
            var parentProp = parentProperty as Prefixes;

            var copy = (Prefixes) Clone();

            if (parentProp == null)
            {
                return copy;
            }
            
            foreach (var prefix in parentProp.Data)
            {
                Data.Add(prefix);
            }

            return copy;
        }

        public override void SetDefaultBannedValue()
        {
            Data = new HashSet<Prefix>();
            Data.Add(new Prefix("Banned", "#e81313"));
        }

        public static bool operator ==(Prefixes a, Prefixes b)
        {
            if (CheckReferenceEquality(a, b) == ReferenceEquality.SameObjects)
            {
                return true;
            } else if (CheckReferenceEquality(a, b) == ReferenceEquality.OneIsNull)
            {
                return false;
            }

            return a.Data.SetEquals(b.Data) && a.GetType() == b.GetType();
        }

        public static bool operator !=(Prefixes a, Prefixes b)
        {
            return !(a == b);
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            try
            {
                Prefixes objConv = obj as Prefixes;
                return this.Data.SetEquals(objConv?.Data) && this.GetType() == obj.GetType();
            }
            catch
            {
                return false;
            }
        }

        public bool Equals(Prefixes obj)
        {
            return Equals((object) obj);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Name, Data).GetHashCode();
        }
    }
}