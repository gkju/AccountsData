using System.Collections.Generic;
using AccountsData.Models.DataModels.Abstracts;

namespace AccountsData.Models.DataModels.Implementations.RoleScope
{
    public class NewsScope: Scope
    {
        public new readonly string Name = "NewsScope";
        
        private void Initialize()
        {
            ParentScopes = new List<Scope> {new GlobalScope()};
        }

        public NewsScope()
        {
            Initialize();
        }
    }
}