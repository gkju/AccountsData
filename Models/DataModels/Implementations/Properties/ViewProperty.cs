using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class ViewProperty : SimpleBoolProperty
    {
        public new static string Name = "View";
        
        public override void SetDefaultBannedValue()
        {
            Data = false;
        }
        public override void SetDefaultAdminValue()
        {
            Data = true;
        }
        
        public ViewProperty(bool data = false)
        {
            this.Data = data;
            Id = Guid.NewGuid();
        }
    }
}