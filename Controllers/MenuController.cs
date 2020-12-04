using BillAdmin.DataBaseAccess;
using BillAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
///===============================================================
/// <summary>
/// FileName : MenuController.cs
/// Description : 메뉴/권한 탭 컨트롤러
/// Copyright : 2020 by PayLetter Inc. All rights reserved.
/// Author : mh_kim@payletter.com, 2020-11-09
/// Modify History : Just Created.
/// </summary>
///================================================================
namespace BillAdmin.Controllers
{
    public class MenuController : Controller
    {
        #region DB 관리
        DataBaseAccess.MenuDAO menuDao = new DataBaseAccess.MenuDAO();
        #endregion
        AdminDAO objAdminDao = new AdminDAO();

        /// -------------------------------------------------------
        /// <summary>
        ///  MenuGroup 페이지 : Show Menu Group List
        /// </summary>
        /// ------------------------------------------------------- 
        public ActionResult MenuGroupForm()
        {
            DataSet ds = menuDao.ShowMgroupDataALL();
            ViewBag.menuGrp = ds.Tables[0];

            return View();
        }

        /// -------------------------------------------------------
        /// <summary>
        ///  MenuGroup 페이지 : Insert Menu Group List
        /// </summary>
        /// ------------------------------------------------------- 
        public ActionResult MenuGroupInsert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MenuGroupInsert(string MenuGroupName, short MenuGroupSort, string UseFlag, string MenuGroupIcon)
        {
            TAdminMenuGroup objTadminMenuGroup = new TAdminMenuGroup();
            TAdminMgmt objTadminMgmt = new TAdminMgmt();

            HttpCookie cookie = new HttpCookie("AdminID", objTadminMgmt.AdminID);
            string strAdminID = cookie.Values["AdminID"];

            objTadminMenuGroup.AdminID = "payletter";
            objTadminMenuGroup.MenuGroupName = MenuGroupName;
            objTadminMenuGroup.MenuGroupSort = MenuGroupSort;
            objTadminMenuGroup.MenuGroupIcon = MenuGroupIcon;
            objTadminMenuGroup.UseFlag = UseFlag;
            
            return Json(menuDao.AddMgroupData(objTadminMenuGroup), JsonRequestBehavior.AllowGet);
        }

        /// -------------------------------------------------------
        /// <summary>
        ///  MenuGroup 페이지 : Delete Menu Group List
        /// </summary>
        /// ------------------------------------------------------- 
        public ActionResult MenuGroupDelete(int intMgroupNo)
        {
            menuDao.DeleteMgroupData(intMgroupNo);
            return RedirectToAction("MenuGroupForm");
        }

        /// -------------------------------------------------------
        /// <summary>
        ///  Menu 페이지 : Show Menu Dropdwon List
        /// </summary>
        /// -------------------------------------------------------       
        public ActionResult MenuForm()
        {
            MHKIM_BILL_DBEntities db = new MHKIM_BILL_DBEntities();
            TAdminMenu objAdminMenu = new TAdminMenu();

            var menuGroupList = db.TAdminMenuGroup.ToList().Where(a => a.UseFlag.Equals("Y"));

            objAdminMenu.MenuGroupList = (from d in menuGroupList
                                          select new SelectListItem
                                     {
                                         Value = d.MenuGroupNo.ToString(),
                                         Text = d.MenuGroupName
                                     }).ToList();

            List<TAdminMenu> lstAdminMenu = new List<TAdminMenu>();
            DataTable dtManager = new DataTable();

            menuDao.ShowMenuDataAll(dtManager);

            foreach (DataRow dtrow in dtManager.Rows)
            {
                TAdminMenu objAdminMenulogs = new TAdminMenu();
                objAdminMenulogs.MenuNo = Convert.ToInt16(dtrow["MenuNo"].ToString());
                objAdminMenulogs.MenuName = dtrow["MenuName"].ToString();
                objAdminMenulogs.MenuENName = dtrow["MenuENName"].ToString();
                objAdminMenulogs.MenuLink = dtrow["MenuLink"].ToString();
                objAdminMenulogs.MenuSort = Convert.ToInt16(dtrow["MenuSort"].ToString());
                objAdminMenulogs.AdminID = dtrow["AdminID"].ToString();

                objAdminMenulogs.RegDate = Convert.ToDateTime(dtrow["RegDate"].ToString());
                objAdminMenulogs.UpdDate = (dtrow["UpdDate"] == DBNull.Value) ? (DateTime?)null : ((DateTime)dtrow["UpdDate"]);
                lstAdminMenu.Add(objAdminMenulogs);
            }

            objAdminMenu.AdminMenuGrid = lstAdminMenu;

            return View("MenuForm", objAdminMenu);
        }

        /// -------------------------------------------------------
        /// <summary>
        ///  Menu 페이지 : Show Menu Dropdwon Filter List
        /// </summary>
        /// -------------------------------------------------------        
        public ActionResult MenuFilter(int intMenuGroupNo)
        {
            MHKIM_BILL_DBEntities db = new MHKIM_BILL_DBEntities();

            int? menuGroupNo = Convert.ToInt16(intMenuGroupNo);
            List<TAdminMenu> lstAdminMenu = new List<TAdminMenu>();

            var menuGroupList = db.TAdminMenuGroup.ToList();
            if (menuGroupNo.Equals(0))
            {
                DataTable dtManager = new DataTable();
                menuDao.ShowMenuDataAll(dtManager);
                foreach (DataRow dtrow in dtManager.Rows)
                {
                    TAdminMenu objAdminMenuLogs = new TAdminMenu();
                    objAdminMenuLogs.MenuNo = Convert.ToInt16(dtrow["MenuNo"].ToString());
                    objAdminMenuLogs.MenuName = dtrow["MenuName"].ToString();
                    objAdminMenuLogs.MenuENName = dtrow["MenuENName"].ToString();
                    objAdminMenuLogs.MenuLink = dtrow["MenuLink"].ToString();
                    objAdminMenuLogs.MenuSort = Convert.ToInt16(dtrow["MenuSort"].ToString());
                    objAdminMenuLogs.AdminID = dtrow["AdminID"].ToString();

                    objAdminMenuLogs.RegDate = Convert.ToDateTime(dtrow["RegDate"].ToString());
                    objAdminMenuLogs.UpdDate = (dtrow["UpdDate"] == DBNull.Value) ? (DateTime?)null : ((DateTime)dtrow["UpdDate"]); lstAdminMenu.Add(objAdminMenuLogs);
                }
            }
            else
            {
                DataTable dtManager = new DataTable();
                menuDao.ShowMenuDataManage(dtManager, intMenuGroupNo);
                foreach (DataRow dtrow in dtManager.Rows)
                {
                    TAdminMenu objAdminMenuLogs = new TAdminMenu();
                    objAdminMenuLogs.MenuNo = Convert.ToInt16(dtrow["MenuNo"].ToString());
                    objAdminMenuLogs.MenuName = dtrow["MenuName"].ToString();
                    objAdminMenuLogs.MenuENName = dtrow["MenuENName"].ToString();
                    objAdminMenuLogs.MenuLink = dtrow["MenuLink"].ToString();
                    objAdminMenuLogs.MenuSort = Convert.ToInt16(dtrow["MenuSort"].ToString());
                    objAdminMenuLogs.AdminID = dtrow["AdminID"].ToString();

                    objAdminMenuLogs.RegDate = Convert.ToDateTime(dtrow["RegDate"].ToString());
                    objAdminMenuLogs.UpdDate = (dtrow["UpdDate"] == DBNull.Value) ? (DateTime?)null : ((DateTime)dtrow["UpdDate"]); lstAdminMenu.Add(objAdminMenuLogs);
                }
            }
            return PartialView("_ShowMenuList", lstAdminMenu);
        }

        /// -------------------------------------------------------
        /// <summary>
        ///  Menu 페이지 : Update Menu Details Info
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult MenuUpdate(TAdminMenu objAdminMenu)
        {
            return Json(menuDao.UpdMenuData(objAdminMenu), JsonRequestBehavior.AllowGet);
        }

        /// -------------------------------------------------------
        /// <summary>
        ///  Menu 페이지 : Delete Menu Details Info
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult MenuDelete(int intMenuNo)
        {
            menuDao.DeleteMenuData(intMenuNo);
            return RedirectToAction("MenuForm");
        }

        /// -------------------------------------------------------
        /// <summary>
        ///  Menu 페이지 : Insert Menu Details Info
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult MenuInsert(TAdminMenu objAdminMenu)
        {
            return Json(menuDao.AddMenuData(objAdminMenu), JsonRequestBehavior.AllowGet);
        }

        /// -------------------------------------------------------
        /// <summary>
        ///  MenuRole 페이지 : Show Menu Role Details Info
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult MenuRoleForm(TAdminMenuRole objAdminMenuRole)
        {
            DataSet ds = menuDao.ShowMenuRoleData();
            ViewBag.menuRolelst = ds.Tables[0];

            return View();
        }
    }
}