using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eblue.Code.Models
{
    public class ProjectSign : Tuple<int, Guid, DateTime, string, string, string>
    {
        public int RowNumber { get { return this.Item1; } }
        public Guid UID { get { return this.Item2; } }
        public DateTime SignDate { get { return this.Item3; } }
        public string SignData { get { return this.Item4; } }
        public string RosterName { get { return this.Item5; } }
        public string RosterData { get { return this.Item6; } }


        /// <summary>
        /// create a projectSign model
        /// </summary>
        /// <param name="a">RowNumber</param>
        /// <param name="b">UID</param>
        /// <param name="c">SignDate</param>
        /// <param name="d">SignData (like Signature)</param>
        /// <param name="e">RosterName</param>
        /// <param name="f">RosterData (like Picture)</param>
        public ProjectSign(int a, Guid b, DateTime c, string d, string e, string f) : base(item1: a, item2: b, item3: c, item4: d, item5: e, item6: f) {}

    }
    public class ProjectSignSet : List<ProjectSign>
    {
        public string InnerText { get; set; }
    }
}