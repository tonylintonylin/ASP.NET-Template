using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ASP.NET.TEMPLATE
{
    // Filter items shown as dropdown options

    #region Interface

    public interface IFilter
    {
        List<SelectListItem> ThingAItems { get; }
        List<SelectListItem> ThingBItems { get; }
        List<SelectListItem> ThingCItems { get; }
        List<SelectListItem> ThingDItems { get; }
        List<SelectListItem> ThingEItems { get; }

        List<SelectListItem> PeopleItems { get; }
        List<SelectListItem> ErrorItems { get; }
    }
    #endregion

    public class Filter : IFilter
    {
        #region Filters

        public List<SelectListItem> ThingAItems
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "0", Text = "All Things A", Selected = true });
                list.Add(new SelectListItem { Value = "1", Text = "Recently Viewed" });
                list.Add(new SelectListItem { Value = "2", Text = "All My Things A" });

                return list;
            }
        }

        public List<SelectListItem> ThingBItems
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "0", Text = "All Things B", Selected = true });
                list.Add(new SelectListItem { Value = "1", Text = "Recently Viewed" });
                list.Add(new SelectListItem { Value = "2", Text = "All My Things B" });

                return list;
            }
        }

        public List<SelectListItem> ThingCItems
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "0", Text = "All Things C", Selected = true });
                list.Add(new SelectListItem { Value = "1", Text = "Recently Viewed" });
                list.Add(new SelectListItem { Value = "2", Text = "All My Things C" });

                return list;
            }
        }

        public List<SelectListItem> ThingDItems
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "0", Text = "All Things D", Selected = true });
                list.Add(new SelectListItem { Value = "1", Text = "Recently Viewed" });
                list.Add(new SelectListItem { Value = "2", Text = "All My Things D" });

                return list;
            }
        }

        public List<SelectListItem> ThingEItems
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "0", Text = "All Things E", Selected = true });
                list.Add(new SelectListItem { Value = "1", Text = "Recently Viewed" });
                list.Add(new SelectListItem { Value = "2", Text = "All My Things E" });

                return list;
            }
        }

        public List<SelectListItem> PeopleItems
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "0", Text = "All People", Selected = true });
                list.Add(new SelectListItem { Value = "1", Text = "In My Department" });
                list.Add(new SelectListItem { Value = "2", Text = "All Admins" });
                list.Add(new SelectListItem { Value = "3", Text = "All Users" });

                return list;
            }
        }

        public List<SelectListItem> ErrorItems
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "0", Text = "-- Select --", Selected = true });
                list.Add(new SelectListItem { Value = "5", Text = "Delete 5 errors" });
                list.Add(new SelectListItem { Value = "10", Text = "Delete 10 errors" });
                list.Add(new SelectListItem { Value = "25", Text = "Delete 25 errors" });
                list.Add(new SelectListItem { Value = "100", Text = "Delete 100 errors" });
                list.Add(new SelectListItem { Value = "All", Text = "Delete All errors" });

                return list;
            }
        }

        #endregion
    }
}
