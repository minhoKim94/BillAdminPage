using BillAdmin.DataBaseAccess;
using BillAdmin.Models;
using System.Data;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace BillAdmin.Controllers
{
    public class AccountController : Controller
    {
        ///===============================================================
        /// <summary>
        /// FileName : AccountController.cs
        /// Description : Admin List
        /// Copyright : 2020 by PayLetter Inc. All rights reserved.
        /// Author : mh_kim@payletter.com, 2020-11-13
        /// Modify History : Just Created.
        /// </summary>
        ///================================================================
        
        #region DB관리
        #endregion
        AccountDAO objAccountDao = new AccountDAO();

        private int    pl_intRegsterCheck = 0;  // 회원가입 등록 결과 값
        private int    pl_intLoginResult  = 0;  // 로그인 등록 결과 값

        /// -------------------------------------------------------
        /// <summary>
        /// 로그인
        /// </summary>
        /// -------------------------------------------------------
        [AllowAnonymous]
        public ActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginForm(TAdminMgmt objAdminMgmt, FormCollection objFormCollect)
        {

            pl_intLoginResult = objAccountDao.LoginAdminInfo(objFormCollect["AdminID"], objFormCollect["AdminPwd"], GetIP());
            if (pl_intLoginResult.Equals(0))  // 로그인 성공 : 0 , 아이디 오류 : -1
            {
                ViewBag.Message = "login Successfully...!";
                FormsAuthentication.SetAuthCookie(objAdminMgmt.AdminID, objAdminMgmt.RememberMe);
                return RedirectToAction("Index", "Home", objAdminMgmt);
            }
            else if(pl_intLoginResult.Equals(-1))
            {
                ViewBag.Message = "Admin is not valid...!";
                return View();
            }
            else
            {
                ViewBag.Message = "password is not correct...!";
                return View();
            }

        }
        /// -------------------------------------------------------
        /// <summary>
        /// 로컬 IP GET
        /// </summary>
        /// -------------------------------------------------------
        private string GetIP()
        {
            string strHostName = "";
            strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[addr.Length - 1].ToString();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            objAccountDao.LogOutMgmt();
            return RedirectToAction("LoginForm", "Account");
        }


        /// -------------------------------------------------------
        /// <summary>
        /// 회원 가입
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult RegisterForm()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterForm(TAdminMgmt objAdminMgmt, FormCollection objFormCollect)
        {
            if (ModelState.IsValid)
            {
                objAdminMgmt.AdminID = objFormCollect["AdminID"];
                objAdminMgmt.AdminPwd = objFormCollect["AdminPwd"];
                objAdminMgmt.AdminName = objFormCollect["AdminName"];
                objAdminMgmt.Dept = objFormCollect["Dept"];
                objAdminMgmt.Position = objFormCollect["Position"];
                objAdminMgmt.OfficePhoneNo = objFormCollect["OfficePhoneNo"];
                objAdminMgmt.MPhoneNo = objFormCollect["MPhoneNo"];
                objAdminMgmt.EmailAddr = objFormCollect["EmailAddr"];
                objAdminMgmt.HAdminID = objFormCollect["AdminID"];
                pl_intRegsterCheck = objAccountDao.RegisterAdminInfo(objAdminMgmt);

                if (pl_intRegsterCheck.Equals(0))
                {
                    ViewBag.Message = "Registration successful.\\nUser Id: " + objAdminMgmt.AdminID.ToString();
                    return RedirectToAction("LoginForm", "Account");

                }
                else
                {
                    ViewBag.Message = "Admin id or Password alerady exist.\\nPlease choose a different username.";
                    return View(objAdminMgmt);
                }
            }
            else
            {
                return View("RegisterForm", objAdminMgmt);
            }

        }
        /// -------------------------------------------------------
        /// <summary>
        /// 관리자 상세 정보 모달
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult ShowAdminInfoModal(string strAdminId)
        {
            DataSet ds = objAccountDao.ShowAdminDetailInfo(strAdminId);
            ViewBag.adminInfo = ds.Tables[0];
            return View();
        }

        /// -------------------------------------------------------
        /// <summary>
        /// 관리자 비밀번호 수정
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult UpdateAdminPwdModal(string strAdminId)
        {
            DataSet ds = objAccountDao.SelectAdminId(strAdminId);
            ViewBag.adminPwdUpd = ds.Tables[0];
            return View();
        }

        [HttpPost]
        public ActionResult UpdateAdminPwdModal(string strAdminId, FormCollection objFormCollect)
        {
            TAdminMgmt objAdminMgmt = new TAdminMgmt();
            objAdminMgmt.AdminID    = objFormCollect["AdminID"];
            objAdminMgmt.AdminPwd   = objFormCollect["AdminPwd"];

            objAccountDao.UpdateAdminPwd(strAdminId, objAdminMgmt);
            return RedirectToAction("UpdateAdminPwdModal");
        }


    }

}