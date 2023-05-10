 using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Eblue.Code
{

    [Serializable()]
    public class TargetSection
    {
        public Guid UId { get; set; }
        public Guid? TargetOf { get; set; }
        public Guid? WithTargetOf { get; set; }
        public bool Isroot { get; set; }

        public Guid? targetId { get; set; }
        public Guid? parentId { get; set; }

        public int rowNumber { get; set; }

        public int numLine { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string route { get; set; }
        public bool isroot { get; set; }
        public bool isagrupation { get; set; }
        
        public bool hastargetID { get {
                return targetId != null && targetId.Value != Guid.Empty;
            } }
        public bool notvisibleformenu { get; set; }
        
        public bool whenData { get; set; }
        public bool dataCapDetail { get; set; }
        public bool dataCapEdit { get; set; }

        //[NonSerialized]

        public bool hideSection
        {
            get { return (!dataCanDetail & !listCanDetail); }
        }
        public bool dataCanEdit 
        {
            get { return whenData & dataCapEdit; }
        }

        private bool feed_blockDataSection;
        private bool feed_blockListSection;

        public bool displayBlockDataSection {
            get => feed_blockDataSection;
            set => feed_blockDataSection = value;
        }
        public bool displayBlockListSection
        {
            get => feed_blockListSection;
            set => feed_blockListSection = value;
        }


        public bool blockDataSection
        {
            get => displayBlockDataSection || (dataCanDetail & (!dataCapEdit));
        
        }

        public bool blockListSection
        {
            get => displayBlockListSection || (listCanDetail & (!listCapEdit & !listCapAdd));

        }

        public bool dataCanDetail
        {
            get { return (whenData & (dataCapDetail) || dataCanEdit); }
        }

        public bool listCanEdit
        {
            get { return whenList & listCapEdit; }
        }

        public bool listCanAdd
        {
            get { return whenList & listCapAdd; }
        }

        public bool listCanRemove
        {
            get { return whenList & listCapRemove; }
        }

        public bool listCanDetail
        {
            get { return (whenList & (listCapDetail) || listCanEdit || listCanRemove|| listCanAdd); }
        }



        //whenData = section_a1.whenData;
        //dataCapEdit = section_a1.dataCapEdit && whenData;
        //dataCapDetail = section_a1.dataCapDetail && whenData;

        //canDataDetail = dataCapDetail || dataCapEdit;
        //canDataEdit = dataCapEdit;
        //canDataEditAndDetail = dataCapEdit && dataCapDetail;

        public bool whenList { get; set; }
        public bool listCapDetail { get; set; }
        public bool listCapAdd { get; set; }
        public bool listCapRemove { get; set; }
        public bool listCapEdit { get; set; }

        public int orderLine { get; set; }

        public override string ToString()
        {
            //char circle = '⃝';
            char circle = char.MinValue;
            var a = (whenData ? $"{circle}❶" : $"{circle}①");
            var b = (dataCanDetail ? $"{circle}❷" : $"{circle}②");
            var c = (dataCapEdit ? $"{circle}❸" : $"{circle}③");

            var d = (whenList ? $"{circle}❶" : $"{circle}①");
            var e = (listCapDetail ? $"{circle}❷" : $"{circle}②");
            var f = (listCapAdd ? $"{circle}❸" : $"{circle}③");
            var g = (listCapEdit ? $"{circle}❹" : $"{circle}④");
            var h = (listCapRemove ? $"{circle}❺" : $"{circle}⑤");

            //  ⃝①②③④⑤⑥⑦❶❷❸❹❺❻❼
            return $@"key: {UId}, name: {name}, description: {description}, route: {route}, isroot: {isroot}, z-caps: {{{a},{b},{c}}}, y-caps: {{{d},{e},{f},{g},{h}}}";
        }

        public T As<T>() where T:TargetSection, new()
        {
            var isType = this is T;
            T result = default(T);

            if (isType) result = this as T;
            return result;
        }
    }

    [Serializable()]
    public class TargetSectionSet: Dictionary<string,TargetSection>
    {

        public TargetSectionSet()
        { 
        
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        public override void OnDeserialization(object sender)
        {
            base.OnDeserialization(sender);
        }

        public TargetSectionSet(SerializationInfo info, StreamingContext context) 
        { 
            
        }

    }
}