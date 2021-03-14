using System;
using AccountsData.Models.DataModels.Helpers;


namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class AuthorityProperty : SimpleIntegerProperty
    {
        public AuthorityProperty()
        {
            Id = new Guid();
        }
        public AuthorityProperty(int data)
        {
            this.Data = data;
            Id = new Guid();
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