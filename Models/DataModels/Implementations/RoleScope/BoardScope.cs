using System;
using System.Collections.Generic;
using AccountsData.Models.DataModels.Abstracts;

namespace AccountsData.Models.DataModels.Implementations.RoleScope
{
    public class BoardScope : Scope
    {
        private void Initialize()
        {
            ParentScopes = new List<Scope> {new GlobalBoardScope()};
        }
        
        public new readonly string Name = "BoardScope";
        public string SubName { get; set; }
        public BoardScope(Board board)
        {
            Initialize();
            
            SubName = board.Name;
        }
        
    }
}