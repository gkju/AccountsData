using System;
using System.ComponentModel.DataAnnotations;

namespace AccountsData.Models.DataModels.Abstracts
{
    public abstract class Role
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }
        
        protected Role(Scope scope, Properties properties, string Name = "DefaultRoleName")
        {
            this.Scope = scope;
            this.Name = Name;
            Id = Guid.NewGuid();
        }

        public virtual string Name { get; set; } = "DefaultRoleName";

        [Key]
        public Guid Id { get; set; }

        public Properties Properties { get; set; }
        public Scope Scope { get; set; }

        public virtual bool UserManageable { get; } = true;
        public static bool operator ==(Role a, Role b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Name == b.Name && a.Id == b.Id && a.Scope == b.Scope;
        }

        public static bool operator !=(Role a, Role b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            Type t = obj.GetType();

            try
            {
                Role otherRole = (Role) obj;

                return otherRole == this;
            }
            catch
            {
                return false;
            }
            
        }

        public bool Equals(Scope obj)
        {
            return Equals((object) obj);
        }
    }
}