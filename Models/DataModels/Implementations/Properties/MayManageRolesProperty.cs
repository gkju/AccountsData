

using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class MayManageRolesProperty : SimpleBoolProperty
    {
        public new static readonly string Name = "ManageRoles";

        public MayManageRolesProperty(bool mayManage = false)
        {
            Data = mayManage;
        }
    }
}