using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class EditOrDelete : SimpleBoolProperty
    {
        public new static string Name = "ManageRoles";
        
        public EditOrDelete(bool data = false)
        {
            this.Data = data;
            Id = new Guid();
        }
    }
}