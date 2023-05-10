using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Security.Authentication;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static Eblue.Utils.ConstantsTools;
using static Eblue.Utils.SessionTools;
using static Eblue.Utils.DataTools;
using static Eblue.Utils.WebTools;

using Eblue.Utils;
using Eblue.Code.Models;


using System.Data.SqlClient;
using System.IO;

using System.Text;

using Eblue.Code.Wraps;
using System.Runtime.Serialization;

namespace Eblue.Utils
{
    [Serializable()]
    public class ControlInfo {
        /*
         * 
         Uid uniqueidentifier primary key not null,
     uniquename nvarchar(255) unique ,
     name nvarchar(max),
     text nvarchar(max),
     origin nvarchar(max),
     tooltip nvarchar(max),
     creationdate datetime not null,
     lastupdate datetime not null,
     identityId int identity(1,1) not null
         */

        public Guid uid { get; set; }
        public string uniquename { get; set; }
        public string name { get; set; }

        public string text { get; set; }
        public string origin { get; set; }
        public string tooltip { get; set; }
        public DateTime creationdate { get; set; }
        public DateTime lastupdate { get; set; }
        public int identityId { get; set; }

    }

    [Serializable()]
    public class ControlInfoList : List<ControlInfo>, ISerializable
    {
        public string InnerText { get; set; }

        //GetObjectData(SerializationInfo info, StreamingContext context);
        //public override void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    //base.GetObjectData(info, context);
        //}
        //public override void OnDeserialization(object sender)
        //{
        //    //base.OnDeserialization(sender);
        //}

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //base.GetObjectData(info, context);
        }

        public ControlInfoList()
        {

        }

        public ControlInfoList(SerializationInfo info, StreamingContext context)
        {

        }
    }
    public static class ControlTools
    {

        public static bool? TryGetControlsInfo(out ControlInfoList ret, ControlCollection ctrls)
        {
            bool? flag = default(bool?);
            ControlInfoList retlist = new ControlInfoList();
            ret = new ControlInfoList(); 
            var controls = ctrls.OfType<Control>();

            var webcontrolsBase = controls.Where(ctrl => ctrl is WebControl) ?? new List<WebControl>();
            var htmcontrolsBase = controls.Where(ctrl => ctrl is HtmlControl) ?? new List<HtmlControl>(); ;
            var ctrlFlags = new { hasWebcontrols = webcontrolsBase.Count() >= 1, hasHtmcontrols = htmcontrolsBase.Count() >= 1 };

            if (ctrlFlags.hasWebcontrols)
            {
                //if (controlList != null && controlList.Count() > 0)
                //{
                //    var webControls = controlList.OfType<WebControl>();
                var webcontrols = webcontrolsBase.OfType<WebControl>();
                var webcontrolsList = webcontrols.Where(ctrl => ctrl is Label) ?? new List<WebControl>();

                if (webcontrolsList != null && webcontrolsList.Count() > 0)
                {
                    var labels = webcontrolsList.OfType<Label>();
                    labels.ToList().ForEach(ctrl =>
                    {
                        string page_uniqueId = ctrl.Page.UniqueID;
                        string form_uniqueId = ctrl.Page.Form.UniqueID;
                        string control_uniqueId = ctrl.Parent.UniqueID;
                        string target_uniqueId = ctrl.UniqueID;
                        string uniquename = string.Empty;
                        string name = string.Empty;
                        string caption = string.Empty;
                        string origin = string.Empty;
                        string tooltip = string.Empty;
                        DateTime creationdate = DateTime.Now;
                        DateTime lastupdate = DateTime.Now;

                        #region webcontrol - Text,ToolTip     //TODO (lang)
                        name = ctrl.ID;
                        uniquename = ctrl.UniqueID;
                        int namechars = uniquename.Length;
                        char[] chars = { };

                        if (namechars >= 16)
                        {
                            chars = uniquename.Substring(0, 16).ToCharArray();
                        }
                        else
                        {

                            int len = namechars - 16;
                            string xid = uniquename;
                            string yid = name.Substring(0, System.Math.Min(len, name.Length));
                            chars = $"{xid}{yid}".ToCharArray();
                        }

                        byte[] bytes = Encoding.ASCII.GetBytes(chars);
                        Guid guid = new Guid(bytes);
                        caption = ctrl.Text;
                        origin = ctrl.Text;
                        tooltip = ctrl.ToolTip;

                        ControlInfo ctrlInfo = new ControlInfo()
                        {
                            uid = guid,
                            uniquename = uniquename,
                            name = name,
                            text = caption,
                            origin = origin,
                            tooltip = tooltip
                        };
                        retlist.Add(ctrlInfo);
                        //ctrl.Text
                        //ctrl.ToolTip
                        #endregion

                        #region webcontrol - CssClass
                        ctrl.CssClass = ctrl.CssClass ?? string.Empty;
                        var formcontrolCssClass = !ctrl.CssClass.Contains("form-control");

                        if (formcontrolCssClass)
                        {
                            ctrl.CssClass = CssClassFormControl;

                        }
                        #endregion



                    });


                }

                //}
                ret = retlist;
                flag = true;
            }
            else {
                flag = false;
            }

            if (ctrlFlags.hasHtmcontrols)
            {

                var stop = true;

            }

            return flag;
        }

        public static void SetControlsInfo(ControlCollection ctrls)
        {
                var controls = ctrls.OfType<Control>();

                var webcontrolsBase = controls.Where(ctrl => ctrl is WebControl) ?? new List<WebControl>();
                var htmcontrolsBase = controls.Where(ctrl => ctrl is HtmlControl) ?? new List<HtmlControl>(); ;
                var ctrlFlags = new { hasWebcontrols = webcontrolsBase.Count() >= 1, hasHtmcontrols = htmcontrolsBase.Count() >= 1 };

                if (ctrlFlags.hasWebcontrols)
                {


                    //if (controlList != null && controlList.Count() > 0)
                    //{
                    //    var webControls = controlList.OfType<WebControl>();
                    var webcontrols = webcontrolsBase.OfType<WebControl>();
                    var webcontrolsList = webcontrols.Where(ctrl => ctrl is Label) ?? new List<WebControl>();

                    if (webcontrolsList != null && webcontrolsList.Count() > 0)
                    {
                        var labels = webcontrolsList.OfType<Label>();
                        labels.ToList().ForEach(ctrl =>
                        {
                            string page_uniqueId = ctrl.Page.UniqueID;
                            string form_uniqueId = ctrl.Page.Form.UniqueID;
                            string control_uniqueId = ctrl.Parent.UniqueID;
                            string target_uniqueId = ctrl.UniqueID;
                            string uniquename = string.Empty;
                            string name = string.Empty;
                            string caption = string.Empty;
                            string origin = string.Empty;
                            string tooltip = string.Empty;
                            DateTime creationdate = DateTime.Now;
                            DateTime lastupdate = DateTime.Now;

                            #region webcontrol - Text,ToolTip     //TODO (lang)
                            name = ctrl.ID;
                            uniquename = ctrl.UniqueID;
                            int namechars = uniquename.Length;
                            char[] chars = { };

                            if (namechars >= 16)
                            {
                                chars = uniquename.Substring(0, 16).ToCharArray();
                            }
                            else {
                                
                                int len = namechars - 16;
                                string xid = uniquename;
                                string yid = name.Substring(0,System.Math.Min(len, name.Length));
                                chars = $"{xid}{yid}".ToCharArray();
                            }
                            
                            byte[] bytes = Encoding.ASCII.GetBytes(chars);
                            Guid uid = new Guid(bytes);
                            caption = ctrl.Text;
                            origin = ctrl.Text;
                            //ctrl.Text
                            //ctrl.ToolTip
                            #endregion

                            #region webcontrol - CssClass
                            ctrl.CssClass = ctrl.CssClass ?? string.Empty;
                            var formcontrolCssClass = !ctrl.CssClass.Contains("form-control");

                            if (formcontrolCssClass)
                            {
                                ctrl.CssClass = CssClassFormControl;

                            }
                            #endregion



                        });


                    }

                    //}

                }

                if (ctrlFlags.hasHtmcontrols)
                {

                    var stop = true;

                }
         

        }

        public static void PageInfoFor(Page target) {
            //Content               //--> control
            //PlaceHolder           //--> control
            //ContentPlaceHolder    //--> control
            //DataList              //--> control, Webcontrol,BaseDataList
            //GridView              //--> control, Webcontrol, BaseDataBoundControl
            //Label                 //--> control, Webcontrol, ITextControl
        }

        public static void FormInfoFor(HtmlForm target) { }

        public static void ControlInfoFor(Control target) { }

        public static void ControlServerInfoFor(WebControl target) { }

        public static void ControlClientInfoFor(HtmlControl target) { }

    }
}