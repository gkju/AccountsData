using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class AdminProperty : SimpleBoolProperty
    {
        public AdminProperty()
        {
            Id = Guid.NewGuid();
        }
        public AdminProperty(bool data)
        {
            this.Data = data;
            Id = Guid.NewGuid();
        }
        
        public override void SetDefaultBannedValue()
        {
            Data = false;
        }
        
        public override void SetDefaultAdminValue()
        {
            Data = true;
        }

        public new static string Name = "Admin";
    }
}