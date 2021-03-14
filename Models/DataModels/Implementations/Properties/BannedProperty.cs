
using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class BannedProperty : SimpleBoolProperty
    {
        public new static string Name = "Banned";
        
        public BannedProperty()
        {
            Data = true;
            Id = new Guid();
        }
    }
}