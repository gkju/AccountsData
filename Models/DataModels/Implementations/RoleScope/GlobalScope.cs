using System;
using System.Collections.Generic;
using AccountsData.Models.DataModels.Abstracts;

namespace AccountsData.Models.DataModels.Implementations.RoleScope
{
    public class GlobalScope : Scope
    {
        public new readonly string Name = "GlobalScope";
    }
}