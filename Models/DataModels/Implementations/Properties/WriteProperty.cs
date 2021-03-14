using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class WriteProperty : SimpleBoolProperty
    {
        public new static string Name = "Write";
        
        public WriteProperty(bool data = false)
        {
            this.Data = data;
            Id = new Guid();
        }
    }
}