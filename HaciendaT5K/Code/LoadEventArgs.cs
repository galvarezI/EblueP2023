using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eblue.Code
{
    public class LoadEventArgs : EventArgs
    {
        public bool? IsBasic { get; set; }
        public bool? IsHome { get; set; }

        public bool? IsError { get; set; }
        public bool? IsUnAuthorize { get; set; }

        public bool? IsScreenLock { get; set; }
        public bool? IsWaitAuthorizeLock { get; set; }

        public bool? IsTryOut { get; set; }

        public bool? IsWhichIparticipate { get; set; }


        public LoadEventArgs() : base()
        {
        }
        public LoadEventArgs(bool? isBasic = null, bool? isHome = null, bool? isError = null, bool? isUnAuthorize = null, bool? isScreenLock = null, 
            bool? isWaitAuthorizeLock = null, bool? isTryOut = null, bool? isWhichIparticipate = null) : base() 
        {
            this.IsBasic = isBasic;
            this.IsHome = isHome;
            this.IsError = isError;
            this.IsUnAuthorize = isUnAuthorize;
            this.IsScreenLock = isScreenLock;
            this.IsWaitAuthorizeLock = isWaitAuthorizeLock;
            this.IsTryOut = isTryOut;
            this.IsWhichIparticipate = isWhichIparticipate;
        }
    }
}