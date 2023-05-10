using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eblue.Code
{

    public class PlayerInfo : Tuple< int, string, Guid, string, string, Guid>
    {
        public int RoleId { get { return this.Item1; } }
        public string RoleName { get { return this.Item2; } }
        public Guid RoleTypeId { get { return this.Item3; } }
        public string RoleTypeDescription { get { return this.Item4; } }
        public string Caption { get { return this.Item5; } }

        public Guid RosterID { get { return this.Item6; } }

        public PlayerInfo(int a, string b, Guid c, string d, string e, Guid f) : base(item1: a, item2: b, item3: c, item4: d, item5: e, item6: f) { }

    }

    public class PlayerInfoSet: List<PlayerInfo>
    { 
    
    }
}