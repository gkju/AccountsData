using System.Collections.Generic;
using AccountsData.Models.DataModels.Abstracts;

namespace AccountsData.Models.DataModels.Implementations.RoleScope
{
    public class ArticlesScope : Scope
    {
        private void Initialize()
        {
            ParentScopes = new List<Scope> {new GlobalScope()};
        }
        
        public new readonly string Name = "GlobalBoardScope";

        public ArticlesScope()
        {
            Initialize();
        }
    }
}