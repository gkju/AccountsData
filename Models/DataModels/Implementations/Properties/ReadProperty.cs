using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class ReadProperty : SimpleBoolProperty
    {
        public new static string Name = "Read";
        
        
        public override void SetDefaultBannedValue()
        {
            Data = false;
        }
        public override void SetDefaultAdminValue()
        {
            Data = true;
        }
        public ReadProperty(bool data = false)
        {
            this.Data = data;
            Id = Guid.NewGuid();
        }
    }
}