
namespace BillAdmin.Models
{
    using System;

    public partial class TAdminSessionHist
    {
        public int SeqNo { get; set; }
        public System.Guid SessionKey { get; set; }
        public string AdminID { get; set; }
        public string IPAddr { get; set; }
        public System.DateTime LoginDate { get; set; }
        public Nullable<System.DateTime> AccessDate { get; set; }
        public Nullable<System.DateTime> LogOutDate { get; set; }
        public string LogOutReason { get; set; }
    }
}
