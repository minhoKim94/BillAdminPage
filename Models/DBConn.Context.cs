﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 템플릿에서 생성되었습니다.
//
//     이 파일을 수동으로 변경하면 응용 프로그램에서 예기치 않은 동작이 발생할 수 있습니다.
//     이 파일을 수동으로 변경하면 코드가 다시 생성될 때 변경 내용을 덮어씁니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillAdmin.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MHKIM_BILL_DBEntities : DbContext
    {
        public MHKIM_BILL_DBEntities()
            : base("name=MHKIM_BILL_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TAdminLog> TAdminLog { get; set; }
        public virtual DbSet<TAdminLoginFail> TAdminLoginFail { get; set; }
        public virtual DbSet<TAdminLoginFailHist> TAdminLoginFailHist { get; set; }
        public virtual DbSet<TAdminMenu> TAdminMenu { get; set; }
        public virtual DbSet<TAdminMenuGroup> TAdminMenuGroup { get; set; }
        public virtual DbSet<TAdminMenuRole> TAdminMenuRole { get; set; }
        public virtual DbSet<TAdminMenuRoleDtl> TAdminMenuRoleDtl { get; set; }
        public virtual DbSet<TAdminMgmt> TAdminMgmt { get; set; }
        public virtual DbSet<TAdminPwdHist> TAdminPwdHist { get; set; }
        public virtual DbSet<TAdminPwdReset> TAdminPwdReset { get; set; }
        public virtual DbSet<TAdminSession> TAdminSession { get; set; }
        public virtual DbSet<TAdminSessionHist> TAdminSessionHist { get; set; }
    
        public virtual ObjectResult<string> UP_ADMIN_INFO_GET_ID(string adminID, string adminPwd, ObjectParameter isValid)
        {
            var adminIDParameter = adminID != null ?
                new ObjectParameter("AdminID", adminID) :
                new ObjectParameter("AdminID", typeof(string));
    
            var adminPwdParameter = adminPwd != null ?
                new ObjectParameter("AdminPwd", adminPwd) :
                new ObjectParameter("AdminPwd", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("UP_ADMIN_INFO_GET_ID", adminIDParameter, adminPwdParameter, isValid);
        }
    
        public virtual ObjectResult<Nullable<int>> LoginModule(string adminID, string adminPwd, ObjectParameter isValid)
        {
            var adminIDParameter = adminID != null ?
                new ObjectParameter("AdminID", adminID) :
                new ObjectParameter("AdminID", typeof(string));
    
            var adminPwdParameter = adminPwd != null ?
                new ObjectParameter("AdminPwd", adminPwd) :
                new ObjectParameter("AdminPwd", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("LoginModule", adminIDParameter, adminPwdParameter, isValid);
        }
    
        public virtual ObjectResult<string> GetAdminInfo(string adminID, string adminPwd, ObjectParameter isValid)
        {
            var adminIDParameter = adminID != null ?
                new ObjectParameter("AdminID", adminID) :
                new ObjectParameter("AdminID", typeof(string));
    
            var adminPwdParameter = adminPwd != null ?
                new ObjectParameter("AdminPwd", adminPwd) :
                new ObjectParameter("AdminPwd", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetAdminInfo", adminIDParameter, adminPwdParameter, isValid);
        }
    
        public virtual ObjectResult<Nullable<int>> UP_LoginID(string adminID, string adminPwd, ObjectParameter isValid)
        {
            var adminIDParameter = adminID != null ?
                new ObjectParameter("AdminID", adminID) :
                new ObjectParameter("AdminID", typeof(string));
    
            var adminPwdParameter = adminPwd != null ?
                new ObjectParameter("AdminPwd", adminPwd) :
                new ObjectParameter("AdminPwd", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("UP_LoginID", adminIDParameter, adminPwdParameter, isValid);
        }
    
    }
}
