using System;

using Eblue.Code;
using System.Web;
using System.Web.SessionState;
using System.Web.Profile;
using static System.Web.Profile.ProfileBase;
using static System.Web.HttpContext;
using System.Security.Permissions;
using System.Runtime.Serialization;

using uniqueidentifier = System.Guid;
using datetime = System.DateTime;

namespace Eblue.Utils
{

    [Flags]
    public enum requestmethodFlags
    {
        None,
        Get, Post
    }

    [Flags]
    public enum requestdirectorFlags
    {
        None,
        Navigation, Redirect,
        ForLock = 4
    }

    public abstract class RequestFilter {


        #region display(s)
        public abstract uniqueidentifier UniqueId { get; set; }
        public abstract requestmethodFlags RequestMethod { get; set; }

        public abstract IRequestFilterBasic FilterBasic { get; }
        public abstract IRequestFilterList FilterList { get; }

        public abstract IRequestFilterDataList FilterDataList { get; }

        #endregion
    }

    public interface IRequestFilterBasic
    {
        uniqueidentifier UniqueId { get; set; }
        requestmethodFlags RequestMethod { get; set; }

        IRequestFilterList FilterList { get; }
        IRequestFilterDataList FilterDataList { get; }

    }

    public interface IRequestFilterList: IRequestFilterBasic
    {

        string ControlNames { get; set; }
        string ControlNamesForClausule { get; set; }

        string ControlNamesForSyncData { get; set; }


    }

    public interface IRequestFilterDataList : IRequestFilterBasic
    {
        string ControlNames { get; set; }
        
        string ControlNamesForClear { get; set; }

    }

    public interface IRequestFilterProject
    {

    }


    [Serializable]
    public class RequestFilterBasic : RequestFilter, IRequestFilterBasic, ISerializable
    {
        private readonly IRequestFilterBasic feedFilterBasic;
        private IRequestFilterList feedFilterList;
        private IRequestFilterDataList feedFilterDataList;
        private uniqueidentifier uniqueId;
        private requestmethodFlags requestMethod;
        //public string controlNames;
        //public string controlNamesForClear;
        //public string controlNamesForSave;
        //public string controlNamesForAction;
        //public string controlNamesForClausule;
        //public string controlNamesForRefresh;
        //public string controlNamesForSyncData;


        #region display(s)
        public override uniqueidentifier UniqueId
        {
            get => this.uniqueId;
            set => this.uniqueId = value;
        }
        public override requestmethodFlags RequestMethod
        {
            get => this.requestMethod;
            set => this.requestMethod = value;
        }

        public override IRequestFilterBasic FilterBasic
        {
            get => this.feedFilterBasic;
            
        }

        public override IRequestFilterList FilterList => this.feedFilterList;
        public override IRequestFilterDataList FilterDataList => this.feedFilterDataList;

        #endregion

        public RequestFilterBasic() 
        {
            feedFilterBasic = this;

        }

        public RequestFilterBasic(IRequestFilterBasic filterBasic)
        {
            feedFilterBasic = filterBasic;
            var fl = feedFilterBasic.FilterList;
            var fdl = feedFilterBasic.FilterDataList;
            this.feedFilterList = fl;
            this.feedFilterDataList = fdl;
        }

        protected RequestFilterBasic(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));


            {
                var _ = nameof(uniqueId);
                var a = typeof(uniqueidentifier);
                var b = info.GetValue(_, a);
                if (b is uniqueidentifier c) this.uniqueId = c;
            }
            {
                var _ = nameof(requestMethod);
                var a = typeof(requestmethodFlags);
                var b = info.GetValue(_, a);
                if (b is requestmethodFlags c) this.requestMethod = c;
            }

            //{
            //    var _ = nameof(controlNames);
            //    var a = typeof(string);
            //    var b = info.GetValue(_, a);
            //    if (b is string c) this.controlNames = c;
            //}

            //{
            //    var _ = nameof(controlNamesForClear);
            //    var a = typeof(string);
            //    var b = info.GetValue(_, a);
            //    if (b is string c) this.controlNamesForClear = c;
            //}

            //{
            //    var _ = nameof(controlNamesForRefresh);
            //    var a = typeof(string);
            //    var b = info.GetValue(_, a);
            //    if (b is string c) this.controlNamesForRefresh = c;
            //}

            //{
            //    var requestmethodObject = info.GetValue(nameof(requestMethod), typeof(requestmethodFlags));
            //    if (requestmethodObject is requestmethodFlags requestmethod) this.requestMethod = requestmethod;
            //}
            //_Title = info..GetString("Title");
        }


        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            {
                var _ = nameof(this.uniqueId);
                var b = this.uniqueId;
                var c = typeof(uniqueidentifier);
                info.AddValue(_, b, c);
            }

            {
                var _ = nameof(this.requestMethod);
                var b = this.requestMethod;
                var c = typeof(requestmethodFlags);
                info.AddValue(_, b, c);
            }
            //{
            //    var _ = nameof(this.controlNames);
            //    var b = this.controlNames;
            //    var c = typeof(string);
            //    info.AddValue(_, b, c);
            //}

            //{
            //    var _ = nameof(this.controlNamesForClear);
            //    var b = this.controlNamesForClear;
            //    var c = typeof(string);
            //    info.AddValue(_, b, c);
            //}

            //{
            //    var _ = nameof(this.controlNamesForRefresh);
            //    var b = this.controlNamesForRefresh;
            //    var c = typeof(string);
            //    info.AddValue(_, b, c);
            //}

            //info.AddValue(nameof(requestMethod), requestMethod, typeof(requestmethodFlags));
            //info.AddValue("Title", _Title);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            GetObjectData(info, context);
        }


    }    

    //used for wichIparticipate
    [Serializable]
    public class RequestFilterList : RequestFilterBasic, IRequestFilterList
    {
        private readonly IRequestFilterList feedFilterList;
        private readonly IRequestFilterDataList feedFilterDataList;

        private string controlNames;
        //public string controlNamesForClear;
        //public string controlNamesForSave;
        private string controlNamesForClausule;
        //public string controlNamesForRefresh;
        private string controlNamesForSyncData;

        //private readonly DateTime _CheckedOut;

        #region display(s)
        public string ControlNames 
        {
            get => this.controlNames;
            set => this.controlNames = value;
        }
        public string ControlNamesForClausule
        {
            get => this.controlNamesForClausule;
            set => this.controlNamesForClausule = value;
        }

        public string ControlNamesForSyncData
        {
            get => this.controlNamesForSyncData;
            set => this.controlNamesForSyncData = value;
        }
        public IRequestFilterList FilterList => feedFilterList;
        public IRequestFilterDataList FilterDataList => feedFilterDataList;

        #endregion

        public RequestFilterList()
            : base()
        {
            this.feedFilterList = this;
            this.feedFilterDataList = null;
            //_CheckedOut = checkedOut;
        }

        protected RequestFilterList(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            //_CheckedOut = info.GetDateTime("CheckedOut");

            {
                var _ = nameof(controlNames);
                var a = typeof(string);
                var b = info.GetValue(_, a);
                if (b is string c) this.controlNames = c;
            }

            {
                var _ = nameof(controlNamesForClausule);
                var a = typeof(string);
                var b = info.GetValue(_, a);
                if (b is string c) this.controlNamesForClausule = c;
            }

            {
                var _ = nameof(controlNamesForSyncData);
                var a = typeof(string);
                var b = info.GetValue(_, a);
                if (b is string c) this.controlNamesForSyncData = c;
            }
        }

        //public DateTime CheckedOut
        //{
        //    get { return _CheckedOut; }
        //}

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            {
                var _ = nameof(this.controlNames);
                var b = this.controlNames;
                var c = typeof(string);
                info.AddValue(_, b, c);
            }

            {
                var _ = nameof(this.controlNamesForClausule);
                var b = this.controlNamesForClausule;
                var c = typeof(string);
                info.AddValue(_, b, c);
            }

            {
                var _ = nameof(this.controlNamesForSyncData);
                var b = this.ControlNamesForSyncData;
                var c = typeof(string);
                info.AddValue(_, b, c);
            }

            //info.AddValue("CheckedOut", _CheckedOut);
        }

    }

    [Serializable]
    public class RequestFilterDataList:RequestFilterBasic, IRequestFilterDataList
    {
        private readonly IRequestFilterList feedFilterList;
        private readonly IRequestFilterDataList feedFilterDataList;
        public string controlNames;
        public string controlNamesForClear;
        //public string controlNamesForSave;
        //public string controlNamesForAction;
        //public string controlNamesForRefresh;
        //public string controlNamesForSyncData;

        //private readonly DateTime _CheckedOut;

        #region display(s)
        public string ControlNames
        {
            get => this.controlNames;
            set => this.controlNames = value;
        }
        public string ControlNamesForClear
        {
            get => this.controlNamesForClear;
            set => this.controlNamesForClear = value;
        }

        public override IRequestFilterList FilterList => feedFilterList;
        public override IRequestFilterDataList FilterDataList => feedFilterDataList;

        #endregion

        public RequestFilterDataList()
            : base()
        {
            this.feedFilterList = null;
            this.feedFilterDataList = this;
            //_CheckedOut = checkedOut;

            //_CheckedOut = checkedOut;
        }

        protected RequestFilterDataList(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            {
                var _ = nameof(controlNames);
                var a = typeof(string);
                var b = info.GetValue(_, a);
                if (b is string c) this.controlNames = c;
            }

            {
                var _ = nameof(this.controlNamesForClear);
                var a = typeof(string);
                var b = info.GetValue(_, a);
                if (b is string c) this.controlNamesForClear = c;
            }
            //_CheckedOut = info.GetDateTime("CheckedOut");
        }

        //public DateTime CheckedOut
        //{
        //    get { return _CheckedOut; }
        //}

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            {
                var _ = nameof(this.controlNames);
                var b = this.controlNames;
                var c = typeof(string);
                info.AddValue(_, b, c);
            }

            {
                var _ = nameof(this.controlNamesForClear);
                var b = this.controlNamesForClear;
                var c = typeof(string);
                info.AddValue(_, b, c);
            }

            
            //info.AddValue("CheckedOut", _CheckedOut);
        }


    }

    [Flags]
    public enum UserTypeFlags {
        None,
        UserType = 1,
        UserTypeAdmin = 2,          //manager, develop, admin,administrator   
        UserTypeCoordinator = 4,     //manager, develop, coordinator,administrator,projectOfficer,     

        UserTypeOwner = 8,           //manager, develop
        UserTypeProject = 16,


    }
    public class IsUserTypeResult
    {
        public readonly Tuple<bool, bool> Args;
        public readonly UserTypeFlags Flags;
        public bool IsNull { get { return Args.Item1; } }
        public bool IsTrue { get { return Args.Item2; } }

        public IsUserTypeResult(UserLogged userLogged, UserTypeFlags flags = UserTypeFlags.None) 
        {

            bool argZ = userLogged != null;
            bool argY = false;
            this.Flags = flags;

            switch (flags)
            {
                case UserTypeFlags.UserType:
                    argY = argZ && (
                   
                    userLogged.IsStudent ||
                    userLogged.IsDeveloper ||
                    userLogged.IsCoordinator || 
                    userLogged.IsPlanViewer  || 
                    //New Integration
                    userLogged.IsAdministrator ||
                    userLogged.IsBudgetOfficer ||
                    userLogged.IsProjectOfficer ||
                    userLogged.IsHumanROfficer ||
                    userLogged.IsAESOfficer);
                    break;
                case UserTypeFlags.UserTypeAdmin:
                    argY = argZ && (
                                       
                    userLogged.IsAdministrator );
                    break;
                case UserTypeFlags.UserTypeCoordinator:
                    argY = argZ && (
                    userLogged.IsManager ||
                    userLogged.IsDeveloper ||
                    //New Integration
                    userLogged.IsBudgetOfficer ||
                   
                    
                    userLogged.IsProjectOfficer);
                    break;
                case UserTypeFlags.UserTypeOwner:
                    argY = argZ && (
                    userLogged.IsManager ||
                    userLogged.IsDeveloper ||
                    //New integration
                    userLogged.IsAdministrator);
                    break;
                case UserTypeFlags.UserTypeProject:
                    argY = argZ && (
                    userLogged.CanBePI ||
                    
                    userLogged.IsDeveloper ||
                    userLogged.IsCoordinator || 
                    //New Integration
                    
                    userLogged.IsBudgetOfficer ||
                    userLogged.IsProjectOfficer ||
                    userLogged.IsHumanROfficer ||
                    userLogged.IsAESOfficer);
                    break;
            }
            
            this.Args = new Tuple<bool, bool>(argZ,argY);
        }

    }
    public static class SessionTools
    {

        public static bool IsActivityCurrent
        {

            get => (HasSession && Current.Session[nameof(ActivityCurrent)] is DateTime dt) ? (dt == DateTime.MinValue ? true: false) : default(bool);
            

        }

        public static bool IsActivityInitial
        {

            get => (HasSession && Current.Session[nameof(ActivityInitial)] is DateTime dt) ? (dt == DateTime.MinValue ? true : false) : default(bool);


        }

        public static DateTime? ActivityCurrent
        {

            get => (HasSession && Current.Session[nameof(ActivityCurrent)] is DateTime dt) ? dt : default(DateTime?);
            set => ((HasSession && Current.Session is HttpSessionState ss) ? ss : Current.Session)[nameof(ActivityCurrent)] =
                (value.HasValue) ? value.Value : DateTime.MinValue;

        }

        public  static DateTime? ActivityInitial
        {

            get => (HasSession && Current.Session[nameof(ActivityInitial)] is DateTime dt) ? dt : default(DateTime?);
            set => ((HasSession && Current.Session is HttpSessionState ss) ? ss : Current.Session)[nameof(ActivityInitial)] = 
                (value.HasValue) ? value.Value : DateTime.MinValue  ;

        }

        //public DateTime? ProjectRoleBlop
        //{

        //    get => (HasSession && Current.Session["ProjectRoleBlop"] is DateTime dt) ? dt : default(DateTime?);
        //    set => ((HasSession && Current.Session is HttpSessionState ss) ? ss : Current.Session)["ProjectRoleBlop"] = value;

        //}

        public static IsUserTypeResult EvalIsUserType(UserTypeFlags flags = UserTypeFlags.None)
        {
            var userLogged = UserInfo;
            IsUserTypeResult result = new IsUserTypeResult(userLogged, flags);
            return result;
        }

        public static IsUserTypeResult EvalIsUserType(UserLogged userLogged, UserTypeFlags flags = UserTypeFlags.None)
        {           
            IsUserTypeResult result = new IsUserTypeResult(userLogged, flags);
            return result;
        }


        public static Guid UserId 
        {
            get {
                var session = System.Web.HttpContext.Current.Session;
                object sessionObject = null;

                if (session != null)
                {
                    sessionObject = session["UserId"];
                }

                
                Guid result;

                if (sessionObject == null)
                {
                    result = Guid.Empty;
                }
                else 
                {
                    Guid.TryParse(sessionObject.ToString(), out result);
                }               
                
                return result;
            }
            set {
                var session = System.Web.HttpContext.Current.Session;                

                if (session != null)
                {
                    session["UserId"] = value;
                }
                
            }
        }
        
        public static Guid RosterId
        {
            get
            {
                var session = System.Web.HttpContext.Current.Session;
                object sessionObject = null;

                if (session != null)
                {
                    sessionObject = session["RosterId"];
                }


                Guid result;

                if (sessionObject == null)
                {
                    result = Guid.Empty;
                }
                else
                {
                    Guid.TryParse(sessionObject.ToString(), out result);
                }

                return result;
            }
            set
            {
                var session = System.Web.HttpContext.Current.Session;

                if (session != null)
                {
                    session["RosterId"] = value;
                }

            }
        }
        public static PanelTuple ActionPanels
        {
            get
            {
                var session = System.Web.HttpContext.Current.Session;
                object sessionObject = null;

                if (session != null)
                {
                    sessionObject = session["ActionPanels"];
                }
                PanelTuple result = null;

                if (sessionObject != null)
                
                {
                    result = sessionObject as PanelTuple;
                }

                return result;
            }
            set
            {
                var session = System.Web.HttpContext.Current.Session;
                

                if (session != null)
                {
                    session["ActionPanels"] = value;
                }

            }
        }

        public static UserLogged UserInfo
        {
            get
            {
                var session = System.Web.HttpContext.Current.Session;
                object sessionObject = null;

                if (session != null)
                {
                    sessionObject = session["UserInfo"];
                }
                
                UserLogged result = null;

                if (sessionObject != null)

                {
                    result = sessionObject as UserLogged;
                }

                return result;
            }
            set
            {
                var session = System.Web.HttpContext.Current.Session;
                

                if (session != null)
                {
                    session["UserInfo"] = value;
                }
                
            }
        }

        public static bool HasSession 
        {
            get {
                bool result = !(System.Web.HttpContext.Current.Session == null);
                return result  ;
            }
        }

        public static string PanelSelectedIndex
        {
            get
            {
                string result = string.Empty;              

                if (HasSession)
                {
                    var session = System.Web.HttpContext.Current.Session;
                    result = session["PanelSelectedIndex"] as string;
                }

                return result;
            }
            set
            {
                if (HasSession)
                {
                    var session = System.Web.HttpContext.Current.Session;
                    session["PanelSelectedIndex"] = value;
                }

            }
        }

        public static string LinkSelectedIndex
        {
            get
            {
                string result = string.Empty;

                if (HasSession)
                {
                    var session = System.Web.HttpContext.Current.Session;
                    result = session["LinkSelectedIndex"] as string;
                }

                return result;
            }
            set
            {
                if (HasSession)
                {
                    var session = System.Web.HttpContext.Current.Session;
                    session["LinkSelectedIndex"] = value;
                }

            }
        }

        public static Guid? ProjectWorkFlowDefaultID
        {
            get
            {
                Guid? result = default(Guid?);

                if (HasSession)
                {
                    var session = System.Web.HttpContext.Current.Session;
                    var objectResult = session["ProjectWorkFlowDefaultID"];
                    bool isGuidType = objectResult is Guid?;

                    if (isGuidType)
                    {
                        result = (Guid)objectResult;
                    
                    }
                }

                return result;
            }
            set
            {
                if (HasSession)
                {
                    var session = System.Web.HttpContext.Current.Session;
                    session["ProjectWorkFlowDefaultID"] = value;
                }

            }
        }

        public static ProfileTupleList UserProfile
        {
            get
            {
                ProfileTupleList result = new ProfileTupleList();

                if (HasSession)
                {
                    var session = System.Web.HttpContext.Current.Session;
                    var resultObject = session["UserProfile"];
                    if (resultObject != null)
                        result = session["UserProfile"] as ProfileTupleList;
                }

                return result;
            }
            set
            {
                if (HasSession)
                {
                    var session = System.Web.HttpContext.Current.Session;
                    session["UserProfile"] = value;
                }

            }
        }

        public static bool? HasUserEvaluated
        {
            get
            {
                
                bool? result = default(bool?);

                if (HasSession)
                {                 
                    

                    var sessionState = Current.Session;
                    var nullableObject = sessionState["HasUserEvaluated"];

                    if (nullableObject != null && nullableObject is IConvertible)
                    {
                        IConvertible exp = nullableObject as IConvertible;
                        if (exp != null)
                        {
                            bool boolean ;
                            boolean = exp.ToBoolean(null);
                            result = boolean;

                        }
                        else
                        {
                            bool boolean;
                            boolean = Convert.ToBoolean(nullableObject);
                            result = boolean;
                            
                        }
                    }
                }

                return result;
            }
            set
            {
                if (HasSession)
                {
                    var sessionState = Current.Session;
                    bool thisValue = value ?? false;
                    sessionState["HasUserEvaluated"] = thisValue;                   
                }

            }
        }

        public static bool? HasErrorEvaluating
        {
            get
            {

                bool? result = default(bool?);

                if (HasSession)
                {


                    var sessionState = Current.Session;
                    var nullableObject = sessionState["HasErrorEvaluating"];

                    if (nullableObject != null && nullableObject is IConvertible)
                    {
                        IConvertible exp = nullableObject as IConvertible;
                        if (exp != null)
                        {
                            bool boolean;
                            boolean = exp.ToBoolean(null);
                            result = boolean;

                        }
                        else
                        {
                            bool boolean;
                            boolean = Convert.ToBoolean(nullableObject);
                            result = boolean;

                        }
                    }
                }

                return result;
            }
            set
            {
                if (HasSession)
                {
                    var sessionState = Current.Session;
                    bool thisValue = value ?? false;
                    sessionState["HasErrorEvaluating"] = thisValue;
                }

            }
        }

        public static void CreateNewProfile()
        {
            var identityName = IdentityUserName;
            if (!string.IsNullOrEmpty(identityName))
            {
                //var newProfile = Create("fmontano", true);
                ProfileBase newProfile = new ProfileBase();
                newProfile.Initialize("fmontano", false);
                CurrentProfile = newProfile;
            }

        }


        //ProfileBase

        public static ProfileBase CurrentProfile
        {
            get => (HasSession) ? Current.Session["CurrentProfile"] as ProfileBase : default(ProfileBase);
            set => SetSessionValue<ProfileBase>("CurrentProfile", value);
        }
        public static string IdentityUserName
        {
            get => (HasSession) ? Current.Session["IdentityUserName"] as string : string.Empty;
            set => SetSessionValue<string>("IdentityUserName", value);
                
                //SetIdentityUserName();
            //get
            //{
            //    string result = string.Empty;

            //    if (HasSession)
            //    {
            //        var session = System.Web.HttpContext.Current.Session;
            //        result = session["IdentityUserName"] as string;
            //    }

            //    return result;
            //}
            //set
            //{
            //    if (HasSession)
            //    {
            //        var session = System.Web.HttpContext.Current.Session;
            //        session["IdentityUserName"] = value;
            //    }

            //}
        }
        public static void SetSessionValue<T>(string uId,T value)
        {
            if (HasSession)
            {
                Current.Session[uId] = value;
            }
        }
        public static void SetIdentityUserName(string value)
        {
            if (HasSession) {
                Current.Session["IdentityUserName"] = value;
            }
        }
    }
}