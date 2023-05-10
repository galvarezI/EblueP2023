
using System;
using System.Data.SqlClient;
using static Eblue.Utils.DataTools;
using static Eblue.Utils.WebTools;

namespace Eblue.Utils
{
    

    public static class LoginTools 
    {

        
        public static bool Log(out int affectedRows, int flags, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            //affectedRows = -1
            bool result;
            Guid uID = Guid.NewGuid();

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@UId", uID),
                new SqlParameter("@userId", primaryKey),
                new SqlParameter("@flags", flags)
                )
            {
                commandString = @"
                    insert into LoginLog(UId, UserId, Flags, Tag)
                    values 
	                      (@UId, @userId, @flags, 
	                      case (@flags)
	                      when  1 then 'grant'
	                      when  2 then 'deny'
	                      when  3 then 'attempt'
	                      end
	                       )
                    "
            };

            result = ExecuteOnly(out affectedRows, reqInfo, out exceptionInfo);


            return result;

        }

        /// <summary>
        /// (log)(handle method)this is ocurred when fail the login access
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="handlerException"></param>
        /// <returns></returns>
        public static bool LogHandle(out int affectedRows, int flags, Guid primaryKey, bool? notHandlerException = null)
        {
            bool result;

            result = Log(out affectedRows, flags, primaryKey, out Tuple<bool?, Exception> exceptionX);

            if (notHandlerException == null || (notHandlerException.HasValue && !notHandlerException.Value))
            {

                if (!result)
                {

                    var errorMessage = "Error at try log login access in this app";
                    var builder = new System.Text.StringBuilder();

                    HandlerExeption(errorMessage, builder, exceptionX);

                }
                return result;
            }

            return result;
        }

        #region grant
        public static bool RegisterGrantAccess(out int affectedRows, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            //affectedRows = -1
            bool result;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@UID", primaryKey))
            {
                commandString = @"
                    update users set
                    HasDenied = 0, IsBlocked = 0, TryAccessQuantity = 0
                    WHERE
                    UID =  @UID
                    "
            };

            result = ExecuteOnly(out affectedRows, reqInfo, out exceptionInfo);


            return result;

        }


        public static bool RegisterGrantAccessHandle(out int affectedRows, Guid primaryKey, bool? notHandlerException = null)
        {
            bool result;

            result = RegisterGrantAccess(out affectedRows, primaryKey, out Tuple<bool?, Exception> exceptionX);

            //TODO - agregate affectedRows to the if
            if (result)
            {
                result = LogHandle(out affectedRows, flags: 1, primaryKey, notHandlerException: null);
            }



            if (notHandlerException == null || (notHandlerException.HasValue && !notHandlerException.Value))
            {

                if (!result)
                {

                    var errorMessage = "Error at try register fail login access in this app";
                    var builder = new System.Text.StringBuilder();

                    HandlerExeption(errorMessage, builder, exceptionX);

                }
                return result;
            }

            return result;
        }
        #endregion

        #region deny


        public static bool RegisterDenyAccess(out int affectedRows, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            //affectedRows = -1
            bool result;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@UID", primaryKey))
            {
                commandString = @"
                    update users set
                    HasDenied = 1
                    WHERE
                    UID =  @UID
                    "
            };

            result = ExecuteOnly(out affectedRows, reqInfo, out exceptionInfo);


            return result;

        }

        
        public static bool RegisterDenyAccessHandle(out int affectedRows, Guid primaryKey, bool? notHandlerException = null)
        {
            bool result;

            result = RegisterDenyAccess(out affectedRows, primaryKey, out Tuple<bool?, Exception> exceptionX);

            //TODO - agregate affectedRows to the if
            if (result)
            {
                result = LogHandle(out affectedRows, flags: 2, primaryKey, notHandlerException: null);
            }



            if (notHandlerException == null || (notHandlerException.HasValue && !notHandlerException.Value))
            {

                if (!result)
                {

                    var errorMessage = "Error at try register fail login access in this app";
                    var builder = new System.Text.StringBuilder();

                    HandlerExeption(errorMessage, builder, exceptionX);

                }
                return result;
            }

            return result;
        }

        #endregion

        #region attempt

        #region log
        /// <summary>
        /// (log)this is ocurred when fail the login access
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="exceptionInfo"></param>
        /// <returns></returns>
        public static bool LogAttempt(out int affectedRows, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            //affectedRows = -1
            bool result;
            Guid uID = Guid.NewGuid();

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@UId", uID),
                new SqlParameter("@userId", primaryKey),
                new SqlParameter("@flags", 3)
                )
            {
                commandString = @"
                    insert into LoginLog(UId, UserId, Flags, Tag)
                    values 
	                      (@UId, @userId, @flags, 
	                      case (@flags)
	                      when  1 then 'grant'
	                      when  2 then 'deny'
	                      when  3 then 'attempt'
	                      end
	                       )
                    "
            };

            result = ExecuteOnly(out affectedRows, reqInfo, out exceptionInfo);


            return result;

        }

        /// <summary>
        /// (log)(handle method)this is ocurred when fail the login access
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="handlerException"></param>
        /// <returns></returns>
        public static bool LogAttemptHandle(out int affectedRows, Guid primaryKey, bool? notHandlerException = null)
        {
            bool result;

            result = LogAttempt(out affectedRows, primaryKey, out Tuple<bool?, Exception> exceptionX);

            if (notHandlerException == null || (notHandlerException.HasValue && !notHandlerException.Value))
            {

                if (!result)
                {

                    var errorMessage = "Error at try log fail login access in this app";
                    var builder = new System.Text.StringBuilder();

                    HandlerExeption(errorMessage, builder, exceptionX);

                }
                return result;
            }

            return result;
        }
        #endregion

        #region register

        /// <summary>
        /// this is ocurred when fail the login access
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="exceptionInfo"></param>
        /// <returns></returns>
        public static bool RegisterAttemptAccess(out int affectedRows, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            //affectedRows = -1
            bool result;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@UID", primaryKey))
            {
                commandString = @"
                    update users set
                    TryAccessQuantity = TryAccessQuantity + 1,
                    IsBlocked = IIF( (TryAccessQuantity + 1)>= 3, 1, 0)
                    WHERE
                    UID =  @UID
                    "
            };

            result = ExecuteOnly(out affectedRows, reqInfo, out exceptionInfo);


            return result;

        }

        /// <summary>
        /// (handle method)this is ocurred when fail the login access
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="handlerException"></param>
        /// <returns></returns>
        public static bool RegisterAttemptAccessHandle(out int affectedRows, Guid primaryKey, bool? notHandlerException = null)
        {
            bool result;

            result = RegisterAttemptAccess(out affectedRows, primaryKey, out Tuple<bool?, Exception> exceptionX);

            //TODO - agregate affectedRows to the if
            if (result)
            {
                result = LogAttemptHandle(out affectedRows, primaryKey, notHandlerException: null);
            }

            

            if (notHandlerException == null || (notHandlerException.HasValue && !notHandlerException.Value))
            {

                if (!result)
                {

                    var errorMessage = "Error at try register fail login access in this app";
                    var builder = new System.Text.StringBuilder();

                    HandlerExeption(errorMessage, builder, exceptionX);

                }
                return result;
            }

            return result;
        }
        #endregion

        #endregion






    }
}