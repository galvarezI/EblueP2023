using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eblue.Code.Models
{
   

    public class ControlAttribute : Tuple<string, Guid, string, bool, bool, string, bool>
    {
        public string TagAutocomplete { get { return this.Item1; } }
        public Guid UID { get { return this.Item2; } }
        public string Name { get { return this.Item3; } }
        public bool ForNumeric { get { return this.Item4; } }
        public bool ForDate { get { return this.Item5; } }
        public string TagType { get { return this.Item6; } }
        public bool HasSuggest { get { return this.Item7; } }

        


        /// <summary>
        /// create a ControlAttribute model
        /// </summary>
        /// <param name="a">TagAutocomplete</param>
        /// <param name="b">UID</param>
        /// <param name="c">Name</param>
        /// <param name="d">ForNumeric </param>
        /// <param name="e">ForDate</param>
        /// <param name="f">TagType</param>
        /// <param name="g">HasSuggest</param>
        public ControlAttribute(string a, Guid b, string c, bool d, bool e, string f, bool g) : base(item1: a, item2: b, item3: c, item4: d, item5: e, item6: f, item7:g) { }

    }
    public class ControlAttributeSet : Dictionary<string,ControlAttribute>
    {
        //public readonly Dictionary<string, ControlAttribute> _dictionaryList;

        //public Dictionary<string, ControlAttribute> DictionaryHelp { get {
        //        return _dictionaryList == null ? new Dictionary<string, ControlAttribute>() : _dictionaryList;
        //    } }
        //public ControlAttributeSet()
        //{ }

        //public ControlAttributeSet(int capacity)
        //{

        //}

        //public Dictionary<string, ControlAttribute> GetDirectionary() 
        //{
        //    Dictionary<string, ControlAttribute> result = ToDictionary<string, ControlAttribute>(item => new KeyValuePair<string, ControlAttribute>(item.Item3, item));//.ToDictionary<string, ControlAttribute>(item=> new );
        //    return result;
        
        //}
        //public string InnerText { get; set; }
    }
}