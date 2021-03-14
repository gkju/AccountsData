using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class WriteProperty : SimpleBoolProperty
    {
        public new static string Name = "Write";
        
        public override void SetDefaultBannedValue()
        {
            Data = false;
        }
        public override void SetDefaultAdminValue()
        {
            Data = true;
        }
        
        public WriteProperty(bool data = false)
        {
            this.Data = data;
            Id = Guid.NewGuid();
        }
    }
}