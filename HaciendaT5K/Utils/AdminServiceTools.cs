using System;
using System.Linq;
using System.Data.SqlClient;
using static Eblue.Utils.DataTools;
using static Eblue.Utils.WebTools;


namespace Eblue.Utils.DataServices
{
    using static SqlParameterCollectionHerpler;
    public class SqlParameterCollectionHerpler
    {
        private System.Collections.Generic.List<SqlParameter> feed_parameters;

        public SqlParameterCollectionHerpler()
        {

            feed_parameters = new System.Collections.Generic.List<SqlParameter>();
        }

        protected SqlParameterCollectionHerpler addParameter(string name, object value)
        {
            //feed_parameters.Add.AddWithValue($"@{name}", value);
            feed_parameters.Add( new SqlParameter( $"@{name}", value ));
            return this;
        }

        protected SqlParameter[] getParameters()
        {
            //int count = feed_parameters.Count;
            //int startIn = 0;
            //SqlParameter[] result = new SqlParameter[count];
            //feed_parameters.CopyTo(result, startIn);
            var result = feed_parameters.ToArray();
            return result;
        }

        public SqlParameter[] parameters() => getParameters();

        public SqlParameterCollectionHerpler add(string name, object value) => addParameter(name, value);

        public static SqlParameterCollectionHerpler createInstance() => new SqlParameterCollectionHerpler();

    }
    public static class AdminDataServiceTools
    {
        #region general for


        public static RequestDataInfo RequestFor(RequestDataInfoConstructor constructor) => RequestDataInfo.createInstance(ctor: constructor);
        public static RequestDataInfo RequestInsertFor(RequestDataInfoConstructor expConstructor) => RequestFor(constructor: expConstructor);

        /*
         //TODO - proceder a conceder el accesso
                                if (RegisterGrantAccessHandle(out int affectedRows, userUId.Value, notHandlerException: null))
                                {
                                    var success = true;
                                    if (success)
                                    { }

                                }
                                else
                                {
                                    var fail = true;
                                    if (fail)
                                    { }

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
         */
        #endregion



        #region department for
        #region for insert

        public static bool InsertDepartment(out int affectedRows, Tuple<string, string, Guid, int> modelTuple, out Tuple<bool?, Exception> exceptionInfo)
        {
            //affectedRows = -1
            bool result;
            var model = new { 
                        DepartmentName = modelTuple.Item1, DepartmentCode = modelTuple.Item2,
                RosterDepartmentDirectorId = modelTuple.Item3,
                DepartmentOf = modelTuple.Item4,
            };

            var parameters = createInstance().
                add(nameof(model.DepartmentName), model.DepartmentName).
                add(nameof(model.DepartmentCode), model.DepartmentCode).
                add(nameof(model.RosterDepartmentDirectorId), model.RosterDepartmentDirectorId).
                add(nameof(model.DepartmentOf), model.DepartmentOf)
                .parameters();

            var parametersArray = parameters.Select(param => param.ParameterName).ToArray();
            var splitter = ",";
            var parametersString = string.Join(splitter, parametersArray);
            var parametersStringTargets = parametersString.Replace("@", string.Empty);

            var commandString = $@"INSERT INTO Department ({parametersStringTargets}) VALUES ({parametersString})";


            var reqDataInfo = RequestInsertFor(new RequestDataInfoConstructor(commandString, parameters));


            result = ExecuteOnly(out affectedRows, reqDataInfo, out exceptionInfo);


            return result;

        }
        public static bool InsertDepartmentHandle(out int affectedRows, Tuple<string,string, Guid, int> modelTuple, bool? notHandlerException = null)
        {
            bool result;

            result = InsertDepartment(out affectedRows, modelTuple, out Tuple<bool?, Exception> exceptionX);


            if (notHandlerException == null || (notHandlerException.HasValue && !notHandlerException.Value))
            {

                if (!result)
                {

                    var errorMessage = "Error at try insert a department in this app";
                    var builder = new System.Text.StringBuilder();

                    HandlerExeption(errorMessage, builder, exceptionX);

                }
                return result;
            }

            return result;
        }
        #endregion

        #region for edit
        public static bool EditDepartment(out int affectedRows, Tuple<int, string, string, Guid, int> modelTuple, out Tuple<bool?, Exception> exceptionInfo)
        {
            //affectedRows = -1
            bool result;
            //var model = new { DepartmentId = modelTuple.Item1, DepartmentName = modelTuple.Item2, RosterProgramaticCoordinatorId = modelTuple.Item3 };

            //var parameters = createInstance().
            //    add(nameof(model.DepartmentId), model.DepartmentId).
            //    add(nameof(model.DepartmentName), model.DepartmentName).
            //    add(nameof(model.DepartmentName), model.DepartmentName).
            //    add(nameof(model.RosterDepartmentDirectorId), model.RosterDepartmentDirectorId).
            //    add(nameof(model.DepartmentOf), model.DepartmentOf).
            //    .parameters();
            var model = new
            {
                DepartmentId = modelTuple.Item1,
                DepartmentName = modelTuple.Item2,
                DepartmentCode = modelTuple.Item3,
                RosterDepartmentDirectorId = modelTuple.Item4,
                DepartmentOf = modelTuple.Item5,
            };

            var sqlHelper = createInstance();
            var parameters = sqlHelper.                
                add(nameof(model.DepartmentName), model.DepartmentName).
                add(nameof(model.DepartmentCode), model.DepartmentCode).
                add(nameof(model.RosterDepartmentDirectorId), model.RosterDepartmentDirectorId).
                add(nameof(model.DepartmentOf), model.DepartmentOf)
                .parameters();


            var parametersArray = parameters.Where(param=> param.ParameterName != nameof(model.DepartmentId)).Select(param => param.ParameterName).ToArray();
            var splitter = "";
            var parametersString = string.Empty;

            var builder = new System.Text.StringBuilder();

            for (int i = 0; i < parametersArray.Length; i++)
            {
                var x = parametersArray[i];
                var y = x.Replace("@", string.Empty);

                builder.AppendLine($"{splitter} {y} = {x}");

                splitter = ",";

            }

            parametersString = builder.ToString();

            var commandString = $@"
                            Update Department set 
                             {parametersString}
                             where DepartmentId = @DepartmentId";



            parameters = sqlHelper.
                add(nameof(model.DepartmentId), model.DepartmentId).parameters();


            var reqDataInfo = RequestFor(new RequestDataInfoConstructor(commandString, parameters));


            result = ExecuteOnly(out affectedRows, reqDataInfo, out exceptionInfo);


            return result;

        }
        public static bool EditDepartmentHandle(out int affectedRows, Tuple<int, string, string, Guid, int> modelTuple, bool? notHandlerException = null)
        {
            bool result;

            result = EditDepartment(out affectedRows, modelTuple, out Tuple<bool?, Exception> exceptionX);

            if (notHandlerException == null || (notHandlerException.HasValue && !notHandlerException.Value))
            {

                if (!result)
                {

                    var errorMessage = "Error at try update a department in this app";
                    var builder = new System.Text.StringBuilder();

                    HandlerExeption(errorMessage, builder, exceptionX);

                }
                return result;
            }

            return result;
        }
        #endregion

        #region for delete
        public static bool DeleteDepartment(out int affectedRows, int primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            //affectedRows = -1
            bool result;
            var model = new { DepartmentId = primaryKey };

            var parameters = createInstance().
                add(nameof(model.DepartmentId), model.DepartmentId)
                .parameters();

            var commandString = @"Delete from Department where DepartmentId = @DepartmentId";


            var reqDataInfo = RequestFor(new RequestDataInfoConstructor(commandString, parameters));


            result = ExecuteOnly(out affectedRows, reqDataInfo, out exceptionInfo);


            return result;

        }
        public static bool DeleteDepartmentHandle(out int affectedRows, int primaryKey, bool? notHandlerException = null)
        {
            bool result;

            result = DeleteDepartment(out affectedRows, primaryKey, out Tuple<bool?, Exception> exceptionX);

            //TODO - agregate affectedRows to the if
            //if (result)
            //{
            //    result = LogHandle(out affectedRows, flags: 1, primaryKey, notHandlerException: null);
            //}

            if (notHandlerException == null || (notHandlerException.HasValue && !notHandlerException.Value))
            {

                if (!result)
                {

                    var errorMessage = "Error at try delete a department in this app";
                    var builder = new System.Text.StringBuilder();

                    HandlerExeption(errorMessage, builder, exceptionX);

                }
                return result;
            }

            return result;
        }
        #endregion

        #endregion

        #region program-area for
        #region for insert

        public static bool InsertProgramArea(out int affectedRows, Tuple<string, Guid> modelTuple, out Tuple<bool?, Exception> exceptionInfo)
        {
            //affectedRows = -1
            bool result;
            var model = new { ProgramAreaName = modelTuple.Item1, RosterProgramaticCoordinatorId = modelTuple.Item2 };

            var parameters = createInstance().
                add(nameof(model.ProgramAreaName), model.ProgramAreaName).
                add(nameof(model.RosterProgramaticCoordinatorId), model.RosterProgramaticCoordinatorId)
                .parameters();

            var commandString = @"INSERT INTO ProgramArea (ProgramAreaName, RosterProgramaticCoordinatorId) VALUES (@ProgramAreaName, @RosterProgramaticCoordinatorId)";


            var reqDataInfo = RequestInsertFor(new RequestDataInfoConstructor(commandString, parameters));


            result = ExecuteOnly(out affectedRows, reqDataInfo, out exceptionInfo);


            return result;

        }
        public static bool InsertProgramAreaHandle(out int affectedRows, Tuple<string, Guid> modelTuple, bool? notHandlerException = null)
        {
            bool result;

            result = InsertProgramArea(out affectedRows, modelTuple, out Tuple<bool?, Exception> exceptionX);

            //TODO - agregate affectedRows to the if
            //if (result)
            //{
            //    result = LogHandle(out affectedRows, flags: 1, primaryKey, notHandlerException: null);
            //}

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

        #region for edit
        public static bool EditProgramArea(out int affectedRows, Tuple<int, string, Guid> modelTuple, out Tuple<bool?, Exception> exceptionInfo)
        {
            //affectedRows = -1
            bool result;
            var model = new { ProgramAreaId = modelTuple.Item1, ProgramAreaName = modelTuple.Item2, RosterProgramaticCoordinatorId = modelTuple.Item3 };

            var parameters = createInstance().
                add(nameof(model.ProgramAreaId), model.ProgramAreaId).
                add(nameof(model.ProgramAreaName), model.ProgramAreaName).
                add(nameof(model.RosterProgramaticCoordinatorId), model.RosterProgramaticCoordinatorId)
                .parameters();

            var commandString = @"
                            Update ProgramArea set ProgramAreaName = @ProgramAreaName,  RosterProgramaticCoordinatorId = @RosterProgramaticCoordinatorId
                             where ProgramAreaId = @ProgramAreaId";


            var reqDataInfo = RequestFor(new RequestDataInfoConstructor(commandString, parameters));


            result = ExecuteOnly(out affectedRows, reqDataInfo, out exceptionInfo);


            return result;

        }
        public static bool EditProgramAreaHandle(out int affectedRows, Tuple<int, string, Guid> modelTuple, bool? notHandlerException = null)
        {
            bool result;

            result = EditProgramArea(out affectedRows, modelTuple, out Tuple<bool?, Exception> exceptionX);

            //TODO - agregate affectedRows to the if
            //if (result)
            //{
            //    result = LogHandle(out affectedRows, flags: 1, primaryKey, notHandlerException: null);
            //}

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

        #region for delete
        public static bool DeleteProgramArea(out int affectedRows, int primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            //affectedRows = -1
            bool result;
            var model = new { ProgramAreaId = primaryKey };

            var parameters = createInstance().
                add(nameof(model.ProgramAreaId), model.ProgramAreaId)
                .parameters();

            var commandString = @"Delete from ProgramArea where ProgramAreaId = @ProgramAreaId";


            var reqDataInfo = RequestFor(new RequestDataInfoConstructor(commandString, parameters));


            result = ExecuteOnly(out affectedRows, reqDataInfo, out exceptionInfo);


            return result;

        }
        public static bool DeleteProgramAreaHandle(out int affectedRows, int primaryKey, bool? notHandlerException = null)
        {
            bool result;

            result = DeleteProgramArea(out affectedRows, primaryKey, out Tuple<bool?, Exception> exceptionX);

            //TODO - agregate affectedRows to the if
            //if (result)
            //{
            //    result = LogHandle(out affectedRows, flags: 1, primaryKey, notHandlerException: null);
            //}

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