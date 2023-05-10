using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Eblue.Utils
{
    using core;
    using expfailure = core.failure;
    using expsqlfailure = core.sqlfailure;
    public static class DataTools
    {



        public static bool FirstOrDefaultRowAndColumn(out object value , RequestDataInfo info)       
        {

            bool result = false;
            value = default(object);

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        info.commandString, cn);
                    cn.Open();

                    if (info.commandtype.HasValue)
                    {
                        cmd.CommandType = info.commandtype.Value;
                    }
                    

                    cmd.Parameters.AddRange(info.parameters);

                    SqlDataReader reader = cmd.ExecuteReader();
                    result = reader.HasRows;
                    if (result)
                    {
                        if (reader.Read())
                        {
                            value = reader.GetValue(0);
                        }

                        


                    }
cn.Close();
                }

            }
            catch (Exception ex)
            {
                var genericmsg = info.genericStringFailure?.Trim();
                var hasgenericmsg = string.IsNullOrEmpty(genericmsg);

                result = false;
                if (ex is SqlException sqlerror)
                {
                    var sqlfailure = new expsqlfailure();
                    sqlfailure.overrideFor(dmd: sqlerror);
                    sqlfailure.setkind = new kindfailure(value: binary.@sqlfailure);
                    if (!hasgenericmsg) {
                        sqlfailure.SetText(txt: genericmsg);
                    }

                    throw sqlfailure;// sqlerror;
                }
                else {
                    var exfailure = new expfailure();
                    exfailure.overrideFor(dmd: ex);
                    exfailure.setkind = new kindfailure( value: binary.@failure );
                    if (!hasgenericmsg)
                    {
                        exfailure.SetText(txt: genericmsg);
                    }
                    throw exfailure;

                }
            }

            #region error handler
            /*
             * 
             catch (Exception ex)
            {
                isSqlException = ex is SqlException;

                if (isSqlException.Value)
                {
                    resultException = new Tuple<bool?, Exception>(isSqlException, ex as SqlException);
                }
                else
                {
                    resultException = new Tuple<bool?, Exception>(isSqlException, ex);
                }

                

            }
            finally             
            {

                if (isSqlException != null)
                {

                    exceptionInfo = resultException;

                }
                else 
                {
                    
                    exceptionInfo = new Tuple<bool?, Exception>(isSqlException, default(Exception) );
                }
            
            }
             * 
             * */
            #endregion


            return result;
        }


        public static bool FetchData(RequestDataInfo info, Action<SqlDataReader> push )
        {

            bool result = false;
            

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        info.commandString, cn);
                    cn.Open();

                    cmd.Parameters.AddRange(info.parameters);

                    SqlDataReader reader = cmd.ExecuteReader();
                    result = reader.HasRows;
                    if (result)
                    {
                        while (reader.Read())
                        {

                            push(reader);
                            
                            //value = reader.GetValue(0);
                        }

                       


                    }
 cn.Close();
                }

            }
            catch (Exception ex)
            {

                if (ex is SqlException)
                { }
            }


            return result;


        }

        public static bool FetchDataFirstOrDefault(RequestDataInfo info, Action<SqlDataReader> push)
        {

            bool result = false;


            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        info.commandString, cn);
                    cn.Open();

                    cmd.Parameters.AddRange(info.parameters);

                    SqlDataReader reader = cmd.ExecuteReader();
                    result = reader.HasRows;
                    if (result)
                    {
                        if (reader.Read())
                        {

                            push(reader);                            
                        }

                    }
                    cn.Close();
                }

            }
            catch (Exception ex)
            {

                if (ex is SqlException)
                { }
            }


            return result;


        }

        public static bool FetchDataFirstOrDefault(RequestDataInfo info, Action<SqlDataReader> push, out Tuple<bool, Exception> exceptionInfo)
        {

            bool result = false;
            exceptionInfo = default(Tuple<bool, Exception>);
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        info.commandString, cn);
                    cn.Open();

                    cmd.Parameters.AddRange(info.parameters);

                    SqlDataReader reader = cmd.ExecuteReader();
                    result = reader.HasRows;
                    if (result)
                    {
                        if (reader.Read())
                        {

                            push(reader);
                        }

                    }
                    cn.Close();
                }

            }
            catch (Exception ex)
            {
                bool isSqlException = false;

                if (ex is SqlException)
                {
                    var sqlException = ex as SqlException;
                    isSqlException = true;
                    exceptionInfo = new Tuple<bool, Exception>(isSqlException, sqlException);
                    //_ = new Tuple<bool, Exception>(isSqlException, sqlException);
                    //resultException = new Tuple<bool, Exception>(isSqlException, sqlException);
                }
                else {
                    exceptionInfo = new Tuple<bool, Exception>(isSqlException, ex);
                }
                //exceptionInfo = new Tuple<bool, Exception>(isSqlException, ex);
            }


            return result;


        }

        public static bool FetchDataFirstOrDefaultBetter(RequestDataInfo info, Action<SqlDataReader> push, out Tuple<bool?, Exception> exceptionInfo)
        {

            System.Threading.Tasks.Task<int> ACT;

            bool result = false;
            bool? isSqlException = default(bool?);
            exceptionInfo = default(Tuple<bool?, Exception>);
            Tuple<bool?, Exception> resultException = default(Tuple<bool?, Exception>);
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        info.commandString, cn);
                    cn.Open();

                    cmd.Parameters.AddRange(info.parameters);

                    SqlDataReader reader = cmd.ExecuteReader();
                    result = reader.HasRows;
                    if (result)
                    {
                        if (reader.Read())
                        {

                            push(reader);
                        }

                    }
                    cn.Close();
                }

            }
            catch (Exception ex)
            {
                isSqlException = ex is SqlException;

                if (isSqlException.Value)
                {
                    resultException = new Tuple<bool?, Exception>(isSqlException, ex as SqlException);
                }
                else
                {
                    resultException = new Tuple<bool?, Exception>(isSqlException, ex);
                }

                

            }
            finally             
            {

                if (isSqlException != null)
                {

                    exceptionInfo = resultException;

                }
                else 
                {
                    
                    exceptionInfo = new Tuple<bool?, Exception>(isSqlException, default(Exception) );
                }
            
            }


            return result;


        }

        public static bool FetchData(RequestDataInfo info, Action<SqlDataReader> push, out Tuple<bool?, Exception> exceptionInfo)
        {

            bool result = false;
            bool? isSqlException = default(bool?);
            exceptionInfo = default(Tuple<bool?, Exception>);
            Tuple<bool?, Exception> resultException = default(Tuple<bool?, Exception>);
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        info.commandString, cn);
                    cn.Open();

                    cmd.Parameters.AddRange(info.parameters);

                    SqlDataReader reader = cmd.ExecuteReader();
                    result = reader.HasRows;
                    if (result)
                    {
                        while (reader.Read())
                        {

                            push(reader);

                            //value = reader.GetValue(0);
                        }




                    }
                    //result = reader.HasRows;
                    //if (result)
                    //{
                    //    if (reader.Read())
                    //    {

                    //        push(reader);
                    //    }

                    //}
                    cn.Close();
                }

            }
            catch (Exception ex)
            {
                isSqlException = ex is SqlException;

                if (isSqlException.Value)
                {
                    resultException = new Tuple<bool?, Exception>(isSqlException, ex as SqlException);
                }
                else
                {
                    resultException = new Tuple<bool?, Exception>(isSqlException, ex);
                }



            }
            finally
            {

                if (isSqlException != null)
                {

                    exceptionInfo = resultException;

                }
                else
                {

                    exceptionInfo = new Tuple<bool?, Exception>(isSqlException, default(Exception));
                }

            }


            return result;


        }

        public static bool ExecuteOnly(out int affectedRows, RequestDataInfo info)
        {

            bool result = false;
            affectedRows = -1;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        info.commandString, cn);
                    cn.Open();

                    cmd.Parameters.AddRange(info.parameters);

                     affectedRows = cmd.ExecuteNonQuery();

                    cn.Close();

                    result = true;

                }

            }
            catch (Exception ex)
            {

                if (ex is SqlException)
                { }
            }


            return result;
        }

        public static bool ExecuteOnly(out int affectedRows, RequestDataInfo info, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool? isSqlException = default(bool?);
            exceptionInfo = default(Tuple<bool?, Exception>);
            Tuple<bool?, Exception> resultException = default(Tuple<bool?, Exception>);
            bool result = false;
            affectedRows = -1;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        info.commandString, cn);
                    cn.Open();

                    if (info.parameters.Count() > 0)
                    {
                        cmd.Parameters.AddRange(info.parameters);
                    }

                    

                    affectedRows = cmd.ExecuteNonQuery();

                    cn.Close();

                    result = true;

                }

            }
            catch (Exception ex)
            {
                isSqlException = ex is SqlException;

                if (isSqlException.Value)
                {
                    resultException = new Tuple<bool?, Exception>(isSqlException, ex as SqlException);
                }
                else
                {
                    resultException = new Tuple<bool?, Exception>(isSqlException, ex);
                }
            }

            finally
            {

                if (isSqlException != null)
                {

                    exceptionInfo = resultException;

                }
                else
                {

                    exceptionInfo = new Tuple<bool?, Exception>(isSqlException, default(Exception));
                }

            }


            return result;

           


        }

    }

    public class RequestDataInfoConstructor
    {

        private readonly SqlParameter[] feed_argsParameters;
        private readonly string feed_commandString;
        public SqlParameter[] DisplayArgsParameters => feed_argsParameters;
        public string DisplayCommandString => feed_commandString;

        public RequestDataInfoConstructor(string commandString, params SqlParameter[] args)
        {
            feed_argsParameters = args;
            feed_commandString = commandString;
        }

    }

    public class RequestDataInfo 
    {
        public string commandString { get; set; }
        public string genericStringFailure { get; set; }

        public System.Data.CommandType? commandtype { get; set; }

        public SqlParameter[] parameters { get; set; }

        public RequestDataInfo() {
            parameters = new SqlParameter[] { };
        }
        public RequestDataInfo( params SqlParameter[] args) {
            parameters = args;
        }

        public RequestDataInfo(string cmdString , params SqlParameter[] args)
        {
            parameters = args;
            commandString = cmdString;
        }

        public static RequestDataInfo createInstance(string cmdString, params SqlParameter[] args) => new RequestDataInfo(cmdString, args);
        public static RequestDataInfo createInstance(RequestDataInfoConstructor ctor) => new RequestDataInfo(ctor.DisplayCommandString, ctor.DisplayArgsParameters);

    }

    [Flags]
    public enum SelectDataInfo
    { 
none,

    
    }
}