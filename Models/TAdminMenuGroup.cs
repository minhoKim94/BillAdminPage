
namespace BillAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    public partial class TAdminMenuGroup
    {
        public short MenuGroupNo { get; set; }
        public string MenuGroupName { get; set; }
        public string MenuGroupENName { get; set; }
        public short MenuGroupSort { get; set; }
        public string MenuGroupIcon { get; set; }
        public string UseFlag { get; set; }
        public string AdminID { get; set; }
        public System.DateTime RegDate { get; set; }
        public Nullable<System.DateTime> UpdDate { get; set; }
        public object IsManager { get; internal set; }

        public List<SelectListItem> MenuGroupList;
        public List<TAdminMenu> AdminMenuGrid;


    }
}
