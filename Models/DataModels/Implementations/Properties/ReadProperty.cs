using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class ReadProperty : SimpleBoolProperty
    {
        public new static string Name = "Read";
        
        public ReadProperty(bool data = false)
        {
            this.Data = data;
            Id = new Guid();
        }
    }
}