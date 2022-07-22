using System;
using AccountsData.Models.DataModels.Helpers;


namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class AuthorityProperty : SimpleIntegerProperty
    {
        public AuthorityProperty()
        {
            Id = Guid.NewGuid();
        }
        public AuthorityProperty(int data)
        {
            this.Data = data;
            Id = Guid.NewGuid();
        }
        
        public override void SetDefaultBannedValue()
        {
            Data = 0;
        }

        public int GetValue()
        {
            return Data;
        }

        public new static string Name = "Authority";
    }
}