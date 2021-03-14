using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class EditOrDeleteProperty : SimpleBoolProperty
    {
        public new static string Name = "EditOrDelete";
        
        public override void SetDefaultBannedValue()
        {
            Data = false;
        }
        public override void SetDefaultAdminValue()
        {
            Data = true;
        }
        
        public EditOrDeleteProperty(bool data = false)
        {
            this.Data = data;
            Id = Guid.NewGuid();
        }
    }
}