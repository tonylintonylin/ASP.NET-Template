using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ASP.NET.TEMPLATE
{
    // Lookup (dropdown) items. 

    #region Interface

    public interface ILookup
    {
        List<SelectListItem> ThingALookups { get; }
        List<SelectListItem> ThingBLookups { get; }
        List<SelectListItem> ThingCLookups { get; }
        List<SelectListItem> ThingDLookups { get; }
        List<SelectListItem> ThingELookups { get; }

        List<SelectListItem> ThingAStatuses { get; }
        List<SelectListItem> ThingBStatuses { get; }
        List<SelectListItem> ThingCStatuses { get; }
        List<SelectListItem> ThingDStatuses { get; }
        List<SelectListItem> ThingEStatuses { get; }

        List<SelectListItem> UserRoles { get; }

    }
    #endregion

    public class Lookup : ILookup
    {
        #region Items

        public List<SelectListItem> ThingALookups
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- None --", Selected = true });
                list.Add(new SelectListItem { Value = "High", Text = "High" });
                list.Add(new SelectListItem { Value = "Medium", Text = "Medium" });
                list.Add(new SelectListItem { Value = "Low", Text = "Low" });

                return list;
            }
        }

        public List<SelectListItem> ThingBLookups
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- None --", Selected = true });
                list.Add(new SelectListItem { Value = "Primary", Text = "Primary" });
                list.Add(new SelectListItem { Value = "Secondary", Text = "Secondary" });
                list.Add(new SelectListItem { Value = "Tertiary", Text = "Tertiary" });

                return list;
            }
        }

        public List<SelectListItem> ThingCLookups
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- None --", Selected = true });
                list.Add(new SelectListItem { Value = "Private", Text = "Private" });
                list.Add(new SelectListItem { Value = "Public", Text = "Public" });
                list.Add(new SelectListItem { Value = "Protected", Text = "Protected" });

                return list;
            }
        }

        public List<SelectListItem> ThingDLookups
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- None --", Selected = true });
                list.Add(new SelectListItem { Value = "Email", Text = "Email" });
                list.Add(new SelectListItem { Value = "Website", Text = "Website" });
                list.Add(new SelectListItem { Value = "Meeting", Text = "Meeting" });
                list.Add(new SelectListItem { Value = "Exhibit", Text = "Exhibit" });
                list.Add(new SelectListItem { Value = "Referral", Text = "Referral" });

                return list;
            }
        }

        public List<SelectListItem> ThingELookups
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- None --", Selected = true });
                list.Add(new SelectListItem { Value = "Installation", Text = "Installation" });
                list.Add(new SelectListItem { Value = "Compatibility", Text = "Compatibility" });
                list.Add(new SelectListItem { Value = "Bug/Defect", Text = "Bug/Defect" });
                list.Add(new SelectListItem { Value = "Data Issue", Text = "Data Issue" });
                list.Add(new SelectListItem { Value = "Change Request", Text = "Change Request" });
                list.Add(new SelectListItem { Value = "Other", Text = "Other" });

                return list;
            }
        }

        public List<SelectListItem> ThingAStatuses
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- None --", Selected = true });
                list.Add(new SelectListItem { Value = "Not Started", Text = "Not Started" });
                list.Add(new SelectListItem { Value = "In Progress", Text = "In Progress" });
                list.Add(new SelectListItem { Value = "Submitted", Text = "Submitted" });
                list.Add(new SelectListItem { Value = "Approved", Text = "Approved" });
                list.Add(new SelectListItem { Value = "Declined", Text = "Declined" });

                return list;
            }
        }

        public List<SelectListItem> ThingBStatuses
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- None --", Selected = true });
                list.Add(new SelectListItem { Value = "Bronze", Text = "Bronze" });
                list.Add(new SelectListItem { Value = "Silver", Text = "Silver" });
                list.Add(new SelectListItem { Value = "Gold", Text = "Gold" });
                list.Add(new SelectListItem { Value = "Platinum", Text = "Platinum" });

                return list;
            }
        }

        public List<SelectListItem> ThingCStatuses
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- None --", Selected = true });
                list.Add(new SelectListItem { Value = "In Review", Text = "In Review" });
                list.Add(new SelectListItem { Value = "Pending", Text = "Pending" });
                list.Add(new SelectListItem { Value = "Complete", Text = "Complete" });

                return list;
            }
        }

        public List<SelectListItem> ThingDStatuses
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- None --", Selected = true });
                list.Add(new SelectListItem { Value = "Junior", Text = "Junior" });
                list.Add(new SelectListItem { Value = "Senior", Text = "Senior" });
                list.Add(new SelectListItem { Value = "Architect", Text = "Architect" });
                list.Add(new SelectListItem { Value = "Manager", Text = "Manager" });

                return list;
            }
        }

        public List<SelectListItem> ThingEStatuses
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- None --", Selected = true });
                list.Add(new SelectListItem { Value = "Sent", Text = "Sent" });
                list.Add(new SelectListItem { Value = "Received", Text = "Received" });
                list.Add(new SelectListItem { Value = "Responded", Text = "Responded" });

                return list;
            }
        }


        public List<SelectListItem> UserRoles
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- None --", Selected = true });
                list.Add(new SelectListItem { Value = "User", Text = "User" });
                list.Add(new SelectListItem { Value = "Admin", Text = "Admin" });

                return list;
            }
        }

        #endregion
    }
}
