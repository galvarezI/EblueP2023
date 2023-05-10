using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eblue.Code
{

    public class ActionPanel : Tuple<Guid, string, string, ActionPanelList, bool>
    {
        public int OrderLine { get; set; }
        public Guid UID { get { return this.Item1; } }
        public string Description { get { return this.Item2; } }
        public string Route { get { return this.Item3; } }

        public ActionPanelList ActionPages { get { return this.Item4; } }

        public bool NotVisibleForMenu { get { return this.Item5; } }

        public ActionPanel(Guid uid, string description, string route, ActionPanelList actionPages, bool notvisibleformenu) : base(item1: uid, item2: description, item3: route, item4: actionPages, item5: notvisibleformenu) { }

    }
    public class PanelTuple : List<ActionPanel>
    {
        public string InnerText { get; set; }
    }

    public class ActionPage : Tuple<Guid, string, string, bool>
    {
        //public ActionPanel Root { get; set; }
        public ActionPanel Parent { get; set; }
        public ProfileTuple Root { get; set; }
        public AgrupationTuple Agrupation { get; set; }

        public int OrderLine { get; set; }
        public Guid UID { get { return this.Item1; } }
        public string Description { get { return this.Item2; } }
        public string Route { get { return this.Item3; } }
        public bool NotVisibleForMenu { get { return this.Item4; } }

        public ActionPage(Guid uid, string description, string route, bool notvisibleformenu) : base(item1: uid, item2: description, item3: route, item4: notvisibleformenu) { }
    }

    public class ActionPanelList : List<ActionPage>
    {
    }

    public class ActionPageList : List<ActionPage>
    {
    }

    //this is like panel agrupation
    public class AgrupationTuple : Tuple<Guid, string, string, ActionPageList, bool, string>
    {
        public int OrderLine { get; set; }
        public Guid UID { get { return this.Item1; } }
        public string Description { get { return this.Item2; } }
        public string Route { get { return this.Item3; } }

        public ActionPageList ActionPages { get { return this.Item4; } }

        public bool NotVisibleForMenu { get { return this.Item5; } }

        public string IconClass { get { return this.Item6; } }

        public AgrupationTuple(Guid uid, string description, string route, ActionPageList actionPages, bool notvisibleformenu, string iconClass) : base(item1: uid, item2: description, item3: route, item4: actionPages, item5: notvisibleformenu, item6: iconClass) { }

    }

    public class AgrupationTupleList : List<AgrupationTuple>
    {

    }

    //this is like profile root
    public class ProfileTuple : Tuple<Guid, string, string, AgrupationTupleList, bool>
    {
        public int OrderLine { get; set; }
        public Guid UID { get { return this.Item1; } }
        public string Description { get { return this.Item2; } }
        public string Route { get { return this.Item3; } }

        public AgrupationTupleList Agrupations { get { return this.Item4; } }

        public bool NotVisibleForMenu { get { return this.Item5; } }

        public ProfileTuple(Guid uid, string description, string route, AgrupationTupleList agrupations, bool notvisibleformenu) : base(item1: uid, item2: description, item3: route, item4: agrupations, item5: notvisibleformenu) { }

        

    }

    public class ProfileTupleList : List<ProfileTuple>
    {
        public string InnerText { get; set; }
    }


}