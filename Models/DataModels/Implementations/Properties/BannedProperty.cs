
using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class BannedProperty : SimpleBoolProperty
    {
        public new static string Name = "Banned";
        
        public override void SetDefaultBannedValue()
        {
            Data = true;
        }
        public BannedProperty()
        {
            Data = true;
            Id = Guid.NewGuid();
        }
    }
}