using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Eblue.Code.Models
{

    [Serializable()]
    public class RolePermission
    { 

       public Guid RoleCategoryID { get; set; }
       public Guid RoleSetPermissionID { get; set; }
        
       public string Desscription { get; set; }
       
       public RoleSetPermissionRoot Root { get; set; }
    }

    [Serializable()]
    public class RoleSetPermissionRoot
    {
        public Guid UId { get; set; }
        public Guid? WithTargetOf { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool HasSections { get; set; }
        public bool HasGrant { get; set; }
        public bool HasGrantNested { get; set; }
        public bool HasGrantRecursive { get; set; }

        public RoleSetSectionPermissionSet SectionSet { get; set; }
        public RoleSetPermissionGrant Grant { get; set; }
        public override string ToString()
        {
            //char circle = '⃝';   
            ////  ⃝①②③④❶❷❸❹
            //return $@"key:{UId}, name:{Name}, description:{Description}, 
            //caps:{{ {(HasSections ? $"{circle}❶" : $"{circle}①")}, 
            //        {(HasGrant ? $"{circle}❷" : $"{circle}②")},
            //        {(HasGrantNested ? $"{circle}❸" : $"{circle}③")},
            //        {(HasGrantRecursive ? $"{circle}❹" : $"{circle}❹")} }}";

            //char circle = '⃝';
            char circle = char.MinValue;
            var a = (HasSections ? $"{circle}❶" : $"{circle}①");
            var b = (HasGrant ? $"{circle}❷" : $"{circle}②");
            var c = (HasGrantNested ? $"{circle}❸" : $"{circle}③");
            var d = (HasGrantRecursive ? $"{circle}❹" : $"{circle}④");


            //  ⃝①②③④⑤⑥⑦❶❷❸❹❺❻❼
            return $@"key: {UId}, name: {Name}, description: {Description}, z-caps: {{{a},{b},{c},{d}}}";
        }
    }

    [Serializable()]
    public class RoleSetPermissionGrant: RoleSetPermissionRoot
    {       
        public RoleSetPermissionGrantSet GrantSet { get; set; }
        public override string ToString()
        {
            var result = base.ToString();
            return result;
        }
    }

    [Serializable()]
    public class RoleSetPermissionGrantSet: Dictionary<Guid, RoleSetPermissionGrant>
    //where x:Guid is Root||Grant
    {
        private System.Text.StringBuilder _sb;
        public string DisplayItems { get; set; }

        public override string ToString()
        {
            return DisplayItems;
        }

        public new void Add(Guid key, RoleSetPermissionGrant value) {

            _sb.AppendLine(value.ToString()) ;
            DisplayItems = _sb.ToString();
            base.Add(key, value);
        }

        public RoleSetPermissionGrantSet()
        {
            _sb = new System.Text.StringBuilder();
            DisplayItems = "";

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        public override void OnDeserialization(object sender)
        {
            base.OnDeserialization(sender);
        }

        public RoleSetPermissionGrantSet(SerializationInfo info, StreamingContext context)
        {
            _sb = new System.Text.StringBuilder();
            DisplayItems = "";

        }

    }


    [Serializable()]
    public class RoleSetSectionPermission:TargetSection
    {
        public override string ToString()
        {
            var result = base.ToString();
            return result;
        }
            //public Guid UId { get; set; }
            //public Guid? TargetOf { get; set; }
            //public Guid? WithTargetOf { get; set; }

            //public int rowNumber { get; set; }

            //public int numLine { get; set; }
            //public string name { get; set; }
            //public string description { get; set; }
            //public bool whenData { get; set; }
            //public bool dataCapDetail { get; set; }
            //public bool dataCapEdit { get; set; }

            //public bool hideSection
            //{
            //    get { return (!dataCanDetail & !listCanDetail); }
            //}
            //public bool dataCanEdit
            //{
            //    get { return whenData & dataCapEdit; }
            //}

            //public bool dataCanDetail
            //{
            //    get { return (whenData & (dataCapDetail) || dataCanEdit); }
            //}

            //public bool listCanEdit
            //{
            //    get { return whenList & listCapEdit; }
            //}

            //public bool listCanAdd
            //{
            //    get { return whenList & listCapAdd; }
            //}

            //public bool listCanRemove
            //{
            //    get { return whenList & listCapRemove; }
            //}

            //public bool listCanDetail
            //{
            //    get { return (whenList & (listCapDetail) || listCanEdit || listCanRemove || listCanAdd); }
            //}

            //public bool whenList { get; set; }
            //public bool listCapDetail { get; set; }
            //public bool listCapAdd { get; set; }
            //public bool listCapRemove { get; set; }
            //public bool listCapEdit { get; set; }

            //public int orderLine { get; set; }

        }

    [Serializable()]
    public class RoleSetSectionPermissionSet : Dictionary<Guid, RoleSetSectionPermission>
        //where x:Guid is Root||Grant
    {
        private System.Text.StringBuilder _sb;
        public string DisplayItems { get; set; }

        public override string ToString()
        {
            return DisplayItems;
        }

        public new void Add(Guid key, RoleSetSectionPermission value)
        {

            _sb.AppendLine(value.ToString());
            DisplayItems = _sb.ToString();
            base.Add(key, value);
        }

        public RoleSetSectionPermissionSet()
        {
            DisplayItems = "";
            _sb = new System.Text.StringBuilder();
        
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        public override void OnDeserialization(object sender)
        {
            base.OnDeserialization(sender);
        }

        public RoleSetSectionPermissionSet(SerializationInfo info, StreamingContext context)
        {
            DisplayItems = "";
            _sb = new System.Text.StringBuilder();

        }

    }

}

namespace Eblue.Code.Types
{

    public class objetive : objetiveType
    { 
    
    }

    public class parent : objetiveType
    { }

    public class agrupation: objetiveType
    { }

    public abstract class objetiveType : targetComponent { }
    public class objetiveComponent : objetiveMember { }

    public abstract class objetiveMember { }

    public class target : targetType { }

    public class root : targetType { }

    public class grant : targetType { }

    public class rollup: targetType { }

    public abstract class targetType: targetComponent { }
    public class targetComponent: targetMember { }

    public abstract class targetMember { }

    public interface Iobjetive { }
    public interface Itarget { }
    public interface Iroot { }
    public interface Igrant { }
    public interface Iagrupation { }

}