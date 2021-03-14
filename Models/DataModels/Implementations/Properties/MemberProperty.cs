using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class MemberProperty : SimpleBoolProperty
    {
        public new static string Name = "Member";

        public override void SetDefaultBannedValue()
        {
            Data = false;
        }
        public override void SetDefaultAdminValue()
        {
            Data = true;
        }
        
        public MemberProperty(bool data = false)
        {
            this.Data = data;
            Id = Guid.NewGuid();
        }
    }
}