using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eblue.Code
{
    public class UserLogged
    {
        
        public Guid UserId { get; set; }
        public Guid RosterId { get; set; }

        #region Old
        public bool CanBePI { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
        public bool IsSupervisor { get; set; }
        public bool IsPersonnel { get; set; }
        public bool IsStudent { get; set; }
        public bool IsCoordinator { get; set; }
        public bool IsScientist { get; set; }
        public bool IsDeveloper { get; set; }

        public bool IsPlanViewer { get; set; }
        #endregion

        #region New Roles
        public bool IsAdministrator { get; set; }
        
        public bool IsProjectOfficer { get; set; }

        public bool IsBudgetOfficer { get; set; }

        public bool IsHumanROfficer { get; set; }

        public bool IsAESOfficer { get; set; }

      

        #endregion

        public string UserFullDescription { get; set; }

        public string RosterName { get; set; }
        public string RosterType { get; set; }
        public string RosterEmail { get; set; }
        public string RosterRole { get; set; }

        public string RosterPicture { get; set; }
        public string RosterSignature { get; set; }


        public Guid? RosterTypeID { get; set; }
        public string RosterTypeName { get; set; }
        public string RosterTypeDescription { get; set; }


    }
}