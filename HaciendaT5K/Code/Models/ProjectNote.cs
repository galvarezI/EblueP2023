using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eblue.Code.Models
{
  

    public class ProjectNote : Tuple<int,Guid, DateTime, string, string, string, int>
    {
        public int RowNumber { get { return this.Item1; } }
        public Guid UID { get { return this.Item2; } }
        public DateTime NoteDate { get { return this.Item3; } }
        public string NoteData { get { return this.Item4; } }
        public string RosterName { get { return this.Item5; } }
        public string RosterPicture { get { return this.Item6; } }

        public int HierarchyOrderLine { get { return this.Item7; } }

        public ProjectNote(int a, Guid b , DateTime c, string d, string e, string f, int g)  : base(item1: a, item2: b, item3: c, item4: d, item5: e, item6: f, item7: g) { }

    }
    public class ProjectNoteSet : List<ProjectNote>
    {
        public string InnerText { get; set; }
    }

  
}