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


namespace Eblue.Code
{

    [Flags]
    public enum ActionFilterFlags
    {
     none,
     Add,
     Remove,
     Confirm = Remove << 1,
     AddToConfirm = Add | Confirm
    }
    public class ButtonInfoActionFilter
    {
        public const string DeleteActionConfirm = "Are you sure to perform this \"delete {0}\" action.";
        public const string AddActionConfirm = "Are you sure to perform this add {0} action.";
        public string Message { get; set; }
        public string Model { get; set; }

        public ActionFilterFlags Flags { get; set; }

    }
    public class PageBasic : System.Web.UI.Page
    {

        #region body

        
        public RequestFilterBasic RequestDataInfo
        {

            get => (HasBasicViewState && ViewState[nameof(RequestDataInfo)] is RequestFilterBasic exp) ? exp : new RequestFilterBasic();
            set => ((HasBasicViewState && ViewState is StateBag vs) ? vs : new StateBag())[nameof(RequestDataInfo)] = value;

        }
        public string ControlNames
        {

            get => (HasBasicViewState && ViewState[nameof(ControlNames)] is string exp) ? exp : string.Empty;
            set => ((HasBasicViewState && ViewState is StateBag vs) ? vs : new StateBag())[nameof(ControlNames)] = value;

        }

        public bool HasBasicViewState
        {
            get
            {
                var viewState = this.ViewState;
                bool result = true;
                if (viewState == null) result = false;

                return result;

            }
        }

        public string PriorityFullFieldID
        {
            get
            {
                var result = string.Empty;
                
                if (HasBasicViewState)
                {
                    var viewState = this.ViewState;
                    result = viewState[nameof(PriorityFullFieldID)] as string;
                }

                return result;
            }
            set
            {

                if (HasBasicViewState)
                {
                    var viewState = this.ViewState;
                    viewState[nameof(PriorityFullFieldID)] = value;
                }

            }
        }

        public bool GetIsGridVModelEditingFor<GridV>(GridV gridV) where GridV: GridView => gridV.EditIndex > -1;
        public bool GetIsDataLModelEditingFor<DataL>(DataL dataL) where DataL : DataList => dataL.EditItemIndex > -1;
        public string GetRosterName() => (UserInfo != null) ? UserInfo.RosterName : string.Empty;
        public string GetRosterSignature() => (UserInfo != null && !string.IsNullOrEmpty(UserInfo.RosterSignature)) ? UserInfo.RosterSignature : Eblue.Utils.ConstantsTools.UserGenericSignData;
        public string GetRosterPicture() => (UserInfo != null && !string.IsNullOrEmpty(UserInfo.RosterPicture)) ? UserInfo.RosterPicture : Eblue.Utils.ConstantsTools.UserGenericPictureData;
        public string PriorityFieldID 
        {
            get {
                var result = string.Empty;
                var viewState = this.ViewState;
                if (viewState != null)
                {
                    result = viewState[nameof(PriorityFieldID)] as string;
                }

                return result;
            }
            set {

                var viewState = this.ViewState;
                if (viewState != null)
                {
                    viewState[nameof(PriorityFieldID)] = value;
                }

                

            }
        }

        public System.Text.StringBuilder OnSaveStateCompleteSentences { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {

                FormsIdentity id = (FormsIdentity)User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;

                var userId = Eblue.Utils.SessionTools.UserId;

                if (userId == Guid.Empty)
                {

                    SignOut();
                }


                var isLoadEventArgs = e is LoadEventArgs;
                bool isErrorPage = false;
                bool isUnAuthorizePage = false;
                bool hasEvaluatePage;

                //bool? testNullable = default(bool?);
                //bool testbool = testNullable.HasValue;

                if (isLoadEventArgs)
                {
                    var eventArgs = e as LoadEventArgs;

                    //isErrorPage = eventArgs.IsError == null ? false : eventArgs.IsError.Value;
                    isErrorPage = eventArgs.IsError != null && eventArgs.IsError.Value;
                    isUnAuthorizePage = eventArgs.IsUnAuthorize != null && eventArgs.IsUnAuthorize.Value;

                    //if (isErrorPage )
                    //{
                    //    isErrorPage = eventArgs.IsError.Value;
                    //}  

                    HasErrorEvaluating = isErrorPage;

                }

                hasEvaluatePage = !(isErrorPage || isUnAuthorizePage);

                //var userLogged = Eblue.Utils.SessionTools.UserInfo;

                //if ( (!isErrorPage && userLogged != null) && (
                //    userLogged.IsAdmin ||
                //    userLogged.IsManager ||
                //    userLogged.IsSupervisor ||
                //    userLogged.IsPersonnel ||
                //    userLogged.IsScientist ||
                //    userLogged.IsStudent ||
                //    userLogged.IsDeveloper ||
                //    userLogged.IsCoordinator))
                //if (!isErrorPage && EvalIsUserType( UserTypeFlags.UserType).Isbool)
                if (hasEvaluatePage && EvalIsUserType(UserTypeFlags.UserType).IsTrue)
                {



                    #region better
                    bool? isHomePage;
                    bool? isWhichIparticipate;
                    isLoadEventArgs = e is LoadEventArgs;
                   

                    if (isLoadEventArgs)
                    {
                        Tuple<string, string, string>[] opts = new [] { 
                            new Tuple<string, string, string>("", "0", "0"),  //-> home Page
                            new Tuple<string, string, string>("", "0", "-1")  //-> whichIparticipate Page
                        };

                        
                        var eventArgs = e as LoadEventArgs;

                        //isHomePage = eventArgs.IsHome == null ? false : eventArgs.IsHome.Value;
                        isHomePage = eventArgs.IsHome ?? eventArgs.IsTryOut; //.HasValue ? ; //!= null && eventArgs.IsHome.Value;

                        isWhichIparticipate = eventArgs.IsWhichIparticipate ?? eventArgs.IsTryOut;


                        if (isHomePage.HasValue && isHomePage.Value)
                        {
                            PanelSelectedIndex = opts[0].Item2;
                            LinkSelectedIndex = opts[0].Item3;
                        }

                        else if (isWhichIparticipate.HasValue && isWhichIparticipate.Value)
                        {
                            PanelSelectedIndex = opts[1].Item2;
                            LinkSelectedIndex = opts[1].Item3;
                        }


                        //if (isHomePage.HasValue && isHomePage.Value)
                        //{
                        //    PanelSelectedIndex = $"0";
                        //    LinkSelectedIndex = $"0";
                        //}

                        #region deprecated
                        //else
                        //{
                        //    CheckUserAuth(uri, actionPanels);

                        //    if (GetSideBarFrom(out Tuple<int, int> sideBar, uri, actionPanels))
                        //    {
                        //        PanelSelectedIndex = $"{sideBar.Item1}";
                        //        LinkSelectedIndex = $"{sideBar.Item2}";
                        //    }
                        //}
                        #endregion



                    }

                    else
                    {


                        var actionPanels = Eblue.Utils.SessionTools.ActionPanels;

                        if (actionPanels != null)
                        {
                            var uri = Request.Url;

                            #region deprecated
                            //isLoadEventArgs = e is LoadEventArgs;

                            //if (isLoadEventArgs)
                            //{
                            //    var eventArgs = e as LoadEventArgs;

                            //    //isHomePage = eventArgs.IsHome == null ? false : eventArgs.IsHome.Value;
                            //    isHomePage = eventArgs.IsHome != null && eventArgs.IsHome.Value;

                            //    if (isHomePage)
                            //    {
                            //        PanelSelectedIndex = $"0";
                            //        LinkSelectedIndex = $"0";
                            //    }
                            //    else
                            //    {
                            //        CheckUserAuth(uri, actionPanels);

                            //        if (GetSideBarFrom(out Tuple<int, int> sideBar, uri, actionPanels))
                            //        {
                            //            PanelSelectedIndex = $"{sideBar.Item1}";
                            //            LinkSelectedIndex = $"{sideBar.Item2}";
                            //        }
                            //    }

                            //}
                            //else
                            #endregion

                            {
                                CheckUserAuth(uri, actionPanels);

                                if (GetSideBarFrom(out Tuple<int, int> sideBar, uri, actionPanels))
                                {
                                    PanelSelectedIndex = $"{sideBar.Item1}";
                                    LinkSelectedIndex = $"{sideBar.Item2}";
                                }
                            }


                        }

                    }
                    #endregion

                    #region deprecated
                    //if (EvalIsUserType(UserTypeFlags.UserTypeOwner).Isbool)
                    //{

                    //    var actionPanels = Eblue.Utils.SessionTools.ActionPanels;

                    //    if (actionPanels != null)
                    //    {
                    //        var uri = Request.Url;

                    //        isLoadEventArgs = e is LoadEventArgs;

                    //        if (isLoadEventArgs)
                    //        {
                    //            var eventArgs = e as LoadEventArgs;

                    //            //isHomePage = eventArgs.IsHome == null ? false : eventArgs.IsHome.Value;
                    //            isHomePage = eventArgs.IsHome != null && eventArgs.IsHome.Value;

                    //            if (isHomePage)
                    //            {
                    //                PanelSelectedIndex = $"0";
                    //                LinkSelectedIndex = $"0";
                    //            }
                    //            else
                    //            {
                    //                CheckUserAuth(uri, actionPanels);

                    //                if (GetSideBarFrom(out Tuple<int, int> sideBar, uri, actionPanels))
                    //                {
                    //                    PanelSelectedIndex = $"{sideBar.Item1}";
                    //                    LinkSelectedIndex = $"{sideBar.Item2}";
                    //                }
                    //            }

                    //        }
                    //        else
                    //        {
                    //            CheckUserAuth(uri, actionPanels);

                    //            if (GetSideBarFrom(out Tuple<int, int> sideBar, uri, actionPanels))
                    //            {
                    //                PanelSelectedIndex = $"{sideBar.Item1}";
                    //                LinkSelectedIndex = $"{sideBar.Item2}";
                    //            }
                    //        }


                    //    }

                    //}
                    //else
                    //{

                    //    var userProfile = Eblue.Utils.SessionTools.UserProfile;
                    //    var actionPanels = Eblue.Utils.SessionTools.ActionPanels;
                    //    if (userProfile != null)
                    //    {
                    //        var uri = Request.Url;

                    //        //var isLoadEventArgs = e is LoadEventArgs;

                    //        if (isLoadEventArgs)
                    //        {
                    //            var eventArgs = e as LoadEventArgs;
                    //            isHomePage = eventArgs.IsHome == null ? false : eventArgs.IsHome.Value;

                    //            if (isHomePage)
                    //            {
                    //                PanelSelectedIndex = $"0";
                    //                LinkSelectedIndex = $"0";
                    //            }
                    //            else
                    //            {
                    //                //CheckUserAuth(uri, userProfile);
                    //                CheckUserAuth(uri, actionPanels);

                    //                if (GetSideBarFrom(out Tuple<int, int> sideBar, uri, userProfile))
                    //                {
                    //                    PanelSelectedIndex = $"{sideBar.Item1}";
                    //                    LinkSelectedIndex = $"{sideBar.Item2}";
                    //                }
                    //            }

                    //        }
                    //        else
                    //        {
                    //            //CheckUserAuth(uri, userProfile);
                    //            CheckUserAuth(uri, actionPanels);

                    //            if (GetSideBarFrom(out Tuple<int, int> sideBar, uri, userProfile))
                    //            {
                    //                PanelSelectedIndex = $"{sideBar.Item1}";
                    //                LinkSelectedIndex = $"{sideBar.Item2}";
                    //            }
                    //        }


                    //    }


                    //}
                    #endregion

                    //if (userLogged.IsAdmin || userLogged.IsDeveloper || userLogged.IsAdministrator)

                }

                //AuthenticationException authExcep = new AuthenticationException();
                //var msg = authExcep.Message;
                OnLoadGeneral();

                if (!Page.IsPostBack)
                {
                    //localizations, focus, etc...

                    //deprecate: porque aunque es realizado el focus, no permite escribir datos debido a que se realizan otras operaciones
                    //despues de realizar el focus, entonces se determina que el metodo idoneo para realizar este metodo es:
                    // OnSaveStateComplete || OnSaveStateCompleteExtend(llamado desde el nestpage override OnSaveStateComplete)
                    FormBaseDefaultActions();

                    //if (!FormBaseSetDefaultFocus())
                    //{
                    //    Console.WriteLine("the form no has default control for focus");
                    //}
                }
            }
            else 
            {
                var stop = true;
                if (stop) {

                    
                        var errorMessage = "Error at the authentication system";
                        var builder = new System.Text.StringBuilder();
                        AuthenticationException authExcep = new AuthenticationException();
                        Tuple<bool?, Exception> exceptionX = new Tuple<bool?, Exception>(false, authExcep);
                        HandlerExeption(errorMessage, builder, exceptionX);                   

                }
            
            }

            

        }

        #region deprecated methods
        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);

        //    if (FormGetDefaultFocus(out WebControl ctrl, this.Form))
        //    {
        //        SetFocusPriorityField(ctrl);

        //    }
        //}
        #endregion




        #region Settings HTML Atributes To Controls
        protected void SetControlFor(Button btnControl, ButtonInfoActionFilter btnFilter)
        {
            //are you sure to perform this "delete note" action.
            //deleteButton.OnClientClick = "if ( !confirm('Are you sure you want to delete this entry?')) return false;";
            if (btnFilter.Flags == ActionFilterFlags.AddToConfirm)
            {
                var txt = ButtonInfoActionFilter.AddActionConfirm;
                var mdl = btnFilter.Model;
                var msg = string.Format(txt, mdl);
                msg = $"{msg} {Environment.NewLine} {btnFilter.Message}";

                if (btnControl != null)
                {
                    //btnControl.OnClientClick = $"if ( !confirm('{msg}?')) return false;";

                }
            }

        }

        protected bool GetDataControlAttributes(out Code.Models.ControlAttributeSet listset)
        {
            bool result = false;
            listset = default(ControlAttributeSet);
            var _listset = new ControlAttributeSet();

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo()
            {
                commandString = "select  dca.UId, dca.Name, dca.ForNumeric, dca.ForDate, dca.TagType, dca.HasSuggest, dca.TagAutocomplete from DataControlAttributes dca"
            };

            result = FetchData(reqInfo, reader => {

                
                Guid.TryParse(reader["UId"]?.ToString(), out Guid Uid);
                var name = reader["name"]?.ToString();
                
                bool.TryParse(reader["ForNumeric"]?.ToString(), out bool forNumeric);
                bool.TryParse(reader["ForDate"]?.ToString(), out bool forDate);
                var tagType = reader["TagType"]?.ToString();
                bool.TryParse(reader["HasSuggest"]?.ToString(), out bool hasSuggest);
                var tagAutocomplete = reader["TagAutocomplete"]?.ToString();

                var model = new ControlAttribute(tagAutocomplete, Uid, name, forNumeric,forDate,tagType, hasSuggest);

                if (!_listset.ContainsKey(name))
                    _listset.Add(name,model);
            });

            listset = _listset;

            return result;

        }
        #endregion



        /// <summary>
        /// Retrieves the control that caused the postback.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected Control GetControlThatCausedPostBack(Page page)
        {
            //initialize a control and set it to null
            Control ctrl = null;

            //get the event target name and find the control
            string ctrlName = page.Request.Params.Get("__EVENTTARGET");
            if (!String.IsNullOrEmpty(ctrlName))
                ctrl = page.FindControl(ctrlName);

            //return the control to the calling method
            return ctrl;
        }

        protected bool GetRequestParamEventTarget(out string value,Page page)
        {
            bool result = false;
            value = string.Empty;

            //get the event target name
            value = page.Request.Params.Get("__EVENTTARGET")?.Trim();
            if (!String.IsNullOrEmpty(value))
                result = true;
            
            return result;
        }

        protected bool GetRequestParamEventArgument(out string value, Page page)
        {
            bool result = false;
            value = string.Empty;

            //get the event target name
            value = page.Request.Params.Get("__EVENTARGUMENT")?.Trim();
            if (!String.IsNullOrEmpty(value))
                result = true;

            return result;
        }

        protected void FormBaseDefaultActions()
        {

            if (!FormBaseSetDefaultFocus())
            {
                Console.WriteLine("the form no has default control for focus");
            }

            //HERE GO to apply localization logic
        }

        protected bool FormBaseSetDefaultFocus()
        {

            return FormSetDefaultFocus(this.Form);
        }

        /// <summary>
        /// apply default focus and css.class('')
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        protected bool FormSetDefaultFocus(HtmlForm form)
        {
            bool result = false;

            //var form = this.Form;

            if (form != null && form.HasControls())
            {
                var controls = form.Controls.OfType<System.Web.UI.Control>();
                //var controlWebList = controls.Where(ctrl => ctrl is WebControl);
                var controlContentPlaceHolders = controls.Where(ctrl => ctrl is ContentPlaceHolder);

                //if (controlWebList != null && controlWebList.Count()> 0)
                if (controlContentPlaceHolders != null && controlContentPlaceHolders.Count() > 0)
                {
                    var contentPlaceHolders = controlContentPlaceHolders.OfType<ContentPlaceHolder>();

                    ContentPlaceHolder placeHolder = contentPlaceHolders.FirstOrDefault(cph => string.Equals(cph.ID, ContentPlaceHolderPageBodyID));


                    if (placeHolder != null && placeHolder.HasControls())
                    {

                        var phControls = placeHolder.Controls.OfType<Control>();
                        var phControlList = phControls.Where(ctrl => ctrl is WebControl);

                        if (phControlList != null && phControlList.Count() > 0)
                        {
                            var webControls = phControlList.OfType<WebControl>();
                            var webcontrolTabulars = webControls.Where(ctrl => ctrl is TextBox || ctrl is ListBox || ctrl is CheckBox || ctrl is DropDownList);

                            if (webcontrolTabulars != null && webcontrolTabulars.Count() > 0)
                            {
                                webcontrolTabulars.ToList().ForEach(ctrl=> 
                                {
                                    ctrl.CssClass = ctrl.CssClass ?? string.Empty;
                                    var formcontrolCssClass = !ctrl.CssClass?.Contains(CssClassFormControl);

                                    if (formcontrolCssClass != null && formcontrolCssClass.Value)
                                    {
                                        ctrl.CssClass = CssClassFormControl;
                                    
                                    }

                                
                                });

                                var wcTabularList = webcontrolTabulars.
                                    Where(tag => (!(tag is TextBox) && tag.Enabled) || (tag.Enabled && tag is TextBox && !( tag as TextBox).ReadOnly ) );

                                if (wcTabularList != null && wcTabularList.Count() > 0)
                                {

                                    var priorityField = wcTabularList.OrderBy(i => i.TabIndex).FirstOrDefault(ctrl => (ctrl.TabIndex >0) && ( ctrl.TabIndex == 1 || ctrl.Enabled));

                                    if (priorityField != null)
                                    {
                                        string priorityFieldID = priorityField.ID;                                        
                                        SetFocusPriorityField(priorityField);
                                        result = true;
                                    }
                                    else 
                                    {
                                        priorityField = wcTabularList.OrderBy(i => i.TabIndex).First();
                                        string priorityFieldID = priorityField.ID;
                                        SetFocusPriorityField(priorityField);
                                        result = true;
                                    }

                                }

                            }

                        }

                    }
                    //var webControls = controlWebList.OfType<WebControl>();
                    //var webControls = controlWebList.OfType<WebControl>();
                    //if (webControls != null && webControls.Count() > 0)
                    //{

                    //    var priorityField = webControls.OrderBy(i => i.TabIndex).FirstOrDefault(ctrl => ctrl.TabIndex == 1 || ctrl.Enabled);

                    //    if (priorityField != null)
                    //    {
                    //        result = true;
                    //        this.SetFocus(priorityField);
                    //    }
                    //}

                }

            }

            return result;

        }

        protected bool FormGetDefaultFocus(out WebControl ctrlFocus ,HtmlForm form)
        {
            bool result = false;
            ctrlFocus = default(WebControl);

            //var form = this.Form;

            if (form != null && form.HasControls())
            {
                var controls = form.Controls.OfType<System.Web.UI.Control>();
                //var controlWebList = controls.Where(ctrl => ctrl is WebControl);
                var controlContentPlaceHolders = controls.Where(ctrl => ctrl is ContentPlaceHolder);

                //if (controlWebList != null && controlWebList.Count()> 0)
                if (controlContentPlaceHolders != null && controlContentPlaceHolders.Count() > 0)
                {
                    var contentPlaceHolders = controlContentPlaceHolders.OfType<ContentPlaceHolder>();

                    ContentPlaceHolder placeHolder = contentPlaceHolders.FirstOrDefault(cph => string.Equals(cph.ID, ContentPlaceHolderPageBodyID));


                    if (placeHolder != null && placeHolder.HasControls())
                    {

                        var phControls = placeHolder.Controls.OfType<Control>();
                        var phControlList = phControls.Where(ctrl => ctrl is WebControl);

                        if (phControlList != null && phControlList.Count() > 0)
                        {
                            var webControls = phControlList.OfType<WebControl>();
                            var webcontrolTabulars = webControls.Where(ctrl => ctrl is TextBox || ctrl is ListBox || ctrl is CheckBox || ctrl is DropDownList);

                            if (webcontrolTabulars != null && webcontrolTabulars.Count() > 0)
                            {
                                webcontrolTabulars.ToList().ForEach(ctrl =>
                                {
                                    ctrl.CssClass = ctrl.CssClass ?? string.Empty;
                                    var formcontrolCssClass = !ctrl.CssClass?.Contains(CssClassFormControl);

                                    if (formcontrolCssClass != null && formcontrolCssClass.Value)
                                    {
                                        ctrl.CssClass = CssClassFormControl;

                                    }


                                });

                                var wcTabularList = webcontrolTabulars.
                                    Where(tag => (!(tag is TextBox) && tag.Enabled) || (tag.Enabled && tag is TextBox && !(tag as TextBox).ReadOnly));

                                if (wcTabularList != null && wcTabularList.Count() > 0)
                                {

                                    var priorityField = wcTabularList.OrderBy(i => i.TabIndex).FirstOrDefault(ctrl => (ctrl.TabIndex > 0) && (ctrl.TabIndex == 1 || ctrl.Enabled));

                                    if (priorityField != null)
                                    {
                                        string priorityFieldID = priorityField.ID;
                                        SetFocusPriorityField(priorityField);
                                        result = true;
                                    }
                                    else
                                    {
                                        priorityField = wcTabularList.OrderBy(i => i.TabIndex).FirstOrDefault();
                                        if (priorityField != null)
                                        {
                                            string priorityFieldID = priorityField.ID;
                                            SetFocusPriorityField(priorityField);
                                            result = true;
                                        }
                                        
                                        
                                    }

                                    ctrlFocus = priorityField;

                                }

                            }

                        }

                    }
                    //var webControls = controlWebList.OfType<WebControl>();
                    //var webControls = controlWebList.OfType<WebControl>();
                    //if (webControls != null && webControls.Count() > 0)
                    //{

                    //    var priorityField = webControls.OrderBy(i => i.TabIndex).FirstOrDefault(ctrl => ctrl.TabIndex == 1 || ctrl.Enabled);

                    //    if (priorityField != null)
                    //    {
                    //        result = true;
                    //        this.SetFocus(priorityField);
                    //    }
                    //}

                }

            }

            return result;

        }

        public void SetFocusPriorityField(WebControl priorityField)
        {
            this.PriorityFieldID = priorityField.ID;
            this.PriorityFullFieldID = priorityField.ClientID;
            //this.SetFocus(priorityField);
            
            //Console.WriteLine($"the priority field '{priorityField.ID}' has focused");
        }

            protected void OnLoadGeneral()
        {
            this.MaintainScrollPositionOnPostBack = true;
        }



        public void ClearWebControl(WebControl ctrl)
        {
            //const int textboxText = 1;
            //const int textboxNumber = 2;
            //const int listbox = 3;
            //const int dropdownlist = 4;
            //const int checkbox = 5;
            //const int radio = 6;
            //const int radiolist = 7;

            controlFlags none = controlFlags.none;
            controlFlags ctrlFlags = controlFlags.none;

            ctrlFlags |= (ctrl is ListBox) ? controlFlags.listbox : none;
            ctrlFlags |= (ctrl is DropDownList) ? controlFlags.dropdownlist : none;
            ctrlFlags |= (ctrl is TextBox) ? controlFlags.txtText : none;
            ctrlFlags |= (ctrl is CheckBox) ? controlFlags.checkbox : none;

            ctrlFlags |= (ctrl is TextBox) && ((ctrl.HasAttributes) && ctrl.Attributes["type"].Contains("text")) ? controlFlags.txtText : none;
            ctrlFlags |= (ctrl is TextBox) && ((ctrl.HasAttributes) && ctrl.Attributes["type"].Contains("number")) ? (~controlFlags.txtText & ctrlFlags) | controlFlags.txtNumber : none;
            //var m = new {  };

            //int typeValue =-1;
            //int typeSelector = -1;

            //typeSelector = (ctrl is ListBox) ? listbox : typeValue;
            //typeSelector = (ctrl is DropDownList) ? dropdownlist : typeValue; typeValue = typeSelector;
            //typeSelector = (ctrl is RadioButton) ? radio : typeValue; typeValue = typeSelector;

            switch (ctrlFlags)
            {
                case controlFlags.none:
                    break;
                case controlFlags.txtText:
                    ((TextBox)ctrl).Text = string.Empty;
                    break;
                case controlFlags.txtNumber | controlFlags.txtText:
                    ((TextBox)ctrl).Text = "0";
                    break;
                case controlFlags.listbox:
                    ((ListBox)ctrl).ClearSelection();
                    break;
                case controlFlags.dropdownlist:
                    ((DropDownList)ctrl).ClearSelection();
                    break;
                case controlFlags.checkbox:
                    ((CheckBox)ctrl).Checked = false;
                    break;

            }

        }
        public void ClearWebControls(params WebControl[] ctrls)
        {
            foreach (var ctrl in ctrls)
            {

                ClearWebControl(ctrl);
            }
        }

        public void MarkTabIndexWebControl(short idx , WebControl ctrl)
        {
            if (ctrl != null)
                ctrl.TabIndex = idx;
        }

        public void MarkTabIndexWebControls(params WebControl[] ctrls)
        {
            short index = 1;
            foreach (var ctrl in ctrls)
            {
                

                MarkTabIndexWebControl(index ,ctrl);
                index++; 
            }
        }

        public string GetControlName(WebControl ctrl) 
        {
            char split = '|';
            string result = $"{ctrl?.ID}|{ctrl?.ClientID}{ctrl?.UniqueID}";

            return result;
        }

        public string GetControlNames(params WebControl[] ctrls)
        {
            char split = ',';
            string result = string.Empty;
            string name = string.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //short index = 1;
            foreach (var ctrl in ctrls)
            {

                name = GetControlName(ctrl);
                sb.Append($"{name}{split}");
                split = ',';
                //MarkTabIndexWebControl(index, ctrl);
                //index++;
            }
            result = sb.ToString();
            return result;
            
        }

        public void PageEventLoadNotPostBackForGridViewHeader(GridView gridView)
        {
            gridView.RowDataBound += GridViewEventRowDataBoundForCommands;
            gridView.RowDeleted += GridViewEventRowDeleted;
            gridView.RowUpdated += GridViewEventRowUpdated;
            
            //if (gridView.HeaderRow != null)
            //    gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        public void PageEventLoadNotPostBackForDataListHeader(DataList dataList)
        {
            //dataList.RowDataBound += GridViewEventRowDataBoundForCommands;
            //gridView.RowDeleted += GridViewEventRowDeleted;
            dataList.ItemDataBound += DataListEventRowDataBoundForCommands;
            dataList.DeleteCommand += DataListEventRowDeleted;
            dataList.UpdateCommand += DataListEventRowUpdated;

            //if (gridView.HeaderRow != null)
            //    gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        public void PageEventLoadPostBackForGridViewHeader(GridView grid)
        {
            grid.RowDataBound += GridViewEventRowDataBoundForCommands;
            grid.RowDeleted += GridViewEventRowDeleted;
            grid.RowUpdated += GridViewEventRowUpdated;

            grid.CssClass = CssClassGridview;
            if (grid.HeaderRow != null)
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;
        
        }

        public void OnGridViewHeaders(GridView grid, bool? notApplyCss = null)
        {
            if (notApplyCss.HasValue && !notApplyCss.Value)
            {
                grid.CssClass = CssClassGridview;

                if (grid.HeaderRow != null)
                    grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
            }

        }

        public void OnGridViewrRowEvents(GridView grid)
        {
            
            grid.RowDeleted += GridViewEventRowDeleted;
            grid.RowUpdated += GridViewEventRowUpdated;

        }

        public void OnGridViewrRowDataEvents(GridView grid)
        {
            grid.RowDataBound += GridViewEventRowDataBoundForCommands;
           

        }

        public void PageEventLoadPostBackForDataListHeader(DataList dataList)
        {
            //dataList.RowDataBound += GridViewEventRowDataBoundForCommands;
            //gridView.RowDeleted += GridViewEventRowDeleted;
            dataList.ItemDataBound += DataListEventRowDataBoundForCommands;
            dataList.DeleteCommand += DataListEventRowDeleted;
            dataList.UpdateCommand += DataListEventRowUpdated;

            //dataList.CssClass = CssClassGridview;
            //if (gridView.HeaderRow != null)
            //    gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        public void PageEventLoadPostBackForGridViewHeaders(params GridView[] gridViews)
        {
            foreach (var gridview in gridViews)
            {

                PageEventLoadPostBackForGridViewHeader(gridview);
            }

        }
        public void GridViewEventRowDataBoundForCommands(object sender, GridViewRowEventArgs e )
        {   
            
            //uniqueId = ctrlServer.UniqueID;
            //caption = ctrlServer.title

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var rowCellList = e.Row.Controls.OfType<System.Web.UI.Control>();
                var ctrlCellList = rowCellList.Where(ctrl => ctrl is DataControlFieldCell);

                if (ctrlCellList != null && ctrlCellList.Count() > 0)
                {
                    var cellList = ctrlCellList.OfType<DataControlFieldCell>();
                    var cmdCellList = cellList.Where(cell => cell.ContainingField is System.Web.UI.WebControls.CommandField);
                    var tplCellList = cellList.Where(cell => cell.ContainingField is System.Web.UI.WebControls.TemplateField);

                    if (tplCellList != null && tplCellList.Count() > 0)
                    {
                        //var dataCellControls = tplCellList.Select(cell=> cell.ContainingField);
                        //foreach (var dataCell in dataCellControls)
                        foreach (var dataCell in tplCellList)
                        {

                            var controls = dataCell.Controls.OfType<Control>();
                            var controlList = controls.Where(ctrl => ctrl is WebControl);

                            if (controlList != null && controlList.Count() > 0)
                            {
                                var webControls = controlList.OfType<WebControl>();
                                var webcontrolTabulars = webControls.Where(ctrl => ctrl is TextBox || ctrl is ListBox || ctrl is CheckBox || ctrl is DropDownList);

                                if (webcontrolTabulars != null && webcontrolTabulars.Count() > 0)
                                {
                                    webcontrolTabulars.ToList().ForEach(ctrl =>
                                    {
                                        ctrl.CssClass = ctrl.CssClass ?? string.Empty;
                                        var formcontrolCssClass = !ctrl.CssClass.Contains("form-control");

                                        if (formcontrolCssClass)
                                        {
                                            ctrl.CssClass = CssClassFormControl;

                                        }


                                    });


                                }

                            }

                        }
                    
                    }

                    if (cmdCellList != null && cmdCellList.Count() > 0)
                    {
                        var cmdCell = cmdCellList.First();

                        //var ctrlList = e.Row.Controls.OfType<System.Web.UI.Control>();
                        var ctrlList = cmdCell.Controls.OfType<System.Web.UI.Control>();

                        var ctrlButtonList = ctrlList.Where(ctrl => ctrl is System.Web.UI.WebControls.Button);
                        var ctrlLiteralControlList = ctrlList.Where(ctrl => ctrl is System.Web.UI.LiteralControl);

                        if (ctrlButtonList != null && ctrlButtonList.Count() > 0)
                        {
                            var buttonList = ctrlButtonList.OfType<System.Web.UI.WebControls.Button>();
                            var deleteButton = buttonList.FirstOrDefault(ctrl => string.Equals("Delete", ctrl.Text, StringComparison.InvariantCultureIgnoreCase)
                            || string.Equals("-", ctrl.Text, StringComparison.InvariantCultureIgnoreCase));

                            if (deleteButton != null)
                            {
                                deleteButton.OnClientClick = "if ( !confirm('Are you sure you want to delete this entry?')) return false;";
                            }

                        }

                        if (ctrlLiteralControlList != null && ctrlLiteralControlList.Count() > 0)
                        {
                            var literalList = ctrlLiteralControlList.OfType<System.Web.UI.LiteralControl>();
                            var literalControl = literalList.FirstOrDefault(ctrl => string.Equals("&nbsp;", ctrl.Text, StringComparison.InvariantCultureIgnoreCase));

                            if (literalControl != null)
                            {
                                literalControl.Text = string.Empty;
                            }
                        }
                    }

                }



                //e.Row.FindControl()
                //LinkButton lnkBtnDelete = e.Row.FindControl("lnkBtnDelete") as LinkButton;
                //System.Web.UI.Control ctrl = e.Row.Cells[7].Controls[2];

                //if (((System.Web.UI.WebControls.Button)ctrl).Text == "Delete" || ((System.Web.UI.WebControls.Button)ctrl).Text == "-")
                //{
                //    ((System.Web.UI.WebControls.Button)ctrl).OnClientClick = "if ( !confirm('Are you sure you want to delete this entry?')) return false;";

                //    ((System.Web.UI.LiteralControl)e.Row.Cells[7].Controls[1]).Text = "";
                //}

                // Use whatever control you want to show in the confirmation message
                //Label lblContactName = e.Row.FindControl("lblContactName") as Label;

                //lnkBtnDelete.Attributes.Add("onclick", string.Format("return confirm('Are you sure you want to delete the contact {0}?');", lblContactName.Text));

            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {

                bool isGridview = sender is GridView;

                if (isGridview)
                {
                    GridView gv = sender as GridView;
                    gv.Visible = true;
                    e.Row.TableSection = TableRowSection.TableHeader;
                }

            }

            else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {

                bool isGridview = sender is GridView;

                if (isGridview)
                {
                    GridView gv = sender as GridView;
                    gv.Visible = false;
                }
            }

        }

        public void GridViewEventRowDeleted(object sender, GridViewDeletedEventArgs e)
        {

            if (e.AffectedRows > 0)
            {

            }
            else
            {

            }

            DoRowDeleted_2(e);
            DoRowDeleted_1(e);

        }

        public void GridViewEventRowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

            if (e.AffectedRows > 0)
            {

            }
            else
            {

            }

            //UpdateModelFromGridRow(e);

            DoRowUpdated_2(e);
            //DoRowUpdated_1(e);

            //DoRowDeleted_2(e);
            //DoRowDeleted_1(e);

        }

        public void DataListEventRowUpdated(Object sender, DataListCommandEventArgs e)
        { 
        
        }

        public void DataListEventRowDeleted(Object sender, DataListCommandEventArgs e)
        {
        
        }

        public void DataListEventRowDataBoundForCommands(object sender, DataListItemEventArgs e)
        {

        }

        public void DoRowDeleted_1(GridViewDeletedEventArgs e)
        {

            var olprimaryK = e.Keys;
            var liprimaryKeys = olprimaryK.Keys;
            var liprimaryValues = olprimaryK.Values;

            var oldataV = e.Values;
            var lidataKeys = oldataV.Keys;
            var lidataValues = oldataV.Values;

            byte[] args = new byte[] { 1, 2, 4 };

            Type keyType = default(Type);
            TypeCode keyTypeCode = TypeCode.Empty;
            Type valueType = default(Type);
            TypeCode valueTypeCode = TypeCode.Empty;

            var type = args.GetType();
            var typeCode = Type.GetTypeCode(type);

            //int index = 0;
            var objectPKeys = liprimaryKeys.OfType<object>();
            var objectPValues = liprimaryValues.OfType<object>();

            var objectDKeys = lidataKeys.OfType<object>();
            var objectDValues = lidataValues.OfType<object>();

            //var objectKeys = liKeys.OfType<object>();//.Where(item=> item != null);
            //var objectValues = liValues.OfType<object>();

            var dateDictionary = new Dictionary<string, DateTime>();
            var stringDictionary = new Dictionary<string, string>();
            var guidDictionary = new Dictionary<string, Guid>();
            var decimalDictionary = new Dictionary<string, decimal>();
            var intDictionary = new Dictionary<string, int>();
            var boolDictionary = new Dictionary<string, bool>();

            //var primaryDictionaries = new Tuple<Dictionary<string, DateTime>>


            var keys = objectPKeys;
            var values = objectPValues;

            int indexPKeys = keys.Count();
            int indexPValues = values.Count();
            bool hasIndexPValids = indexPKeys == indexPValues;

            if (hasIndexPValids)
            {

                for (int index = 0; index < keys.Count(); index++)
                {
                    var key = keys.ElementAt(index);

                    if (key != null)
                    {
                        keyType = key.GetType();
                        keyTypeCode = Type.GetTypeCode(keyType);
                        string keyName = (key is string) ? $"primaryName-{key}" : key as string;

                        var value = values.ElementAt(index);
                        if (value != null)
                        {
                            valueType = value.GetType();
                            valueTypeCode = Type.GetTypeCode(valueType);
                            switch (valueTypeCode)
                            {
                                case TypeCode.Decimal:
                                    decimalDictionary.Add(keyName, Convert.ToDecimal(value));
                                    break;
                                case TypeCode.Int32:
                                    intDictionary.Add(keyName, Convert.ToInt32(value));
                                    break;
                                case TypeCode.String:

                                    stringDictionary.Add(keyName, value as string);
                                    break;
                                case TypeCode.DateTime:
                                    dateDictionary.Add(keyName, Convert.ToDateTime(value));
                                    break;
                                case TypeCode.Boolean:
                                    boolDictionary.Add(keyName, Convert.ToBoolean(value));
                                    break;
                                default:
                                    var valueString = value.ToString();
                                    if (Guid.TryParse(valueString, out Guid valueGuid))
                                    {
                                        guidDictionary.Add(keyName, valueGuid);
                                    }

                                    break;

                            }
                            //switch (valueTypeCode)
                            //{
                            //    case TypeCode.Int32:
                            //        intDictionary.Add(keyName, Convert.ToInt32(value));
                            //        break;
                            //    case TypeCode.String:
                            //        stringDictionary.Add(keyName, value as string);
                            //        break;
                            //    case TypeCode.DateTime:
                            //        dateDictionary.Add(keyName, Convert.ToDateTime(value));
                            //        break;
                            //    case TypeCode.Boolean:
                            //        boolDictionary.Add(keyName, Convert.ToBoolean(value));
                            //        break;
                            //    default:
                            //        var valueString = value.ToString();
                            //        if (Guid.TryParse(valueString, out Guid valueGuid))
                            //        {
                            //            guidDictionary.Add(keyName, valueGuid);
                            //        }
                            //        break;

                            //}

                        }
                    }

                }
            }
            else
            {

            }
            keys = objectDKeys;
            values = objectDValues;

            int indexDKeys = keys.Count();
            int indexDValues = values.Count();
            bool hasIndexDValids = indexDKeys == indexDValues;

            if (hasIndexDValids)
            {

                for (int index = 0; index < keys.Count(); index++)
                {
                    var key = keys.ElementAt(index);

                    if (key != null)
                    {
                        keyType = key.GetType();
                        keyTypeCode = Type.GetTypeCode(keyType);
                        string keyName = (key is string) ? $"foreingName-{key}" : key as string;

                        var value = values.ElementAt(index);
                        if (value != null)
                        {
                            valueType = value.GetType();
                            valueTypeCode = Type.GetTypeCode(valueType);

                            switch (valueTypeCode)
                            {
                                case TypeCode.Decimal:
                                    decimalDictionary.Add(keyName, Convert.ToDecimal(value));
                                    break;
                                case TypeCode.Int32:
                                    intDictionary.Add(keyName, Convert.ToInt32(value));
                                    break;
                                case TypeCode.String:

                                    stringDictionary.Add(keyName, value as string);
                                    break;
                                case TypeCode.DateTime:
                                    dateDictionary.Add(keyName, Convert.ToDateTime(value));
                                    break;
                                case TypeCode.Boolean:
                                    boolDictionary.Add(keyName, Convert.ToBoolean(value));
                                    break;
                                default:
                                    var valueString = value.ToString();
                                    if (Guid.TryParse(valueString, out Guid valueGuid))
                                    {
                                        guidDictionary.Add(keyName, valueGuid);
                                    }

                                    break;

                            }

                        }
                    }

                }
            }
            else
            {

            }

        }

        public void DoRowDeleted_2(GridViewDeletedEventArgs e)
        {
            //var primaryKeysSet = e.Keys;
            //var primaryKeysCat = primaryKeysSet.Keys;

            //var primaryValuesSet = e.Values;
            //var primaryValuesCat = primaryValuesSet.Values;

            var primaryKeysSet = e.Keys;
            var primaryKeys = primaryKeysSet.Keys;
            var primaryValues = primaryKeysSet.Values;

            bool primaryIndexValids = primaryKeys.Count == primaryValues.Count;

            if (primaryIndexValids)
            { }
            else 
            {
            
            }

            var foreingKeysSet = e.Values;
            var foreingKeys = foreingKeysSet.Keys;
            var foreingValues = foreingKeysSet.Values;

            bool foreingIndexValids = foreingKeys.Count == foreingValues.Count;


            if (foreingIndexValids)
            { }
            else
            {

            }


        }

        //public void DoRowDeleted_1(GridViewDeletedEventArgs e)
        //{

        //    var olprimaryK = e.Keys;
        //    var liprimaryKeys = olprimaryK.Keys;
        //    var liprimaryValues = olprimaryK.Values;

        //    var oldataV = e.Values;
        //    var lidataKeys = oldataV.Keys;
        //    var lidataValues = oldataV.Values;

        //    byte[] args = new byte[] { 1, 2, 4 };

        //    Type keyType = default(Type);
        //    TypeCode keyTypeCode = TypeCode.Empty;
        //    Type valueType = default(Type);
        //    TypeCode valueTypeCode = TypeCode.Empty;

        //    var type = args.GetType();
        //    var typeCode = Type.GetTypeCode(type);

        //    //int index = 0;
        //    var objectPKeys = liprimaryKeys.OfType<object>();
        //    var objectPValues = liprimaryValues.OfType<object>();

        //    var objectDKeys = lidataKeys.OfType<object>();
        //    var objectDValues = lidataValues.OfType<object>();

        //    //var objectKeys = liKeys.OfType<object>();//.Where(item=> item != null);
        //    //var objectValues = liValues.OfType<object>();

        //    var dateDictionary = new Dictionary<string, DateTime>();
        //    var stringDictionary = new Dictionary<string, string>();
        //    var guidDictionary = new Dictionary<string, Guid>();
        //    var decimalDictionary = new Dictionary<string, decimal>();
        //    var intDictionary = new Dictionary<string, int>();
        //    var boolDictionary = new Dictionary<string, bool>();

        //    //var primaryDictionaries = new Tuple<Dictionary<string, DateTime>>


        //    var keys = objectPKeys;
        //    var values = objectPValues;

        //    int indexPKeys = keys.Count();
        //    int indexPValues = values.Count();
        //    bool hasIndexPValids = indexPKeys == indexPValues;

        //    if (hasIndexPValids)
        //    {

        //        for (int index = 0; index < keys.Count(); index++)
        //        {
        //            var key = keys.ElementAt(index);

        //            if (key != null)
        //            {
        //                keyType = key.GetType();
        //                keyTypeCode = Type.GetTypeCode(keyType);
        //                string keyName = (key is string) ? $"primaryName-{key}" : key as string;

        //                var value = values.ElementAt(index);
        //                if (value != null)
        //                {
        //                    valueType = value.GetType();
        //                    valueTypeCode = Type.GetTypeCode(valueType);
        //                    switch (valueTypeCode)
        //                    {
        //                        case TypeCode.Decimal:
        //                            decimalDictionary.Add(keyName, Convert.ToDecimal(value));
        //                            break;
        //                        case TypeCode.Int32:
        //                            intDictionary.Add(keyName, Convert.ToInt32(value));
        //                            break;
        //                        case TypeCode.String:

        //                            stringDictionary.Add(keyName, value as string);
        //                            break;
        //                        case TypeCode.DateTime:
        //                            dateDictionary.Add(keyName, Convert.ToDateTime(value));
        //                            break;
        //                        case TypeCode.Boolean:
        //                            boolDictionary.Add(keyName, Convert.ToBoolean(value));
        //                            break;
        //                        default:
        //                            var valueString = value.ToString();
        //                            if (Guid.TryParse(valueString, out Guid valueGuid))
        //                            {
        //                                guidDictionary.Add(keyName, valueGuid);
        //                            }

        //                            break;

        //                    }
        //                    //switch (valueTypeCode)
        //                    //{
        //                    //    case TypeCode.Int32:
        //                    //        intDictionary.Add(keyName, Convert.ToInt32(value));
        //                    //        break;
        //                    //    case TypeCode.String:
        //                    //        stringDictionary.Add(keyName, value as string);
        //                    //        break;
        //                    //    case TypeCode.DateTime:
        //                    //        dateDictionary.Add(keyName, Convert.ToDateTime(value));
        //                    //        break;
        //                    //    case TypeCode.Boolean:
        //                    //        boolDictionary.Add(keyName, Convert.ToBoolean(value));
        //                    //        break;
        //                    //    default:
        //                    //        var valueString = value.ToString();
        //                    //        if (Guid.TryParse(valueString, out Guid valueGuid))
        //                    //        {
        //                    //            guidDictionary.Add(keyName, valueGuid);
        //                    //        }
        //                    //        break;

        //                    //}

        //                }
        //            }

        //        }
        //    }
        //    else
        //    {

        //    }
        //    keys = objectDKeys;
        //    values = objectDValues;

        //    int indexDKeys = keys.Count();
        //    int indexDValues = values.Count();
        //    bool hasIndexDValids = indexDKeys == indexDValues;

        //    if (hasIndexDValids)
        //    {

        //        for (int index = 0; index < keys.Count(); index++)
        //        {
        //            var key = keys.ElementAt(index);

        //            if (key != null)
        //            {
        //                keyType = key.GetType();
        //                keyTypeCode = Type.GetTypeCode(keyType);
        //                string keyName = (key is string) ? $"foreingName-{key}" : key as string;

        //                var value = values.ElementAt(index);
        //                if (value != null)
        //                {
        //                    valueType = value.GetType();
        //                    valueTypeCode = Type.GetTypeCode(valueType);

        //                    switch (valueTypeCode)
        //                    {
        //                        case TypeCode.Decimal:
        //                            decimalDictionary.Add(keyName, Convert.ToDecimal(value));
        //                            break;
        //                        case TypeCode.Int32:
        //                            intDictionary.Add(keyName, Convert.ToInt32(value));
        //                            break;
        //                        case TypeCode.String:

        //                            stringDictionary.Add(keyName, value as string);
        //                            break;
        //                        case TypeCode.DateTime:
        //                            dateDictionary.Add(keyName, Convert.ToDateTime(value));
        //                            break;
        //                        case TypeCode.Boolean:
        //                            boolDictionary.Add(keyName, Convert.ToBoolean(value));
        //                            break;
        //                        default:
        //                            var valueString = value.ToString();
        //                            if (Guid.TryParse(valueString, out Guid valueGuid))
        //                            {
        //                                guidDictionary.Add(keyName, valueGuid);
        //                            }

        //                            break;

        //                    }

        //                }
        //            }

        //        }
        //    }
        //    else
        //    {

        //    }

        //}

        public void DoRowUpdated_2(GridViewUpdatedEventArgs e)
        {
            //var primaryKeysSet = e.Keys;
            //var primaryKeysCat = primaryKeysSet.Keys;

            //var primaryValuesSet = e.Values;
            //var primaryValuesCat = primaryValuesSet.Values;

            var primaryKeysSet = e.Keys;
            var primaryKeys = primaryKeysSet.Keys;
            var primaryValues = primaryKeysSet.Values;

            bool primaryIndexValids = primaryKeys.Count == primaryValues.Count;

            if (primaryIndexValids)
            { }
            else
            {

            }

            //oldValues
            var foreingKeysSet = e.OldValues;
            var foreingKeys = foreingKeysSet.Keys;
            var foreingValues = foreingKeysSet.Values;

            bool foreingIndexValids = foreingKeys.Count == foreingValues.Count;


            if (foreingIndexValids)
            { }
            else
            {

            }

            var relatingKeysSet = e.OldValues;
            var relatingKeys = relatingKeysSet.Keys;
            var relatingValues = relatingKeysSet.Values;

            bool relatingIndexValids = relatingKeys.Count == relatingValues.Count;


            if (relatingIndexValids)
            { }
            else
            {

            }


        }

        protected override void OnSaveStateComplete(EventArgs e)
        {

            //FormBaseDefaultActions();
            //"$('[data-widget = \"pushmenu\"]').click(); });";
            //var script = "$(document).ready(function () { " +
            //    " var pushbutton = $('[data-widget = \"pushmenu\"]').first();" +
            //    "$(pushbutton).click(); });";
            //ClientScript.RegisterStartupScript(this.GetType(), "script", script, true);
        }

        protected void OnSaveStateCompleteExtend(Page view)
        {

            if (!view.IsPostBack)
            {
                var statementJS = "$('.card-tools button').click();";

                var script = $"$(document).ready(function () {{{statementJS}}});";
                //ClientScript.RegisterStartupScript(this.GetType(), "scriptCardTools", script, true);

                {
                    statementJS = "var tbl = $('.gridview'); $(tbl).DataTable({'paging': false,'lengthChange': false,'searching': true,'ordering': true,'info': true,'autoWidth': true }); " +
               " $('input[value = \"Delete\"]').attr('value', '-'); $('input[value = \"Edit\"]').attr('value', '≡'); $('a[value = \"Delete\"]').attr('value', '-'); $('a[value = \"Edit\"]').attr('value', '≡'); " +
               " $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Edit', '≡')); });" +
               "  $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Delete', '-')); });";

                    script = $"$(document).ready(function () {{{statementJS}}});";
                   view.ClientScript.RegisterStartupScript(this.GetType(), "scriptGridCommands", script, true);
                }

                {
                    //var inputs = $('.gridview input[type = \"text\"]' )
                    statementJS = $" var inputs = $('table input[type = \"text\"]' ).not(\"[class='form-control']\"); $(inputs).addClass('form-control');";


                    script = $"$(document).ready(function () {{{statementJS}}});";
                   view.ClientScript.RegisterStartupScript(this.GetType(), "scriptGridControls", script, true);
                }
            }
            else
            {

                var statementJS = string.Empty;
                var script = string.Empty;

                {
                    statementJS = "var tbl = $('.gridview'); $(tbl).DataTable({'paging': false,'lengthChange': false,'searching': true,'ordering': true,'info': true,'autoWidth': true }); " +
              " $('input[value = \"Delete\"]').attr('value', '-'); $('input[value = \"Edit\"]').attr('value', '≡'); $('a[value = \"Delete\"]').attr('value', '-'); $('a[value = \"Edit\"]').attr('value', '≡');" +
              " $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Edit', '≡')); }); " +
              " $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Delete', '-')); });";

                    script = $"$(document).ready(function () {{{statementJS}}});";
                   view.ClientScript.RegisterStartupScript(this.GetType(), "scriptGridCommands", script, true);
                }
                {
                    //var inputs = $('.gridview input[type = \"text\"]' )
                    statementJS = $" var inputs = $('table input[type = \"text\"]' ).not(\"[class='form-control']\"); $(inputs).addClass('form-control');";


                    script = $"$(document).ready(function () {{{statementJS}}});";
                   view.ClientScript.RegisterStartupScript(this.GetType(), "scriptGridControls", script, true);
                }

            }

            //var txts = $('input[type = \"text\"]')
            //$(txts).keydown(function(e) { e.preventDefault(); })

            var statementJsList = new List<Tuple<string, string>>();

            var statementJs = string.Empty;
            var scriptJs = string.Empty;
            var scriptName = string.Empty;

            {
                scriptName = "preventPostbackEnter";
                statementJs = "" +
                    "var textboxes = $('input[type = \"text\"]');" +
                    "$(textboxes).keydown(function(e) { if (e.keyCode == 13) { e.preventDefault();} })";

                scriptJs = $"$(document).ready(function () {{{statementJs}}});";
                statementJsList.Add(new Tuple<string, string>(scriptName, scriptJs));
            }

            {
                scriptName = "animateZoomingImages";
                statementJs = @"
                            $('div img').hover(
                    function() {
                        $(this).animate({ 'zoom': 1.2 }, 400);
                    },
                    function() {
                        $(this).animate({ 'zoom': 1 }, 400);
                    });
                    ";


                scriptJs = $"$(document).ready(function () {{{statementJs}}});";
                statementJsList.Add(new Tuple<string, string>(scriptName, scriptJs));
            }

            {

                if (!string.IsNullOrEmpty(this.PriorityFullFieldID))
                {

                    scriptName = "SetFocusPriority";
                    statementJs = $@"
                           var txt =  $('#{PriorityFullFieldID}');
                           if (txt != undefined || txt != null)
                            {{
                            $('#{PriorityFullFieldID}').focus();
                          }}
                    ";


                    scriptJs = $"$(document).ready(function () {{{statementJs}}});";
                    //statementJsList.Add(new Tuple<string, string>(scriptName, scriptJs));
                }
            }

            statementJsList.ForEach(sts=> {

                view.ClientScript.RegisterStartupScript(this.GetType(), $"{sts.Item1}", $"{sts.Item2}", true);

            });
        }

        protected void SignOut()
        {
            WebTools.SignOut(this);
            //HasUserEvaluated = false;
            //Eblue.Utils.SessionTools.UserId = Guid.Empty;
            //Eblue.Utils.SessionTools.ActionPanels = null;

            //PanelSelectedIndex = string.Empty;
            //LinkSelectedIndex = string.Empty;


            //FormsAuthentication.SignOut();
            //Response.Redirect(this.ResolveClientUrl("~/Default.aspx"), true);

        }

        protected void GoToHome()
        {
            Response.Redirect(this.ResolveClientUrl("~/project/whichIparticipate.aspx"), true);
        }

            protected void CheckUserAuth( Uri uri ,Eblue.Code.PanelTuple actionPanels)
        {
            var route = uri.LocalPath;
            var queryParams = uri.Query ?? string.Empty;
            //route = ResolveUrlFor(route);
            if (route.Contains("/eblue"))
            {
                route = route.Replace("/eblue", string.Empty);
            }
            //var exist = actionPanels.Exists(panel => panel.ActionPages.Exists(page => string.Equals(ResolveUrlFor(page.Route), route, StringComparison.InvariantCultureIgnoreCase)));
            var exist = actionPanels.Exists(panel => panel.ActionPages.Exists(page => string.Equals(page.Route, route, StringComparison.InvariantCultureIgnoreCase)));
            
            if (!exist)
            {
                if (!string.IsNullOrEmpty(queryParams))
                {
                    GoToUnAuthorizeRoute(route: route, query: queryParams);
                }
                else 
                {
                    GoToUnAuthorizeRoute(route);
                }
                
            }
        }

        protected void CheckUserAuth(Uri uri, Eblue.Code.ProfileTupleList userProfile)
        {
            var route = uri.LocalPath;
            var queryParams = uri.Query ?? string.Empty;

            if (route.Contains("/eblue"))
            {
                route = route.Replace("/eblue", string.Empty);
            }
            //route = ResolveUrlFor(route);
            var actionPanels = userProfile.SelectMany(thisProfile => thisProfile.Agrupations).Distinct().ToList();
            //var exist = actionPanels.Exists(panel => panel.ActionPages.Exists(page => string.Equals(ResolveUrlFor(page.Route), route, StringComparison.InvariantCultureIgnoreCase)));
            var exist = actionPanels.Exists(panel => panel.ActionPages.Exists(page => string.Equals(page.Route, route, StringComparison.InvariantCultureIgnoreCase)));

            if (!exist)
            {
                if (!string.IsNullOrEmpty(queryParams))
                {
                    GoToUnAuthorizeRoute(route: route, query: queryParams);
                }
                else
                {
                    GoToUnAuthorizeRoute(route);
                }

            }
        }

        protected bool GetSideBarFrom(out Tuple<int, int> sideBar ,Uri uri, Eblue.Code.PanelTuple container)
        {
            bool result = false;
            var route = uri.LocalPath;
            sideBar = null;
            //route = ResolveUrlFor(route);

            //var exist = actionPanels.Exists(panel => panel.ActionPages.Exists(page => string.Equals(ResolveUrlFor(page.Route), route, StringComparison.InvariantCultureIgnoreCase)));
            var exist = container.SelectMany(panel => panel.ActionPages).FirstOrDefault(link => string.Equals(link.Route, route, StringComparison.InvariantCultureIgnoreCase));
                //.Where(panel => panel.ActionPages.Exists(page => string.Equals(page.Route, route, StringComparison.InvariantCultureIgnoreCase)));

            if (exist != null)
            {
                var parent = exist.Parent;
                var option = exist;
                sideBar = new Tuple<int, int>(parent.OrderLine, option.OrderLine);
                result = true;
            }

            return result;
        }

        protected bool GetSideBarFrom(out Tuple<int, int> sideBar, Uri uri, Eblue.Code.ProfileTupleList userProfile)
        {
            bool result = false;
            var route = uri.LocalPath;
            sideBar = null;
            //route = ResolveUrlFor(route);
            var container = userProfile.SelectMany(thisProfile => thisProfile.Agrupations).Distinct().ToList();
            //var exist = actionPanels.Exists(panel => panel.ActionPages.Exists(page => string.Equals(ResolveUrlFor(page.Route), route, StringComparison.InvariantCultureIgnoreCase)));
            var exist = container.SelectMany(panel => panel.ActionPages).FirstOrDefault(link => string.Equals(link.Route, route, StringComparison.InvariantCultureIgnoreCase));
            //.Where(panel => panel.ActionPages.Exists(page => string.Equals(page.Route, route, StringComparison.InvariantCultureIgnoreCase)));

            if (exist != null)
            {
                var parent = exist.Agrupation;  //exist.Parent;
                var option = exist;
                sideBar = new Tuple<int, int>(parent.OrderLine, option.OrderLine);
                result = true;
            }

            return result;
        }

        public string ResolveUrlFor(string route)
        {
            int? idxSlash = route?.IndexOf('/');

            if (idxSlash == null || idxSlash > 0 || idxSlash < 0) route = $"/{route}";

            route = this.ResolveClientUrl($"~{route}");

            return route;
        }

        public virtual void GoToUnAuthorizeRoute()
        {
            Response.Redirect(this.ResolveClientUrl("~/UnAuthorizeRoute.aspx"));

        }

        public virtual void GoToUnAuthorizeRoute(string route)
        {
            Response.Redirect(this.ResolveClientUrl($"~/UnAuthorizeRoute.aspx?route={route}"));

        }

        public virtual void GoToUnAuthorizeRoute(string route, string query)
        {
            Response.Redirect(this.ResolveClientUrl($"~/UnAuthorizeRoute.aspx?route={route}&args={query}"));

        }

        public virtual string GetAbsoluteUrl(Uri uri) => $"?route={uri.LocalPath}";
        public virtual string GetAbsoluteUrlWithParams(Uri uri) => $"?route={uri.LocalPath}&args={uri.Query ?? string.Empty}";
        #endregion
    }


    [Flags()]
    public enum controlFlags
    {
        none,
        txtText,
        txtNumber,
        listbox = txtNumber << 1,
        dropdownlist = listbox << 1,
        checkbox = dropdownlist << 1
    }
}