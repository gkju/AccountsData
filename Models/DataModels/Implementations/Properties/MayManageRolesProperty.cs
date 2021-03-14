﻿

using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class MayManageRolesProperty : SimpleBoolProperty
    {
        public new static string Name = "ManageRoles";
        
        public MayManageRolesProperty(bool data = false)
        {
            this.Data = data;
            Id = new Guid();
        }
    }
}