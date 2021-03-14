using System;
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class MemberProperty : SimpleBoolProperty
    {
        public new static string Name = "Member";

        public MemberProperty(bool data = false)
        {
            this.Data = data;
            Id = new Guid();
        }
    }
}