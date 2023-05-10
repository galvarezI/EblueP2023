using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Eblue.Utils;
using static Eblue.Utils.DataTools;
using Eblue.Code.Models;
using Eblue.Code;

using Eblue.Project;

using static Eblue.Utils.WebTools;
using static Eblue.Utils.SectionTools;
using static Eblue.Utils.PageTools;
using TypePermission = Eblue.Code.Models.RolePermission;
using static Eblue.Utils.ReportTools;


namespace Eblue.Utils
{
    using sqluniqueidentifier = Guid;
    #region core references
    using core;
    using msobject = Object;
    using msboolsafe = Nullable<bool>;
    using msexception = Exception;
    using expfailure = core.failure;
    using expsqlfailure = core.sqlfailure;
    #endregion

    #region json definitions
    [Serializable]
    public class Rjson
    {
        public Rtmold tmold { get; set; }
        public Rthead thead { get; set; }
        public Rtbody tbody { get; set; }
        //public 
    }

    public class Rtmold
    {
        public string caption { get; set; }
    }

    public class Rthead
    {
        public string items { get; set; }
    }

    public class Rtbody
    {
        public string rows { get; set; }
        public string cols { get; set; }
    }
    public class JsonString
    {
        public string tsql { get; set; }
        public Dictionary<string, string> dictionary { get; set; }

        public JsonString(string data)
        {
            tsql = data;
            dictionary = new Dictionary<string, string>();
            char delimiter = ';';
            char separator = '=';
            char grouping = ',';
            string[] records = data.Split(delimiter);
            int idx = 0;
            int lim = 0;
            int rds = records.Length;
            int fds = 0;
            string keydata = string.Empty;
            string valuedata = string.Empty;

            for (; idx < rds; idx++)
            {

                var record = records[idx];
                var fields = record.Split(separator);
                fds = fields.Length;
                if (fds >= 2)
                {
                    keydata = fields[0]?.ToLower();
                    valuedata = fields[1];
                    bool hasempty = !dictionary.ContainsKey(keydata);
                    if (hasempty)
                    {
                        dictionary.Add(keydata, valuedata);
                    }
                }

            }

        }

        public Rtsql ASRtsql(Rtsql arg = default(Rtsql))
        {
            Rtsql res = arg ?? new Rtsql();
            try
            {
                var empty = string.Empty;
                res.name = dictionary.ContainsKey("name") ? dictionary["name"] : empty;
                res.caption = dictionary.ContainsKey("caption") ? dictionary["caption"] : empty;
                res.items = dictionary.ContainsKey("items") ? dictionary["items"] : empty;
                res.rows = dictionary.ContainsKey("rows") ? dictionary["rows"] : empty;
                res.cols = dictionary.ContainsKey("cols") ? dictionary["cols"] : empty;
            }
            catch (msexception ex)
            {

                throw new expfailure(ex);
            }
            
           
            return res;
        }
    }

    [Serializable]
    public class Rtsql
    {
        public string name { get; set; }
        public string caption { get; set; }
        public string purpose { get; set; }

        public string items { get; set; }
        public string rows { get; set; }
        public string cols { get; set; }

        //public Rthead ASRthead() {

        //}
    }

    public class JsonPivot {
        bool jsonValid;
        string jsonText;

        public bool IsValid() => !jsonValid;
        public string GetJson() => jsonText;

        public JsonPivot(Rtsql src)
        {
            var rowJson = "[@rows]";
            String rowString = null;
            String colString = null;
            var colJson = "[@cols]";
            var rowlist = src.rows.Split(',');
            var collist = src.cols.Split(',');
            char rsep = '\0';
            char csep = '\0';
            int idx = 0;
            bool valid = true;

            for (; idx < rowlist.Length; idx++) {
                var row = $"\"{rowlist[idx]}\"";
                valid = !(src.items.IndexOf(row) > -1);
                if (valid && !jsonValid)
                {
                    jsonValid = true;
                }

                if (String.IsNullOrEmpty(rowString))
                {
                    rowString = row;
                    rsep = ',';
                }
                else
                {
                    rowString = string.Concat(rowString, rsep, row);// rowString.Append()

                }

                //rowString = string.Concat(rowString, rsep, row);// rowString.Append()
                //rsep = ',';
            }

            rowJson = rowJson.Replace("@rows", rowString);

            for (idx = 0; idx < collist.Length; idx++)
            {
                var col = $"\"{collist[idx]}\"";
                valid = !(src.items.IndexOf(col) > -1);
                if (valid && !jsonValid)
                {
                    jsonValid = true;
                }
                if ( String.IsNullOrEmpty(colString))
                {
                    colString = col;
                    csep = ',';
                }
                else {
                    colString = string.Concat(colString, csep, col);// rowString.Append()
                    
                }
                
            }
            colJson = colJson.Replace("@cols", colString);

            var fname = $"\"name\"";
            var vname = $"\"{src.name}\"";
            var fcaption = $"\"caption\"";
            var vcaption = $"\"{src.caption}\"";
            var frows = $"\"rows\"";
            var fcols = $"\"cols\"";
            var fitems = $"\"items\"";
            var vitems = src.items.Replace("items=", "");
            var json = $"{{{fname}:{vname}, {fcaption}:{vcaption}, {frows}:{rowJson}, {fcols}:{colJson}, {fitems}:{vitems}}}";
            this.jsonText = json;
        }
    
    }
    #endregion



    public static class @convert {
        //method as delegate; object as delegate func<int>
        public static exp ensure<exp>(this msobject obj)  //in this case string is not int
        {
            exp res = default(exp);
            if (obj is exp @value) {
                res = @value;
            }
            return res;
        }
    }

    public class reportresult      //used for scalar report result
    {
        private expfailure varfailure;
        private msobject varobject;
        private msboolsafe nonbool;


        public msobject GetObject() => varobject ?? new msobject();
        public expfailure GetFailure() => varfailure ?? new expfailure();

        public bool HasFlag { get => nonbool.HasValue; }
        public bool HasFail { get => nonbool.HasValue && !(nonbool.Value); }
        public bool HasOkay { get => nonbool.HasValue && (nonbool.Value); }
        public reportresult()  //when this case no ha fallado ni tampoco tiene resultado
        {

        }
        public reportresult(msobject obj)
        {
            nonbool = true; varobject = obj;
        }
        public reportresult(expfailure exc)
        {
            nonbool = false; varfailure = exc;
        }

    }


    public static class ReportTools
    {

       

        

      

        

        public class reportttemplate {

            public void forextruct<exp>()
            where exp:struct
            {
                //this.
                exp? value = default(exp);
            }
        }
        /*
         *
           //binarysafe = Nullable<binary>
           //binary
           //@ref<exp> where exp:struct
           //flags<exp> where exp: struct, Enum{}
           //
           //ASformatprovider {object ConvertTo(Type)}
           //ASenumeration{}, AStransferable {} AStranslatable {} 
           //ASconvertible{ int ToInt32(provider)} ASequatable{}
           //AScomparable{} ASenumerable {}
           //interface @new<>{}
           //interface @struct:  { type gettype() @string tostring() @int gethashcode() @bool equals(@object)    @int TOint(); @double TOdouble(); datetime TOdatetime(); guid TOguid(); pixel TOpixel() } 
           //interface @enum :@struct { databit TOdatabit()}
           //interface @class {  }
           //struct databit: @enum { binary TObinary();  }
           //struct flag : @enum {_},kind{_,a,b},mode{_,a,b},mark{_,a,b}
           //struct bool : @struct {} AStransferable { databit TOdatabit()}
           //struct char : @struct {}AStransferable {Object TOobject(Type); }
           //struct byte : @struct {} AStransferable { Int32 TOint32()}
           //struct double : @struct { Single TOsingle(); Decimal TOdecimal()}
           //blank<exp>: @struct where exp:@enum,new()   { binarysafe, exp?} ASblank where exp:struct, AStransferable { Nullable<exp>   }
           //empty<exp>: @struct where exp:@struct,new() { binarysafe, exp?}
           //organ<exp>: ASorgan {}
           //failure   
           //local 
           //system : external{dbexception->sql,ora}, argument{nullrefence, outofrange} 
           //<outofrange>used for params[..] idx is less(int) or than(int) equals true
           //<nullreference> for params[.].value<ogn||fgr> is that(organ||flagr) equals true
         */

        //GetReportResultSet
        public static bool GenerateReportResultSet(out Rtsql ret, sqluniqueidentifier uid)
        {
            //ArgumentOutOfRangeException
            //ArgumentNullException
            bool flagmethod = false;
            ret = default(Rtsql);
            #region statement body
            /*
             var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@rolecategoryID", roletypeId))
          {
          commandString = "select top 1 uid from rolePermission where rolecategoryID = @rolecategoryID"
          };


          if (FirstOrDefaultRowAndColumn(out object value, reqInfo))
          {
              string valueString = value?.ToString();
              result = Guid.TryParse(valueString, out uid);               

          }
             */
            #endregion
            //sp_report_resultset
            var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@Uid", uid))
            {
                commandtype = System.Data.CommandType.StoredProcedure,
                commandString = "sp_report_resultset",
                genericStringFailure = "Error at try getting the report result in this project"
            };



            //try
            //{
            if (FirstOrDefaultRowAndColumn(out object value, reqInfo))
            {

                string valueString = value?.ToString();
                JsonString jsonString = new JsonString(valueString);
                ret = jsonString.ASRtsql();
                //result = Guid.TryParse(valueString, out uid);
                flagmethod = true;
            }
            //}
            //catch (expfailure fail)
            //{

            //    throw;
            //}



            return flagmethod;

        }
        public static bool GenerateJsonTextReport(out Rtsql ret, sqluniqueidentifier uid) {
            bool flagmethod = false;
            ret = default(Rtsql);
            #region statement body
            /*
             var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@rolecategoryID", roletypeId))
          {
          commandString = "select top 1 uid from rolePermission where rolecategoryID = @rolecategoryID"
          };


          if (FirstOrDefaultRowAndColumn(out object value, reqInfo))
          {
              string valueString = value?.ToString();
              result = Guid.TryParse(valueString, out uid);               

          }
             */
            #endregion
            //sp_report_resultset
            var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@Uid", uid))
            {
                commandtype = System.Data.CommandType.StoredProcedure,
                commandString = "sp_report_resultset"
            };

            try
            {

            }
            catch (expfailure fail)
            {

                throw;
            }

            if (FirstOrDefaultRowAndColumn(out object value, reqInfo))
            {
                try
                {
                    string valueString = value?.ToString();
                    JsonString jsonString = new JsonString(valueString);
                    ret = jsonString.ASRtsql();
                    //result = Guid.TryParse(valueString, out uid);
                    flagmethod = true;
                }
                catch (Exception ex)
                {

                    //throw;
                }
              
                
            }

            return flagmethod;

        }

        public static bool GenerateJsonTextReportOperateFor(out Rtsql ret, sqluniqueidentifier uid)
        {
            bool flagmethod = false;
            ret = default(Rtsql);
            #region statement body
            /*
             var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@rolecategoryID", roletypeId))
          {
          commandString = "select top 1 uid from rolePermission where rolecategoryID = @rolecategoryID"
          };


          if (FirstOrDefaultRowAndColumn(out object value, reqInfo))
          {
              string valueString = value?.ToString();
              result = Guid.TryParse(valueString, out uid);               

          }
             */
            #endregion
            //sp_report_resultset
            var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@Uid", uid))
            {
                commandtype = System.Data.CommandType.StoredProcedure,
                commandString = "sp_report_resultset"
            };

            if (FirstOrDefaultRowAndColumn(out object value, reqInfo))
            {
                try
                {
                    string valueString = value?.ToString();
                    JsonString jsonString = new JsonString(valueString);
                    ret = jsonString.ASRtsql();
                    //result = Guid.TryParse(valueString, out uid);
                    flagmethod = true;
                }
                catch (Exception ex)
                {

                    //throw;
                }

            }

            return flagmethod;

        }

    }

    public static class ReportTools<ext> { }
    public static class ReportHelpers { }
    public static class ReportHelpers<ext> { }
    public static class ReportServices {

        public static reportresult GetReportResultSet(Guid primaryKey)
        {
            reportresult res;
            try
            {
                if (ReportTools.GenerateReportResultSet(out Rtsql value, primaryKey))
                {
                    res = new reportresult(obj:value);

                }
                else 
                {
                    res = new reportresult();
                }
        }
            catch (expfailure fail)
            {

                res = new reportresult(exc:fail);
            }


            return res;
        }
       public static bool GetReportResultSetHandle(out Rtsql model, Guid primaryKey)
        {
            bool result = false;
            model = default(Rtsql);

            var res = GetReportResultSet(primaryKey: primaryKey);
            result = res.HasOkay;

            if (res.HasOkay) {
                model = res.GetObject().ensure<Rtsql>();
            }
            //result = GetProjectRoleType(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (res.HasFail)
            {
                var exc = res.GetFailure();
                HandlerFailure(exc);
                //var errorMessage = "Error at try getting the report result in this project";
                //var builder = new System.Text.StringBuilder();

                //HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }

    }
    public static class ReportServices<ext>   //used for ext-> extensions of exp<var> where var:class
    {

        #region body statement

        public static bool GetProjectRoleType(out ProjectRoleType model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(ProjectRoleType);
            model = defaultValue;
            ProjectRoleType modelResult = defaultValue;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@RoleCategoryId", primaryKey))
            {
                commandString = @"
                    SELECT 
                    UId,
                    Name,
                    Description,
                    IsDirectiveLeader,
                    IsAssistantLeader,
                    IsDirectiveManager,
                    IsVisorCompany,
                    IsWorkAdministrator,
                    IsWorkMember,
                    IsTaskOfficer,
                    IsInvestigationOfficer,
                    IsViewerOnly
                    FROM RoleCategory rc
                     where rc.uid = @RoleCategoryId
                    "
            };


            result = FetchDataFirstOrDefaultBetter(reqInfo, (reader => {

                modelResult = new ProjectRoleType();
                Guid.TryParse(reader["uid"]?.ToString(), out Guid uId);
                modelResult.uID = uId;

                var name = reader["name"]?.ToString();
                modelResult.name = name;
                var description = reader["description"]?.ToString();
                modelResult.description = description;

                bool.TryParse(reader["IsDirectiveManager"].ToString(), out bool isDM);
                modelResult.isDM = isDM;

                bool.TryParse(reader["IsInvestigationOfficer"].ToString(), out bool isIO);
                modelResult.isIO = isIO;

                bool.TryParse(reader["IsDirectiveLeader"].ToString(), out bool isDL);
                modelResult.isDL = isDL;

                bool.TryParse(reader["IsAssistantLeader"].ToString(), out bool isAL);
                modelResult.isAL = isAL;




            }), out exceptionInfo);

            if (result)
                model = modelResult;

            return result;


        }
        public static bool GetProjectRoleTypeHandle(out ProjectRoleType model, Guid primaryKey)
        {
            bool result;

            result = GetProjectRoleType(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the role type in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }

        #endregion



    }
}

namespace programing {
    using binarysafe =  Nullable<binary>;
    using safebinary = Nullable<binary>;
    using injObjetive = ASobjetive;
    using safebool = Nullable<bool>;
    //IConvertible
    //TypeCode GetTypeCode();
    //object ToType(Type,IFormatProvider)
    // library
    // reference
    // recourse like .netmodule || .vsc||.vsn -module
    // engine
    // .eng        like 
    // .inf, .sys, 
    // .dll, .yar
    // lib,   tool -> svc   desk -> cli.exe, site -> app.web web.com,web.net  
    // .dll [] {}, .exe []{}, .svc [] {} 
    // .com [] {}, .net []{}, .edu [] {},     commerce, network, education
    //engagetime
    //factoringtime 
    //generictime
    //programingtime     --> this is like 
    //radingtime
    //machinetime
    //assemblytime
    //compiletime
    //static async Task closuretime
    //static async Task designtime
    //static async Task buildtime
    //static async Task runtime
    //static async Task dinamyctime
    //all this times are the pattern for  computingtime,  studiotime -> ,  softwaretime -> closuretime
    //bin  used for exp:

    public partial class mainpoint {

        public virtual void engage<exp>(params aimed [] aims ) {
        
        }

        public virtual void engagetime<exp>()
        {
            object obj = 1;
            int? res = (obj is int val) ? val : default(int?);
            //res.HasValue
        }
        public virtual void generictime<bin,aim>() 
            where bin:struct, Enum
            where aim:struct, injObjetive, @ref<bin>
        {
           // System.compari
        }
    }

    #region my own notes
    #region javascript
    /*
     * about oop
     *using facetAmbient = injAmbient: ASambient
     *using newfunction = ambient.function
          //case in array
          //ambient<?>:facetAmbient {delegate object:work function(object:demmand)
          //order||command|| ->operation
          //rutine {}
          //hook { event newfunction react = $donothing; raise<>()  }
          //method {firm,rutine}
          //field,choice,property,rutine,indice,method{activity<[verb:]Task, [verb:] Task||ValueTask<?>>, command }
          //var hook   ;//used for rutine
          //var verb   ;//used for activity: method async Task,ValueTask
          //var mold   ;//used for property with class that only have members like json,flag
          //var code   ;//used for field
          //var flag   ;//used for choice  5-> 1,4; 14-> 2,4,8; "c,z"->
          //var work   ;//used for command:method void call(<aim>?||?, params object[]), void exec(out object,?||,?,params object[] )
          //var blop   ;//used for 
          //var json   ;//used for property               //json only can have field,property,indice:property 
          //string key, string txt 
          //int one ,int two,
          //...array<string> args => 
          //xlist = args[one] ; ylist= args[two];
          //blop.items = xlist; xlist.push(key);
          //when this blop.items.size is 1 and contains key
          //blop.items.push(txt); //when this xlist.size is 2 and contains txt
          //->@phd here is demostrate that about reference persistence
     */
    #endregion
    #endregion

    [Flags]public enum binary : sbyte {

        @default = default(sbyte),
        @assembly,
        @solution, 
        @project=4,
        @navigator = 16,
        @hub   //like localhost or localhost/microsoft -> edb/iis/[1]www.delear.com or localhost/eblue-> edb/iis/[2]www.eblue.edu
       ,
        @suite //like localhost or localhost/dealer -> edb/iis/[1]www.delear.com or localhost/eblue-> edb/iis/[2]www.eblue.edu
       ,
        @explorer = 32,
        @volumen = 64,
        ////@folder = 128,
        ////@subfolder = 256,
        @modifiers = sbyte.MinValue,
    }

    public interface ASobjetive {   }
    public interface ASobjetive<exp> { }

    public partial struct aimed : @ref<binary>, ASobjetive {
        // private safebool 
        private readonly safebinary data;
        public safebinary @value { get => data; }
        public aimed(binary bin )
        {
            
            data = bin;
        }


        public static implicit operator aimed(binary from) { return new aimed(from); }
    }

    public delegate activity function(demmand @value = default(demmand));
    /// <summary>
    /// in this case only you can   add or remove  handlers if you can be permitted
    /// </summary>
    public interface reaction { event function dispatch;   }
    public class activity { }
    public class demmand { }
    public class indice { }
    public class rutine { public event function dispatch;
        public rutine()
        {
            //dispatch()
        }
    }
    public class method {

        public method()
        {
            //reaction rct; rct.dispatch
        }
    }

    public partial class command {
      
        public void exec<exp>(out exp ret, aimed ide) { ret = default(exp); }
        public void exec<exp,wkn>(out exp ret, aimed ide, params wkn[] args) { ret = default(exp); }

        public void call<exp>(exp val, aimed ide) { }
        public void call<exp, wkn>(exp val, aimed ide, params wkn[] args) { }
    }

    public interface @ref<exp> where exp:  struct { }
    internal interface @const<exp> where exp: IConvertible {  }

    internal interface @new { }
    public interface @new<exp> { }
    public interface @params<exp> { }
    public interface @out<exp> { }
    public interface @in<exp> { }
    public interface @as<exp> { }
    public interface @is<exp> { }
    public interface @is<exp, wkn> { }
    public interface @struct { }
    public interface @enum { }
    public interface @object { }

    public partial struct datavalue  { private binary feed; public binary display { get => feed; set => feed = value; }

        public datavalue(binary @value)
        {
            feed = @value;
        }

        public static implicit operator datavalue(binary from) {
            return new datavalue(from);
        }
    }
    public partial struct binarypair { public binarysafe thiskey; public binarysafe thisvalue; }
    public partial struct bitwise
    {
        private datavalue data;
        public datavalue getvalue { get => data;  }
        public datavalue setvalue { set => data = value; }
        public bitwise(datavalue @value)
        {
            data = @value;
        }

        public void negate() { data.display = ~data.display; }
        public void absolute(binary @value) {
            var minvalue = binary.modifiers;
            var maxvalue = ~minvalue;
            var xabs = value | minvalue;
            var yabs = data.display | minvalue;
            var zabs = (xabs | yabs);
            var demo = zabs & maxvalue;
            data.display = demo;
        }
        public void contrast(binary @value) { absolute(@value); data.display = ~data.display;  }
        
        public void inverse(binary @value) { data.display = (binary.modifiers| (data.display) | @value); }
        public void reverse(binary @value) { data.display = (binary.modifiers | (data.display) ^ @value); }
        public void preverse(binary @value) { data.display = (binary.modifiers | (data.display) & @value); }

        public void combine(binary @value) { data.display |= @value; }
        public void include(binary @value) { data.display ^= @value; }
        public void exclude(binary @value) { data.display &= @value; }
        public void supress(binary @value) { data.display &= ~@value; }
    }

    #region enum definitions
    public partial struct flag : @enum {
        private  binarypair feed; 
        public binarypair display { get => feed; set => feed = value; }
        //@int    hashcodeFor(){}
        //@string tostringFor(){}
        //@bool   equalsFor(@object)
        //type    gettypeFor()
        //static @int|@byte ParseStruct( string ||bool ingoreCase
        //static @bool TryParseStruct(out @int||@byte, string ||bool ingoreCase
        //static @object ParseEnum(Type, string ||bool ingoreCase 
        //static exp ParseEnum<exp>( string ||bool ingoreCase 
        //static @bool TryParse<exp>(out exp, string ||bool ingoreCase 
        //static @bool   referenceequalsFor(@object,@object)
        //static 
        public flag(binarypair @value)
        {
            feed = value;
            //string.try
            //Enum.TryParse
            //int.parse
            //bool.TryParse()
         //Enum.Parse TryParse<binary>()
        }
    }
    public partial struct kind : @enum {
        private binarypair feed;
        public binarypair display { get => feed; set => feed = value; }
    }
    public partial struct mode : @enum {
        private binarypair feed;
        public binarypair display { get => feed; set => feed = value; }
    }
    #endregion

    public partial struct datetimeoffset : @struct {
        
    
    }
    public partial struct datetime : @struct { }
    public partial struct timespan : @struct { }

    public partial struct hierarchyid : @struct { }
    public partial struct guid : @struct {
        
    }

    public partial struct color : @struct { }
    public partial struct pixel : @struct { }

    public partial struct hierarchyid : @struct { }
    public partial struct guid : @struct { }

    public partial struct @double : @struct { }
    public partial struct @float : @struct { }
    public partial struct @char : @struct { }
    public partial struct @bool : @struct { }
    public partial struct blank<exp> : @struct where exp: struct { private exp? non;  }
    public partial struct empty<exp> : @struct where exp : struct { private exp? non; }

    public partial class assembler {
       // private const Guid uid = default(Guid);
        public assembler()
        {
           //string v = new string();
        }
    
    }
    /*
         *
           //binarysafe = Nullable<binary>
           //binary
           //@ref<exp> where exp:struct
           //flags<exp> where exp: struct, Enum{}
           //
           //ASformatprovider {object ConvertTo(Type)}
           //ASenumeration{}, AStransferable {} AStranslatable {} 
           //ASconvertible{ int ToInt32(provider)} ASequatable{}
           //AScomparable{} ASenumerable {}
           //interface @new<>{}
           //interface @struct:  { type gettype() @string tostring() @int gethashcode() @bool equals(@object)    @int TOint(); @double TOdouble(); datetime TOdatetime(); guid TOguid(); pixel TOpixel() } 
           //interface @enum :@struct { databit TOdatabit()}
           //interface @class {  }
           //struct databit: @enum { binary TObinary();  }
           //struct flag : @enum {_},kind{_,a,b},mode{_,a,b},mark{_,a,b}
           //struct bool : @struct {} AStransferable { databit TOdatabit()}
           //struct char : @struct {}AStransferable {Object TOobject(Type); }
           //struct byte : @struct {} AStransferable { Int32 TOint32()}
           //struct double : @struct { Single TOsingle(); Decimal TOdecimal()}
           //blank<exp>: @struct where exp:@enum,new()   { binarysafe, exp?} ASblank where exp:struct, AStransferable { Nullable<exp>   }
           //empty<exp>: @struct where exp:@struct,new() { binarysafe, exp?}
           //organ<exp>: ASorgan {}
           //failure   
           //local 
           //system : external{dbexception->sql,ora}, argument{nullrefence, outofrange} 
           //<outofrange>used for params[..] idx is less(int) or than(int) equals true
           //<nullreference> for params[.].value<ogn||fgr> is that(organ||flagr) equals true
         */

}
namespace core
{
    using System.Data.Common;
    using System.Data.SqlClient;
    using msdbexception = System.Data.Common.DbException;
    using mssqlexception = System.Data.SqlClient.SqlException;
    using msexception = System.Exception;
    using varfailure = failure;
    using varstring = System.String;
    using varsqlfailure = sqlfailure;
    //using varexception = 

    #region body syntax, syntac, expressive
    //TODO create tag for  abstract class, for interface
    //inj -> injection
    //dbe -> Idbcontext
    //sql -> 
    //ora ->
    
    //exc -> exception, yes can use new<exc>()
    //var -> nop can use new<var> or yes can use new<var<?>>
    using databool = System.Boolean;
    using dataint = System.Int32;
    using databyte = System.Byte;
    using datasbyte = System.SByte;
    using databinary = System.SByte;
    using datachar = System.Char;
    using datalong = System.Int64;
    using datafloat = System.Single;
    using datadouble = System.Double;
    using datashort = System.Int16;
    using datadecimal = System.Decimal;
    using varobject = System.Object;
    using vartype = System.Type;
    using varmemberinfo = System.Reflection.MemberInfo; //to [] {eventinfo, methodbase, fieldinfo,propertyinfo, type}
    using vartypedelegator = System.Reflection.TypeDelegator;    //from memberinfo .> type .> typeinfo               //System.ComponentModel.TypeDescriptor
    using varwork = System.Threading.Tasks.Task;
    using varworkorgan = System.Threading.Tasks.Task<object>;
    using varworkcodec = System.Threading.Tasks.Task<System.ValueType>;
    using expwork = System.Threading.Tasks.Task<mdl>;
    using varset = System.Collections.Generic.Dictionary<@string, mdl>;
    using lbdworkexec = Func<mdl>;
    using lbdworkcall = Action<mdl>;
    using databit = binary;
    using numint = binary;
    using numbyte = binary;
    using enumeration = binary;
    using varspecification = spc;
    using varstatement = smt;
    using vargeneric = gnc;
    using facetstatement = statement;
    using facetgeneric = generic;
    using vardataresult = dat;
    using varengagement = egt;
    using facetdataresult = dataresult;
    using facetprocessing = ASproccesing;
    using facetraising = ASraising;
    using dataprocesing = processing;
    using dataraising = raising;
    using safebool = System.Nullable<bool>;
    using safedecimal = System.Nullable<decimal>;
    using expultimate = ultimate;
    public interface ASwork { }

    public class works : expwork
    { /**/
        public works(lbdworkexec exec) : base(function: exec)
        {

        }
    }
    //initializer, contructor,
    public class callworks { /*void doit()*/ }
    public class setworks { /*void doit(params object[] args)*/ }
    public class getworks { /*object returnit()*/}
    public class execworks { /*object returnit(params object[] args)*/}
    public class arguments //: varset
    { /*{mtd<ctl>, obj<tgt> ,pars<?>[]{<aritm>{a<cdc>,b<cdc>||..}<grapm>{b<mdl>,c<mdl>}}}*/
        //indicer<String>
        //target   {}  variantCALCULATOR<exp>.proceed, graphicCALCULATOR<exp>.  
        //react    {}  substract,click,press, 
        //structure {}  
        //     for math-> aritmethicStructure{a,b cdc},
        //     for verb-> platformStructure{a<sender>,b<eventargs> }
        public arguments() //: base(capacity: dataint.MaxValue)
        {

        }
    }
    //bitwise<left:databit>,arithmetical{left:decimal||int,rigth:}, reac<sender:butttonbox||textbox,e:clickargs||pressargs||itemargs||orderargs<{before,after,}update|edit||>>
    //
    public class arithmetic { }
    public class bitwise { }

    #region specification
    public interface spc { }
    public interface smt : varspecification { }
    public interface gnc : varspecification { }
    public interface egt : varspecification { }
    public interface syx : varspecification { }
    public interface syc : varspecification { }
    public interface gmc : varspecification { }
    public interface dat : varspecification { }
    public interface mdl : varspecification { }
    public interface ogn : varspecification { }
    public interface cdc : varspecification { }
    public interface exp<ll> : varspecification where ll : vardataresult { }
    public interface mdl<ll> : varspecification where ll : vardataresult { }
    public interface ogn<ll> : varspecification where ll : vardataresult { }
    public interface cdc<ll> : varspecification where ll : vardataresult { }
    public interface num<ll> : varspecification where ll : vardataresult { }
    //public interface exp<ll> : varspecification where ll : vardataresult { }
    #endregion
    #region statement 
    //interface st @params
    public interface statement : varstatement { }
    public interface resultset { exp @this<exp, key>(key idx) where key : struct where exp : struct; }
    public interface storeset { exp @this<exp, key>(key idx) where key : struct where exp : class; }
    public interface ff { } // used for member method with 
    //public interface ff { } // used for member method with 
    public interface feed { } //used for member field with cdc
    public interface variant { } //used for member field with ogn
    public interface display { }  //used for member property with cdc display 
    public interface target { } //used for member property with ogn 
    public interface order { /*function*/  } //used for member property with ogn -> cmd
    public interface operation { }  //used for member method with ogn -> 
    public interface service { varwork proceed(params object[] args); }  //used for member method with ogn  -> 
    public interface reaction { }  //used for member event with ogn -> 
    public interface record { }   //used for storeset { mdl<ogn> this []}
    public interface datalist { } //used for member<item> with resultset
    #endregion
    #region  generic
    //interface st @params
    //model, organ,  codec
    public interface generic : vargeneric { }
    public interface @params<ll> : facetgeneric { }  //used for mdl: ogn, cdc
    public interface @ref<ll> : facetgeneric { }  //used for cdc: struct||enum, new()
    public interface @new<ll> : facetgeneric { }  //used for class, struct, enum,  not used for interface, event
    public interface @readonly<ll> { }
    public interface @operator<ll> { }
    public interface @implicit<ll> { }
    public interface @explicit<ll> { }
    public interface @const<ll> { }     //used for struct,enum where implementAs<verbatim> class : not new()
    #endregion
    #region dataresult
    public interface dataresult : vardataresult { }
    #endregion
    #region engagement Todo find this for w3c 
    //interface @namespace, @using, @extern
    public interface engagement : varengagement { }
    public partial class engagementOrgan
    {
        public delegate works function(arguments @value = default(arguments));

    }
    public partial class operationOrgan
    {
        public works proceed(arguments @value)
        {
            return null;
        }
    }
    public partial class serviceOrgan
    {
        public async varwork proceed(params object[] orgs)
        {
            await varwork.Delay(1);

        }
        public async varworkorgan proceedFORorgan(params object[] orgs)
        {
            await varwork.Delay(1);
            return new object();
        }

        public async varworkcodec proceedFORcodec(params object[] orgs)
        {
            await varwork.Delay(1);
            return new decimal();
        }
    }
    #endregion
    #region about syntax 
    //interface @class,@event, @delegate, @interface, @struct, @enum 
    #endregion

    #region about syntac

    //feed<field<cdc>>, variant<field<obj>>, field, display<property<cdc>>, target<property<obj>>, property, method,
    //datalist<collection<cdc[]>>, record<collection<obj[]>>
    //work<method<obj:mtd>>, operation<method<obj:sve>>
    //task
    //task<@>
    //valuework,
    //valuework<@> where @: facetbinary-> ASbinary 
    //modelwork, modelwork<@> where @:class, facetbinary
    //arraywork<@> where @:class, facetbinary, facetrecord, new()
    //codecwork, codecwork<@> where @:struct
    //interface mdl, obj, rct, cmd,rfe, cc  @class,@event, @delegate, @interface, @struct, @enum 
    //mdl class
    //obj class, new
    //rcd class, new , params<mdl>
    //dlt class, new , params<cdc>
    //amb delegate  //definition
    //lmd delegate  //member<display>
    //sve async task||task<@>||valuetask<@> invoke
    //mtd delegate  //member<>      varActivity operate (varDemmand)
    //rct event     //definition
    //rfe interface //member<display> 
    //cdc enum      //member
    //num 
    //opt 
    //mmt
    //pin
    #endregion
    #region about gramatic
    //interface noun, common, adjetive, agent, verb
    //exp
    //pms 
    #endregion

    #region about cdc-> enum and struct 
    [Flags] public enum binary : databinary { 
        lol = default(databinary), c, l, z = 4, m = 8, y = 16, s = 32, b = 64,
        @failure = z|b ,@argumentfailure, @sqlfailure,
        j = databinary.MinValue,
        
    
    }
    public struct codec { }
    public struct param { } //used for struct, enum
    public struct field { } //used for struct, enum
    public struct flag { }  //used for enum:struct
    public struct choice<exp> where exp:struct, Enum { } //used for struct with enum  
    public struct @int { }
    public struct @byte { }
    public struct @string
    {

    }
    public class organ { } //used for class
    public class paramset { } //used to collect struct||enum this [@int||@long hash-line]
    public class paramdoc { } //used to collect struct||enum this [@string]
    public class organset { } //used to collect class,delegate,event,interface this  [@int||long hash-line ]
    public class organdoc { } //used to collect class,delegate,event,interface this  [@string name]

    public class varset { }
    public class display<exp> where exp:struct{ } //used for class with struct
    public class property { } //used for class

    public interface model { } //used for general -> class,struct,enum
    #endregion
    #endregion

    #region body types
    /// <summary>
    /// used for ? like ctl-> contextual, workspace<ctl> wks: communication with talk(er) and sink(er)
    /// </summary>
    public partial class ambient
    {

        #region body statement definitions
        /// <summary>
        /// user for var:class
        /// </summary>
        /// <typeparam name="varActivity"></typeparam>
        /// <typeparam name="varDemmand"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public delegate varActivity function<varActivity, varDemmand>(varDemmand @value) where varActivity : class where varDemmand : class;
        #endregion

    }
    //public partial class reaction { }

    //interface exp<@>
    //  interface mdl<@>
    //     interface obj<>
    //     interface cmd<> {property<of amb.function>}
    //     interface dph<> (property<of rtn>)
    //     interface amb<> {delegate function}
    //  interface cdc<@>
    //     interface ??<@>   //used for [1] char, bool, [2] datetime,guid [] color [] entry
    //     interface num<@>
    //     interface opt<@>
    //interface synx<>
    //interface sync<>

    //exp<cdc:struct>, exp<num:struct, new<cdc>>, exp<opt:enum, new<cdc>>
    //exp<rfe:interface>
    //exp<mdl:class>, exp<obj:class, new<mdl>>, exp<cmd:delegate>, exp<dph:event||dph:event,cmd:delegate>
    //exp<ext:class>, exp<svc:>, exp<amb>
    //exp<dmd:>, exp<par:>, exp<dim:class<num[]>||class<mdl[]>>, exp<arg:>, exp<var:>,
    //exp<aty:>,
    //exp<mtd:class(method)> exp<tsk:for task>, exp<tsk:for task<?:mdl>>, exp<tsk:for task<?:cdc>>

    //exp<gov:namespace>, exp<wks:namespace, using<gov>, exp<lib:namespace>m <exp:mle(module)>
    //numInt32
    //numint = binary;
    //numbyte = binary;
    //optflag = binary;
    //databit = binary;
    //enum binary
    //@byte {const numbyte  minvalue = corelib.@int<@>.minvalue; }
    //@short
    //@int  {const numint   minvalue = corelib.@byte<@>.minvalue; }
    //flag {}
    //entry {}
    //guide {}
    //identity {}
    //syntax -> noun, common<>, proper<>, adjetive, verb, agent<>, facet<>, act<>
    //syntactyc -> schema(family),  (name,lastname,nickname)(guide,genre,datetime) entity sup(person), sub(women,man)
    //expression ->
    //
    public partial class workspace { }
    public partial class workspaces { }

    //processing, communicating, memmoring, operating, calling, executing, functioning, messaging, remoting, controlling

    public class error<exp> : msexception { }

    //solution.project<web> {/*shared .>editors<> datamodel -> datacodec, dataorgan, datafacet    */}
    //datamodel
    //datacodec -> struct[]{bool, char, (s)byte, (u)short,int,long, float,double,decimal, ()uid,hid,    }, enum[]{}
    //dataorgan -> class[]{},event[]{},delegate[]{}
    //datafacet 
    public static class error<exp, ext> { }   //like Html.TextBoxFor
    public class order<cmd> { }

    public interface ASproccesing<exp> { }
    public interface AScodecprocessing<cdc> where cdc : struct { }
    public interface ASproccesing : AScodecprocessing<dataprocesing> { }

    public interface ASraising<exp> { }
    public interface ASorganraising<ogn> where ogn : class { }
    public interface ASraising : ASorganraising<dataraising> { }
    public struct processing : facetprocessing
    {
        private safebool feed; public safebool getvalue { get => feed; }
        public safebool setvalue { set => feed = value; }
    }

    public class raising : facetraising {
        private safebool feed; public safebool getvalue { get => feed; }
        public safebool setvalue { set => feed = value; }
    }

    public class agent<exp> {

    }

    public class interop {
        public delegate work function(argument @value);
    }
    public class argument {
       //demmand<opt>   {mode,kind}
       //message<arg>   {parameterset }   /used to know wich are the parameter<set> for the work 
    }
    public class demmand       //used for how the work it functionality
    {
        //codecpair<kindof,modeof> 
        //kind {get =>}
        //mode {get => }

        //kind [1] exec
        //kind [2] call
        //mode [1] less
        //mode [2] with
    }

    public struct @struct { public databinary line { get; set; } public databinary name { get; set; } }
    public struct @enum { public databinary line { get; set; } public databinary name { get; set; } }

    public class activity<exp> {

        public work operate(argument @value) { return null; }  //used for call (less),call(with); exec(less), call(with)
                                                               // public work operate(argument @value) { return null; }
    }

    public class service<exp>
    {

        //public varwork operate(argument @value) { return null; }
    }

    public struct codecpair<id,item> where id:struct where item:struct { public id key { get; set; } public item value { get; set; } }
    public class organpair<id, item> where item : class { public id key { get; set; } public item value { get; set; } }


    public struct kindof { public @enum display { get; set; }  }
    public struct modeof { public @enum display { get; set; } }



    public class soap { 
    
    
    }
    public class work {
        public dataprocesing hasproccesing { get; set; }
        // bool? calling        when null, true, false
        // bool? executing      when null, true, false
        //when call databit -> paramsless initialize<out bool? calling>(), construct
        //when call databit -> paramswith instance<out bool? calling>()

        public void call<exp>(databit less = default(databit)) { }  //used to determinate cual es la function<action to realize>
        public void call<exp,arg>( params arg[] with) { }
        public void exec<exp>(databit with = default(databit)) { }
        public void exec<exp, arg>(params arg[] args) { }
    }
    #endregion
    //public class sqlexception: mssqlexception { 


    //}
    public class ultimate { public varobject json { get; set; }
        public ultimate()
        {

        }
        public ultimate(varobject obj)
        {
            json = obj;
        }
    }
    public class modelresult<exc, ult>
        where exc:varfailure, new()
        where ult:expultimate, new()      //used for scalar report result
    {
        private exc varfailure;
        private ult varobject;            //this can contains  executeReader() for fetchresult 
        private safebool nonbool;

        public ult GetObject() => varobject ?? new ult();
        public exc GetFailure() => varfailure ?? new exc();

        public bool HasFlag { get => nonbool.HasValue; }
        public bool HasFail { get => nonbool.HasValue && !(nonbool.Value); }
        public bool HasOkay { get => nonbool.HasValue && (nonbool.Value); }
        public modelresult()  //when this case no ha fallado ni tampoco tiene resultado
        {

        }
        public modelresult(ult obj)
        {
            nonbool = true; varobject = obj;
        }
        public modelresult(exc exc)
        {
            nonbool = false; varfailure = exc;
        }

    }
    public class idearesult<exc, ult>
        where exc : class, new()
        where ult : class, new()      //used for scalar report result
    {
        private exc varfailure;
        private ult varobject;            //this can contains  executeReader() for fetchresult 
        private safebool nonbool;

        public ult GetObject() => varobject ?? new ult();
        public exc GetFailure() => varfailure ?? new exc();

        public bool HasFlag { get => nonbool.HasValue; }
        public bool HasFail { get => nonbool.HasValue && !(nonbool.Value); }
        public bool HasOkay { get => nonbool.HasValue && (nonbool.Value); }
        public idearesult()  //when this case no ha fallado ni tampoco tiene resultado
        {

        }
        public idearesult(ult obj)
        {
            nonbool = true; varobject = obj;
        }
        public idearesult(exc exc)
        {
            nonbool = false; varfailure = exc;
        }

    }

    public struct studfailure
    {
        //system, application
    }
    public struct kindfailure 
    {

        private readonly databit feed;
        public databit display { get => feed; }
        public kindfailure(databit @value)
        {
            feed = @value;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();  
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);    
        }

        public static bool operator ==(kindfailure x, databit y) => x.feed == y;
        public static bool operator !=(kindfailure x, databit y) => x.feed != y;
        // tsql
        // math
        //inj -> injection only with    method body 
        //var -> variable only with     method body   
        //par -> parameter only with    method body firm 
        //arg -> exp:new.member
        //arg:null.member
        //exp:null.member/
        //exp:new.member:null.
        //system       -> memory{outrange{insufficient}} argument{nullref, outrange}, external{db}
        //application  -> target {}
        //core     ->
        //local    ->
    }
    public struct modefailure
    {
     
    }
    public class failure : msexception
    {
        private const varstring empty = nameof(failure);
        private msexception json;
        private safebool flag;
        private kindfailure kind;
        private bool overtext;
        private string text;

        public string GetText() => overtext ? text: empty;
        public void SetText(string txt) {
            DefaultText(out overtext);
            text = txt;
        }
        public void DefaultText(out bool flg) => flg = true;
        public kindfailure getkind { get => kind; }
        public kindfailure setkind { set => kind = value; }
        public msexception based {
            get => json;
            set => ensureSet(out flag, item:value);
        }
        private void ensureSet(out bool? flg, msexception item) {
            flg = item == null;
            json = item;
        }

        public virtual void overrideFor(msexception dmd) { based = dmd; }
        public virtual void overrideFor(varfailure dmd) { based = dmd.json; }
        public virtual void overrideNew(varfailure dmd) { based = dmd; }

        public bool baseAS<exp>(out exp ret) where exp:msexception {
            bool flg = false;
                ret = json is exp var ? var : null;
            flg = ret == null;
            return flg;
        }

        public bool thisAS<exp>(out exp ret) where exp : varfailure
        {
            bool flg = false;
            ret = this is exp var ? var : null;
            flg = ret == null;
            return flg;
        }

        public failure() : base(message: empty)
        {
            json = this;
        }
        public failure(msexception dmd) : base(message:empty)
        {
            json = dmd; flag = true;
        }
        //public failure(bool? flg, msexception dmd) : base(message: empty)
        //{
        //    json = dmd;
        //}
        //private failure(bool? flg) : base(message: empty)
        //{
        //    flag = flg;
        //}

    }
    public class failure<exp> : varfailure
        where exp:msexception
    {
        //verbs halt:detener,stop}, warn:advertir,notice
        //okay, fail
        //okay, okay with mistaken(fix,fixif), okay with caution,
        //{float feed;  sumAdd  <exp:double>(float par, @ref<exp> arg  ) { feed = } {exp dat = arg.display; float num = (arg.display is float fvalue) ? fvalue : arg     }
        //{float  <exp>(float ) where exp: 
        //sqlfailure
        //error -> reason, reasonCollection
        //reason{ kind->{error(haltfail), fixure(), ca halt(), warning(), success(okay)  }}   call().ensureRequest(then:, okay:, fail:, wrong:)
        //okay=
        //fail= causas exclusivas por las cuales se debe detener(stop) 
        //wrong= causas exclusivas por las cuales se debe pausar(pause) para determinar accion posibles:infalibles
        //  ej. (int x) y se envia decimal entonces se realiza explicit operation x=(int)par(arg)
        //then= causas exclusivas por las cuales se debe pausar

    }
    public class dbfailure<dbe> : failure<dbe>
        where dbe : msdbexception
    {
        //mssqlexception
        //SqlError
    }

    public class argfailure<exc> : failure<exc>
        where exc : ArgumentException
    {
        //mssqlexception
        //SqlError
    }
    public class sqlfailure : dbfailure<mssqlexception>        
    {
        //mssqlexception
        //SqlError
    }
    public class argfailure : argfailure<ArgumentException>      
    {
        //mssqlexception
        //SqlError
    }
    public class sqlfailure<exp>: varsqlfailure   //dbfailure<mssqlexception>
        where exp:msdbexception, new()
    {
        //mssqlexception
        //SqlError
    }
    public class reason<exp> { 
     //SqlError
    }
    public class reason { }
}

namespace builtin {
    #region body statetement
    using uniqueidentifier = System.Guid;
    using sqllong = System.Int64;
    using sqlbool = System.Boolean;
    using sqlstring = System.String;   //used for nvarchar(max)
    using sqldouble = System.Double;
    using sqlfloat = System.Single;
    using sqlshort = System.Int16;
    using index = System.Collections.Generic.KeyValuePair<@int, @string>;
    using sqluniqueidentifier = System.Guid;
    using sqlchar = System.Char;     //used for nchar(1)
    using sqlint = System.Int32;
    using sqldecimal = System.Decimal;
    using sqlpointer = System.IntPtr;
    using sqldatatype = kind;

    #region arrange rad
    /*
     * 
     public partial class parameters<varchar, parameterjson, parameterargs>
        where varchar:class, new()
    {
        private const varchar keyempty = default(varchar);
        private varchar keystring;
        //private action<>
        internal parameterargs dictionary { get; set; }

        public parameterjson this[varchar idx]
        {
            get => ensureget(idx);
            set => ensureset(idx, value);
        }

        internal parameterjson ensureget(varchar idx)
        {
            return keystring.IndexOf(ensurekey(idx)) > -1 ? dictionary[idx] : dictionary[keyempty];
        }
        internal void ensureset(varchar key, parameterjson value)
        {
            bit found = (keystring.IndexOf(ensurekey(key)) > -1);
            if (found)
            {
                dictionary[key] = value;
            }

        }


        public varchar ensurekey(varchar arg) => $"({arg})";
        public parameters()
        {
            this.dictionary = new parameterargs();
            this.dictionary.Add(keyempty, default(parameterjson));

        }

        public void add(varchar key, parameterjson value)
        {
            key = key ?? uniqueidentifier.NewGuid().ToString();
            varchar idx = ensurekey(key);
            bit notfound = !(keystring.IndexOf(idx) > -1);

            if (notfound)
            {
                keystring = keystring.Insert(0, idx);
                dictionary.Add(key, value);
            }
            else
            {

            }
        }

        public void remove(varchar key)
        {
            key = key ?? uniqueidentifier.NewGuid().ToString();
            varchar idx = ensurekey(key);
            bit found = (keystring.IndexOf(idx) > -1);

            if (found)
            {
                dictionary.Remove(key);
                keystring = keystring.Replace(idx, keyempty);

            }
            else
            {

            }
        }

    }
     * 
     * */
    #endregion

    // /\a,b,d,h,p    ll    xx pq qp db bd ll ft fu
    // ()a,b,d,h,p    db   
    // <>a,b,d,h,p    
    // []a,b,d,h,p
    // {}a,b,d,h,p
    // 1245233444443
    public partial class array
    {  //[a1][b2](3)89
       //"(#)(<#>):($){@}(</#>);"
       //Dictionary<>
       //keyvaluepair {key,value}
       //entry {hash, next, key, value}
       //findentry(key)
       //keycollection,valuecollection
    }

    public partial class record
    {
        //ASvalue this [identiy idx] { get=> ensureget(arg:idx.key); set=> ensureset(arg:idx.key, exp:arg) }
        //indexer idxr 
        //indexer { pattern = "(#)(<#>):($){@}(</#>);.."   }
        //"(234)(<234>):(uname){'mainform'}(</234>);"
        //"(455)(<455>):(id){124}(</455>); (456)(<456>):(quantiy){824}(</456>);
        //entryset {   }
        //dictionary
        //valueitemset  {}
        //valueitem  {int line, value}
        //keyitemset    {}
        //keyitem  {int line, key}
        //int findkey(key)=> entries.getindexFor<int>(with: indr)
    }

    public partial class entryset
    {
        private ASentry[] feedsentry;
        public ASentry this[int index]
        {
            get
            {
                return feedsentry[0];
            }
            set
            {
                feedsentry[0] = value;
            }
        }
        public entryset()
        {
            feedsentry = new ASentry[1];
        }
    }

    public interface ASentry { }
    public interface ASdataentry : ASentry { }
    public interface ASmoldentry : ASentry { }

    public partial struct dataindex { private @int hash; private @int next; }
    public partial struct dataindex<key, value> { private @int hash; private @int next; key @get; value @set; }
    public partial struct dataentry { private @int hash; private @int next; }
    public partial struct dataentry<key, value> { private @int hash; private @int next; key @get; value @set; }
    public partial class moldentry { private @int hash; private @int next; }
    public partial struct moldentry<key, value> { private @int hash; private @int next; key @get; value @set; }

    [Flags]
    public enum kind : sqlshort
    {


        lol = default(sqlshort),
        ldefaultl = lol,
        c, l, z = l * 2,
        m = z * 2, y = m * 2, s = y * 2, b = s * 2, j = b * 2, g = j * 2, q = g * 2, p = q * 2, v = p * 2,
        lc = v * 2, ll = lc * 2, lz = ll * 2,
        lm = sqlshort.MinValue,
        len, l8000l, l1000l,
        size, max,
        sizemax,
        capacity, presision, scale,
        //capacity for nvarchar max is 8000
        //capacity for varbinary max is 8000
        //capacity for media and record max is 1000
        @nvarchar, //this is the family
        #region nvarchar stud(s)
        @stream, //this is built-in from nvarchar(max)
        @media, //this is built-in from nvarchar(1000)
        #endregion
        @varbinary, //this is the family
        #region @varbinary stud(s)
        @record, //this is built-in from varbinary(max)
        @list, //this is built-in from varbinary(1000)
        #endregion

        @nchar, @money, @real, @bigint, @datetime, @uniqueidentifier,


        // ldefaultl = lol 
        // lol
        //+  {@l0l:@l9l[10],A:Z[26], _ [1], a:z[26]} 10,52, 1, 63
        //+ -> 
        // @c, @l:@v @c(@1), @l(@2):@v(12) [1,2:11,12] -> 1, 9,10,11,12
        // lc, ll:lv lc(13), ll(14):lv(24) 13,21,22,23,24 lj(20)  lg(21), lq(22) lp(23), lv
        // zc, zl:zv zc(25), zl(26):zv(36) zb()zj()zg()zq(),zp()
        // mc, zl:zv zc(37), ml(38):zv(48) mb()zj()mg()mq(),mp()
        // yc, zl:zv zc(49), yl(50):zv(60) yb()yj()yg()yq(),yp()
        // sc, sl,sz sc(61), sl(62),sz(63)
        // sm  sm(64)
        // -
        // - -> family
        // - -> sqlstring, sqlint, sqllong, 

    }   //used like t-sql datatype 
    public struct identity { }
    public struct @int { }   //used like t-sql datavalue
    public struct @string { }

    public struct tag { }

    public class system { }
    public class systemset { }
    public class systemlist<set> { }


    public class errormodel : System.Exception { }
    #endregion

}


namespace native {

    using databit = System.Boolean;
    using datareal = System.Double;
    using datafloat = System.Single;
    using datamoney = System.Decimal;
    using databigint = System.Int64;
    using dataint = System.Int32;
    using datasmallint = System.Int16;
    using datatinyint = System.Byte;
    using datadatetime = System.DateTime;
    using datadatetimeoffset = System.DateTimeOffset;
    using datauniqueidentifier = System.Guid;
    using datatimestamp = System.TimeSpan;
    using datanvarchar = System.String;
    using datavarbinary = System.ArraySegment<byte>;
    using datanchar = System.Char;
    using datahierarchyid = System.Text.StringBuilder;
    using columnnodeclass = System.Collections.Generic.Dictionary<indice, column>;
    using flagers = System.FlagsAttribute;
    using enumblop = @enum;
    using structblop = @struct;
    using bitstruct = bit;
    using columnstruct = column;
    using indexeritem = indice;
    using datetime = datetimeref;
    using guid = guidref;
    using DateTime = datetimeref;
    using Guid = guidref;
    using static @enum;

    public interface ASdatasource { 
     Guid uid { get; }
    }
    public interface AScatalog {
     guid uid { get; }
    }
    public interface AStable { }

    public interface ASfunction { }
    public interface ASview { }
    public interface ASprocedure { }


    [flagers]public enum @enum: databigint { 
         @minvalue = databigint.MinValue,

         @unset = default(databigint) ,
        // p-c:v, h-c:v, d-c:v, b-c:v, a-c:v, [60] ()-c:z
        c1, c9, //wc[1], wl, wz, wg
         l2, l9, //wq[10], wp, wv[12], xc[13], xl, xz, xm, xy, xs[18],
         z3, z9, //xb[19], xj, xg, xq, xp, xv[24], yc, yl, yz[27] 
         m4, m9, //ym, yy, ys, yb, yj, yg, yq, yp, yv
         y5, y9, //zc, zg
         s6, s9, //zq, zp, zv, 1c:1s
         b7, b9, //1b, 1v, 2c, 2l, 2z

         @maxvalue = databigint.MaxValue
    }

    public struct bit {
        public readonly enumblop value;
        public bit(enumblop arg) => value = arg;
    }
    public struct keyword {
        public readonly bitstruct data;
        public keyword(enumblop arg) => data = new bitstruct(arg);
    }
    public struct choice {
          
    }

    public interface @int { }
    public interface @string { }
    public interface @double { }
    public interface @float { }
    public interface @object { }
    public interface @byte { }
    public interface @bool { }
    public interface @long { }
    public interface @char { }
    public interface @void { }

    public interface datetimeref { }
    public interface guidref { }

    public struct @struct { }
    public struct indice { 
          //guide, index
          
    }
    public struct address { }  //used like IntPtr -> void*, int*, char*
    //delegate*, 
    //public struct platt

    public struct datakind { public keyword kind { get; set; } }  //used for express 'money', 'real', 'numeric(9,2)'
    public struct datavalue { 
        public structblop feed { get; set; }    
    }
    public struct identifier { }


    public class storeinstance { } //datasource
    public class storecontext //database
    {
        //storeset stores

    }

    public class storeset    //list of all table
    {
        //dictionary of store

    }
    public class store {
        //dictionary of modelset

        //modelset   models
    }  //


    public struct column {
        public uniqueindexer thiskey { get; set; }
        //public indice thiskey { get; set; }
        public datakind thistype { get; set; }
        public identifier thisname { get; set; }
    }
    public class columnset {
        //TODO remember add string to use like  indexerOf
        private columnnodeclass basenode;

        public columnstruct this[indexeritem key]
        {  get => basenode[key];
            set => basenode[key] = value;
        }
    
    }

    public class row {
      //cellset cells
    }
    public class rowset { }

    public struct uniqueindexer { }

    public class cell {
        //private datavalue thisvalue;
        //public uniqueindexer thiskey { get; set; }
        //datavalue setvalue
        //datavalue getvalue
    }
    public class cellset {
       
    }
    public class model { 
    
      //columnset columns
      //rowset  rows
    }      //used for express table
    public class modelset 
    { 
       
    
    }

    //public class store { }      //used for 
    public class collection { }

    //public class datatype { }
   
}

namespace primitive {
    #region body statements
    using System.Threading.Tasks;
    using uniqueidentifier = System.Guid;
    using task = System.Threading.Tasks.Task;
    using bitwises = System.FlagsAttribute;
    using taskmodel = System.Threading.Tasks.Task<ASvalue>;
    using parameterargs = System.Collections.Generic.Dictionary<string, parameter>;
    using parameterjson = parameter;
    using varchar = System.String;
    using bit = System.Boolean;
    using bigint = System.Int64;
    using sqlvariant = System.Object;
    using facetvalue = ASvalue;
    using facetfeed = ASfeed;
    using facetmold = ASmold;
    using bitsexp = bits;
    using bitsnon = Nullable<bits>;
    using static tasker;

    [bitwises]
    public enum bits : bigint
    {
        @minvalue = bigint.MinValue,

        @bits = default(bigint),
        _, c,
        e = c * 2,
        z = c * 21,
        a1, b1, d1, h1, p1,
        ll,
        ll1, b2, d2, h2, p2,
        a,
        ll2, a2, d3, h3, p3,
        b,
        ll3, a3, b3, h4, p4,
        d,
        ll4, a4, b4, d4, p5,
        h,
        ll5, a5, b5, d5, h5,
        p,
        ll6, a6, b6, d6, h6, p6,

        @maxvalue = bigint.MaxValue
    }

    public struct bitcode
    {
        private bitsexp thisvalue;
        public bitsexp getvalue { get => thisvalue; }
        public bitsexp setvalue { set => thisvalue = value; }
        public bitcode(bitsexp value)
        {
            thisvalue = value;
        }
    }


    public struct address
    {
        private guide thisguide;
        private index thisindex;

        public address(params bitsexp[] args)
        {
            int len = args.Length;
            //guide x;
            //index y;
            //if (len > 0) {
            //    x = new guide() { thisdata = new bitcode( args[0]) }; 
            //}
            //if (len > 1) {
            //    y = new index() { thisdata = new bitcode(args[0]) };
            //}
            thisguide = len > 0 ? new guide() { thisdata = new bitcode(args[0]) } : default(guide);
            thisindex = len > 1 ? new index() { thisdata = new bitcode(args[1]) } : default(index);

        }

        public guide guideFor() => thisguide;
        public index indexFor() => thisindex;
    }

    public struct identifier
    {

    }
    public struct guide        //used for <exp> like hierarchyid
    {
        public bitcode thisdata { get; set; }
    }
    public struct index
    {
        public bitcode thisdata { get; set; }
    }

    public class model
    {

    }

    public class column
    {
        private guide thisiid;
    }
    public class columnset
    {
        private Dictionary<guide, column> thisnode;

        public column this[address key]
        {
            get => thisnode.getvalue<sqlvariant>(key);
            set => thisnode.setvalue<sqlvariant>(key, value);
        }

    }

    public static class columnsetdome
    {


        public static void setvalue<exp>(this Dictionary<guide, column> target, address key, column value)
        {
            guide iid = key.guideFor();
            target[iid] = value;


        }

        public static column getvalue<exp>(this Dictionary<guide, column> target, address key)
        {
            guide iid = key.guideFor();
            column value = target[iid];
            return value;

        }

    }



    public class cell { }
    public class cellset
    {

    }
    public class row
    {
        private index thisiid;

    }
    public class rowset
    {


    }



    public interface pairfeed { }
    public interface ASvalue
    {
        //facetfeed getfeed { get; }
        //facetfeed setfeed { set; }
        //ASmold getmold { get; }
        //ASmold setmold { get; }
        exp valueFor<exp>() where exp : facetvalue;
        bit valueForIf<exp, par>(out facetvalue ret, par arg, exp dat = default(exp)) where exp : facetvalue;
        //bit valueForIf<exp, par>(out exp ret, par arg = default(par))where exp: facetvalue;
        //facetfeed feed { get; set; }
        //facetmold mold { get; set; }
    }
    public interface ASfeed : facetvalue { }  //used for valuetype
    public interface ASmark { }  //used for value<
    //key
    public interface AScode { }  //used for value<struct, mark
    //long, int, short, byte,
    //decimal, double, float
    //datetime,guid,
    public interface ASflag { }  //used for value<enum, code, mark
    //numericstyles, memberkinds, 
    public interface ASsign { }  //used for value<enum, flag
    // datatype ->  sql_variant -> money, datetime, binary, real, bigint, nvarchar -> sqlstream
    // datatype -> sql_variant -> hierarchyid, uniqueidentifier 
    // nvarchar "<hid>||<uid>;->@value"
    // serialize(value: newjson) result in (sql_variant||binary||nvarchar) from  serializer (activity operate(demmand))
    // datavalue -> newmoney, newreal
    // datatype ->
    // parameter 
    // variable
    // column       { identifier name, datatype
    // row          { identifier cellset cells}
    // rowset       { row this [address  ]}
    // cell         { datavalue value  }
    // cellset      { cell this [address ]}
    // collection   { columnset columns }
    // model        {}         
    // store         add(insert:),remove(delete),edit(update:),search(select:) 
    // query         name, tsql for generate 
    public interface ASnoun : ASfeed { }  //used for value<enum, flag
    //ASaddress : ASnoun
    //struct address {  integer integerFor() }  //used to localize any member:item in node
    //ASidentifier : ASnoun, 
    //struct identifier :  { hierarchy    }    //used with enum<opt-ion>
    //struct index : { numericint  }           //used with struct<num-eric> values 
    //struct limit : {   }
    //AScolumn 
    //ASname
    //char, bool,

    //key{ (0):(9);[0]:[9], A:ZñÑ, }
    //10,20,40,80,61
    //30,50,60,70,90,
    //10,11,21,31,41,51
    //71,81,91,
    //20,12,22,32,42,52
    //62,72,82,92,
    //30,13
    //(1)
    //[1]
    //(a)
    //[a]
    //a,b,d,h,p
    //a,b,d,h
    //p,h,d,b
    // 12480
    // 1234567890
    // 1248
    // ab{}d{}h{}p
    // c,l,z,m,y,s,b,j,g,q
    // zl
    // mzl
    // mlz
    // ymlz, ymzl, yzml, yzlm, ylmz, ylzm
    // mzl, mlz, zml, zlm, lmz, lzm
    // d-n 1,2    lz
    // d-u 1,2    zl
    // x2,x3,x4
    // v:2, h:4, w:3,
    // l:p
    // l, c1,
    // {bc,dc,hc,pc}a, {ac,dl,hl,pl}b, {al,bl,hz,pz}d, {az,bz,dz,pm}h, {am,bm,dm,hm}p {ay,by,dy,hy,py}
    //30
    //63
    //33
    //d,
    // c:0, a:1, b:2,d:4,h:8,p:16
    // 6
    // c -> a:p-1
    // a -> c;b:p-2
    // b -> c,d:p-3
    // d ->  h:p-4
    // h ->  p-5
    // p ->  a:h-6
    // c-a:p-7
    // 5,c,5,a,5,b,5,d,5,h,5,p,6
    // 36, 6: 42
    // 63, 42, 21
    // 26, _, a:z, ()
    // 28
    // {[21]} {[42<30,6>, 6]}, ()= longminvalue
    // 77-63, 14
    // 
    // bits : long {
    // () = longminvalue,
    // @bits = default(long),
    // _ , c,
    // e, f, g,
    // i, j, k, l, m, n, o,
    // q, r, s, t, u, v, w, x, y, z,
    // ~(),a1:p1 [5]
    // (),
    // ~a,()1, b2:p2  [5]
    // a,
    // ~b, ()2:a2, d3:p3 
    // b,
    // ~c, ()2-a2-b2, h3:p3 
    // d,
    // ~h, ()2-a2-b2, h3:p3
    // h,
    // p,
    // ()7; a7:p7
    // }


    public interface ASmold : facetvalue { }  //used for referencetype
    public interface ASjson { }  //used for reference<class,object
    public interface ASsoap { }  //used for reference<event,json

    public delegate activity function(demmand value);

    public class json { }
    public class verb
    {
        public activity operate(demmand dmd = default(demmand))
        {
            return null;
        }

    }
    public class soap { public event function react; }
    public class ject { public function invoke { get; set; } }
    public class node
    {
        private facetvalue defaultitem = default(facetvalue);
        public facetvalue this[varchar key] { get => null; set => defaultitem = value; }
    }

    public struct mark { }
    public struct code { }
    public struct flag { }
    public struct sign { }

    public partial class tasker
    {
        public static readonly Action donothing = delegate { /*TODO log.writeToLog('do nothing')*/ };
        //public  async taskmodel operate(demmand value)
        //{
        //    await task.Delay(1);
        //    return default(facetvalue);
        //}
        public activity operate(demmand value)
        {
            return new activity();
            //await task.Delay(1);
            //return default(facetvalue);
        }
        public tasker()
        {
            function fnc = new function(operate);
        }
    }
    public partial class taskresult : taskmodel
    {

        public taskresult() : base(function: () => default(facetvalue))    //base(action: tasker.donothing)
        {
            //base.
        }

    }
    public partial class demmand : taskresult
    {


    }

    public partial class parameter { }
    public partial class parameters
    {
        private const varchar keyempty = "";
        private varchar keystring;
        //private action<>
        internal parameterargs dictionary { get; set; }

        public parameterjson this[varchar idx]
        {
            get => ensureget(idx);
            set => ensureset(idx, value);
        }

        internal parameterjson ensureget(varchar idx)
        {
            return keystring.IndexOf(ensurekey(idx)) > -1 ? dictionary[idx] : dictionary[keyempty];
        }
        internal void ensureset(varchar key, parameterjson value)
        {
            bit found = (keystring.IndexOf(ensurekey(key)) > -1);
            if (found)
            {
                dictionary[key] = value;
            }

        }


        public varchar ensurekey(varchar arg) => $"({arg})";
        public parameters()
        {
            this.dictionary = new parameterargs();
            this.dictionary.Add(keyempty, default(parameterjson));

        }

        public void add(varchar key, parameterjson value)
        {
            key = key ?? uniqueidentifier.NewGuid().ToString();
            varchar idx = ensurekey(key);
            bit notfound = !(keystring.IndexOf(idx) > -1);

            if (notfound)
            {
                keystring = keystring.Insert(0, idx);
                dictionary.Add(key, value);
            }
            else
            {

            }
        }

        public void remove(varchar key)
        {
            key = key ?? uniqueidentifier.NewGuid().ToString();
            varchar idx = ensurekey(key);
            bit found = (keystring.IndexOf(idx) > -1);

            if (found)
            {
                dictionary.Remove(key);
                keystring = keystring.Replace(idx, keyempty);

            }
            else
            {

            }
        }

    }


    public partial class t
    {
        //object     sender - target
        //eventargs  e      - parameters
        //event
        //arguments {parameters, caller(from method-statement), sender, event }
    }

    public partial class activity : taskresult { }

    public partial class call : activity { }
    public partial class docall : activity { }
    public partial class exec : activity { }
    public partial class returnexec : activity { }

    public partial class checkconstraint
    {
        public Predicate<facetvalue> predicate { get; set; }
    }

    public interface ASidentifier { }



    public partial class pairvalue
    {

        public facetvalue left { get; set; }
        public facetvalue rigth { get; set; }
    }

    public partial class checksconstraint
    {
        public Predicate<pairvalue> predicate { get; set; }
    }
    public partial class constraint     // like predicate -> predicate<exp> equals, 
    {
        private facetvalue thisdata;

        //public constraint()
        //{
        //    object var;
        //    int num;
        //    //object.ref
        //}
        //internal constraint()
        //{

        //}
        public virtual void @override(checksconstraint with)
        {
            var var = with?.predicate ?? constraintdome.checkvalues.predicate;

            constraintdome.checkvalues.predicate = var;

        }

        public bit equals(facetvalue value) => constraint.equals(thisdata, value);
        public static bit equals(facetvalue a, facetvalue b) => constraintdome.checkvalues.predicate(new pairvalue() { left = a, rigth = b });
    }

    public static partial class constraintdome
    {
        public static checksconstraint checkvalues { get; set; }
        static constraintdome()
        {
            checkvalues = new checksconstraint() { predicate = (pair) => sqlvariant.ReferenceEquals(pair.left, pair.rigth) };
        }

    }

    public class extensions
    {

    }

    public partial class plane { }   //used like for viewresult : viewaction, viewmold -> viewstep


    public partial class target<dmd>
    {


    }

    public partial class target { }
    public partial class argument { }

    public partial class argument<dmd>
    {


    }

    //public partial class broad



    /*
     * 
     choice   {}  //used for struct flag{bit}flag<exp> implicit flag<bit>
field    {}  //used for struct code{bit,bit}code<exp>{code,exp} implicit code<double>
property {}  //used for class  json 
indice   {}  //used for class  node
broad    {}  //used for class  soap                    event method.function reactit }
method   {}  //used for class  ject                    delegate activity function(demmand dmd = default(demmand) );  public statement body {get;set;}  }

statement{}  //used for class  verb        // public async activity operate(demmand dmd = default(demmand));  } 
     *
      mold
          json    --> used for class
             {}
          verb    --> used for class with method-member
          ject    --> used for class with 
      mark
          code    --> used for struct
          sign
          flag
     * */


    #endregion


}