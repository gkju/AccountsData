using AccountsData.Models.DataModels.Helpers;


namespace AccountsData.Models.DataModels.Implementations.Properties
{
    public class AuthorityProperty : SimpleIntegerProperty
    {
        public AuthorityProperty(int data = 0)
        {
            Data = data;
        }
        
        public override void SetDefaultBannedValue()
        {
            Data = 0;
        }

        public int GetValue()
        {
            return Data;
        }

        public new static readonly string Name = "Authority";
    }
}