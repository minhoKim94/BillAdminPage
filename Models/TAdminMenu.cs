
namespace BillAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    public partial class TAdminMenu
    {
        public short MenuNo { get; set; }
        public short MenuGroupNo { get; set; }
        public string MenuName { get; set; }
        public string MenuENName { get; set; }
        public string MenuLink { get; set; }
        public short MenuSort { get; set; }
        public string MenuDesc { get; set; }
        public string MenuENDesc { get; set; }
        public byte UseStateCode { get; set; }
        public string AdminID { get; set; }
        public System.DateTime RegDate { get; set; }
        public Nullable<System.DateTime> UpdDate { get; set; }

        
        public List<TAdminMenu> AdminMenuGrid;
        public List<SelectListItem> MenuGroupList;
    }
}
