using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Eblue.Code.Models
{

    [Serializable()]
    public class ProjectProccessInfo : Tuple<Tuple<int, Guid, int>, Tuple<  Guid, string>, Tuple<Guid, string>, Tuple< Guid, string, int>, Tuple<Guid, string, int>, Tuple<bool, bool, bool, bool, bool>, Tuple<bool, bool, bool, bool, bool>>
    {
        //public int RowNumber { get { return this.Item1; } }
        //public Guid UID { get { return this.Item2; } }
        public Tuple<int , Guid, int> Way { get { return this.Item1; } }

        public Tuple<Guid, string> Project { get { return this.Item2; } }
        public Tuple<Guid, string> CurrentProccess { get { return this.Item3; } }
        public Tuple<Guid, string, int> NextProccess { get { return this.Item4; } }
        public Tuple<Guid, string,int> AlwaysProccess { get { return this.Item5; } }

        //public Guid ProjectID { get { return this.Item2; } }
        //public string ProjectDescription { get { return this.Item4; } }
        //public Guid CurrentProcccessId { get { return this.Item2; } }
        //public string CurrentProcccessDescription { get { return this.Item4; } }
        //public Guid NextProcccessId { get { return this.Item2; } }
        //public string NextProcccessDescription { get { return this.Item4; } }

        //public Guid AlwaysProcccessId { get { return this.Item2; } }
        //public string AlwaysProcccessDescription { get { return this.Item4; } }
        public Tuple<bool, bool, bool, bool, bool> availabledChecks { get { return this.Item6; } }

        public Tuple<bool, bool, bool, bool, bool> enabledChecks { get { return this.Item7; } }

        public ProjectProccessInfo(Tuple<int, Guid, int> a, Tuple<Guid, string> b , Tuple<Guid, string> c, Tuple<Guid, string, int> d, Tuple<Guid, string, int> e, Tuple<bool, bool, bool, bool, bool> f, Tuple<bool, bool, bool, bool, bool> g)
            : base (a,b,c,d,e,f,g)
        { }


       
    }

    [Serializable()]
    public class ProjectProccessInfoSet : List<ProjectProccessInfo>, ISerializable
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

        public ProjectProccessInfoSet()
        {

        }

        public ProjectProccessInfoSet(SerializationInfo info, StreamingContext context)
        {

        }
    }


    [Serializable()]
    public class ProjectProcess: ISerializable
    {
        //TODO ProjectProcess step 1 - setting role type process
        #region not mapped one
        public bool isDM { get; set; }
        public bool isIO { get; set; }
        public bool isDL { get; set; }
        public bool isAL { get; set; }

        public bool isRDR { get; set; }

        public bool isEO { get; set; }  

        public bool isERS { get; set; }

        /// <summary>
        /// is not driver type like []{ <isVC, isWA>, <isWM, isTO>, isVO}
        /// </summary>
        public bool isND { get; set; }
        #endregion

        //TODO ProjectProcess step 2 - setting next and always process
        #region not mapped two
        public Guid? nextUId { get; set; }
        public Guid? alwaysUId { get; set; }

        #endregion


        //TODO ProjectProcess step 3 - setting <processBehavior-Blop> and <processProvider-Blop> process
        #region about question

        public bool isStarter { get; set; }
       
        public bool isCloser { get; set; }
       
        public bool objetionsAvailableds { get; set; }

        public bool assentsAvailableds { get; set; }

        #endregion


        //TODO ProjectProcess step 4 - setting <processDriver-Blop> process
        #region about used for
        /// <summary>
        /// for directive manager
        /// </summary>
        public bool usedForDM { get; set; }
        /// <summary>
        /// for investigation officer
        /// </summary>
        public bool usedForIO { get; set; }
        /// <summary>
        /// for directive leader
        /// </summary>
        public bool usedForDL { get; set; }

        /// <summary>
        /// for assistant leader
        /// </summary>
        /// 

      

        public bool usedForAL { get; set; }
        /// <summary>
        /// for only directive manager
        /// </summary>
        public bool onlyForDM { get; set; }
        #endregion

        /// <summary>
        /// for Research Director
        /// </summary>
        /// 
        public bool usedForRDR { get; set; }


        /// <summary>
        /// for Executive Officer
        /// </summary>
        /// 

        public bool usedForEO { get; set; }

        /// <summary>
        /// for External Resources
        /// </summary>
        /// 

        public bool usedForERS { get; set; }






        #region feeds
        private bool feed_usedForDMorOnlyDM => (usedForDM || onlyForDM);
        private bool feed_isDMandUsedForDMorOnlyDM => (isDM & feed_usedForDMorOnlyDM);
        private bool feed_isALorDMandUsedForDMorOnlyDM => (isAL || feed_isDMandUsedForDMorOnlyDM);
        private bool feed_isDriver => (isDM || isIO || isDL || isAL);
        private bool feed_showNextButton => (nextUId.HasValue && feed_isDriver);
        private bool feed_showAlwaysButton => (alwaysUId.HasValue && feed_isDriver);

        private bool feed_isNOTDriver => (isND  | !feed_isDriver);
        #endregion

        #region displays
        //:determination
        public bool displayObjetionsAvailableds
        {
            get => (feed_isALorDMandUsedForDMorOnlyDM & objetionsAvailableds);
        }

        public bool displayAssentsAvailableds
        {
            get => (feed_isALorDMandUsedForDMorOnlyDM & assentsAvailableds);
        }

        public bool displayShowNextButton
        {
            get => feed_showNextButton;
        }

        public bool displayShowAlwaysButton
        {
            get => feed_showAlwaysButton;
        }

        public bool displayIsNotDriver
        {
            get => feed_isNOTDriver;
        }
        #endregion


        public ProjectProcess()
        {

        }
        public ProjectProcess(SerializationInfo info, StreamingContext context)
        {

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //base.GetObjectData(info, context);
        }
    }


    [Serializable()]
    public class ProjectRoleType : ISerializable
    {

        #region not mapped 
        //setting from sql statement
        public bool isND { get; set; }
        #endregion

        #region db fields
        public Guid uID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
       
        public bool isDM { get; set; }
        public bool isIO { get; set; }
        public bool isDL { get; set; }
        public bool isAL { get; set; }

        public bool isVC { get; set;  }
        public bool isWA { get; set; }

        public bool isWM { get; set; }
        public bool isTO { get; set; }
        public bool isVO { get; set; }

        public bool isRDR { get; set; }

        public bool isEO { get; set; }

        public bool isERS { get; set; }

        #endregion


        public ProjectRoleType()
        {

        }
        public ProjectRoleType(SerializationInfo info, StreamingContext context)
        {

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //base.GetObjectData(info, context);
        }

    }

    public class ProjectStatus {
      public int RowNumber { get; set; }
      public short EstatusId { get; set; }
      public string Description { get; set; }
    }
}