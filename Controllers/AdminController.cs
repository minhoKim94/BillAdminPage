using BillAdmin.DataBaseAccess;
using BillAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
///===============================================================
/// <summary>
/// FileName : AdminController.cs
/// Description : 관리자 관련 탭 컨트롤러
/// Copyright : 2020 by PayLetter Inc. All rights reserved.
/// Author : mh_kim@payletter.com, 2020-11-10
/// Modify History : Just Created.
/// </summary>
///================================================================
namespace BillAdmin.Controllers
{
    public class AdminController : Controller
    {
        AccountDAO objActDao = new AccountDAO();
        AdminDAO objAdminDao = new AdminDAO();

        /// -------------------------------------------------------
        /// <summary>
        /// 관리자 리스트
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult AdminFrm()
        {
            DataSet ds = objActDao.SearchAdminDetailInfo("");
            ViewBag.adminList = ds.Tables[0];
            return View();
        }

        [HttpPost]
        public ActionResult AdminFrm(string strAdminId)
        {
            DataSet ds = objActDao.SearchAdminDetailInfo(strAdminId);
            ViewBag.adminList = ds.Tables[0];
            return View();
        }

        /// -------------------------------------------------------
        /// <summary>
        /// 실시간 세션 리스트
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult SessionLstFrm()
        {
            DataSet ds = objAdminDao.SearchAdminSessionLst("", "");
            ViewBag.sesionList = ds.Tables[0];
            return View();
        }

        [HttpPost]
        public ActionResult SessionLstFrm(string strAdminId, string strIPAddr)
        {
            DataSet ds = objAdminDao.SearchAdminSessionLst(strAdminId, strIPAddr);
            ViewBag.sesionList = ds.Tables[0];
            return View();
        }
        /// -------------------------------------------------------
        /// <summary>
        /// 로그인 세션 이력 리스트
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult LoginSessionLstFrm()
        {
            DataSet ds = objAdminDao.SearchAdminLoginSessionLst("", "");
            ViewBag.loginSesionList = ds.Tables[0];
            return View();
        }

        [HttpPost]
        public ActionResult LoginSessionLstFrm(string strAdminId, string strIPAddr)
        {
            DataSet ds = objAdminDao.SearchAdminLoginSessionLst(strAdminId, strIPAddr);
            ViewBag.loginSesionList = ds.Tables[0];
            return View();
        }

        /// -------------------------------------------------------
        /// <summary>
        /// 로그인 실패 이력 리스트
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult LoginFailLstFrm()
        {
            List<TAdminLoginFail> lstAdminLoginFail = new List<TAdminLoginFail>();
            TAdminLoginFail objAdminLoginFail = new TAdminLoginFail();
            DataSet ds = new DataSet();
            ds = objAdminDao.SearchAdminSessionFailLst("");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TAdminLoginFail objAdminLoginFailLog = new TAdminLoginFail();
                objAdminLoginFailLog.TryYMD = Convert.ToDateTime(dr["TryYMD"]);
                objAdminLoginFailLog.AdminID = dr["AdminID"].ToString();
                objAdminLoginFailLog.TryCnt = Convert.ToByte(dr["TryCnt"]);
                objAdminLoginFailLog.ResetCnt = Convert.ToByte(dr["ResetCnt"]);
                objAdminLoginFailLog.HAdminID = dr["HAdminID"].ToString();
                objAdminLoginFailLog.RegDate = Convert.ToDateTime(dr["RegDate"]);
                objAdminLoginFailLog.UpdDate = (dr["UpdDate"] == DBNull.Value) ? (DateTime?)null : ((DateTime)dr["UpdDate"]);

                lstAdminLoginFail.Add(objAdminLoginFail);
            }
            objAdminLoginFail.AdminLoginFailGrid = lstAdminLoginFail;

            return View("LoginFailLstFrm", objAdminLoginFail);
        }

        [HttpPost]
        public ActionResult LoginFailLstFrm(string strAdminID)
        {
            List<TAdminLoginFail> lstAdminLoginFail = new List<TAdminLoginFail>();
            DataSet ds = new DataSet();
            ds = objAdminDao.SearchAdminSessionFailLst(strAdminID);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TAdminLoginFail objAdminLoginFail = new TAdminLoginFail();
                objAdminLoginFail.TryYMD = Convert.ToDateTime(dr["TryYMD"]);
                objAdminLoginFail.AdminID = dr["AdminID"].ToString();
                objAdminLoginFail.TryCnt = Convert.ToByte(dr["TryCnt"]);
                objAdminLoginFail.ResetCnt = Convert.ToByte(dr["ResetCnt"]);
                objAdminLoginFail.HAdminID = dr["HAdminID"].ToString();
                objAdminLoginFail.RegDate = Convert.ToDateTime(dr["RegDate"]);
                objAdminLoginFail.UpdDate = (dr["UpdDate"] == DBNull.Value) ? (DateTime?)null : ((DateTime)dr["UpdDate"]);

                lstAdminLoginFail.Add(objAdminLoginFail);
            }
            return PartialView("_ShowLoginFailList", lstAdminLoginFail);
        }
        /// -------------------------------------------------------
        /// <summary>
        /// 관리자 로그 입력
        /// </summary>
        /// -------------------------------------------------------
        public JsonResult AdminLogInsert(TAdminLog objAdminLog)
        {
            return Json(objAdminDao.AddAdminLogData(objAdminLog), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdminDelete(string strAdminID)
        {
            objAdminDao.DeleteAdmin(strAdminID);
            return RedirectToAction("AdminFrm");
        }
    } 
}