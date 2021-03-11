
using AccountsData.Models.DataModels.Helpers;

namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class BannedProperty : SimpleBoolProperty
    {
        public new static readonly string Name = "Banned";

        public BannedProperty()
        {
            Data = true;
        }
    }
}