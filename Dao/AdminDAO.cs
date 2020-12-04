using BillAdmin.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

///===============================================================
/// <summary>
/// FileName : AdminDAO.cs
/// Description : 관리자/로그인 탭
/// Copyright : 2020 by PayLetter Inc. All rights reserved.
/// Author : mh_kim@payletter.com, 2020-11-13
/// Modify History : Just Created.
/// </summary>
///================================================================
namespace BillAdmin.DataBaseAccess
{
    public class AdminDAO
    {

        #region DB연결
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        #endregion

        private int pl_intRetVal = 0;

        /// -------------------------------------------------------
        /// <summary>
        /// 관리자 세션 리스트 
        /// </summary>
        /// -------------------------------------------------------
        public DataSet SearchAdminSessionLst(string strAdminID, string strIPAddr)
        {
            SqlCommand com = new SqlCommand("dbo.UP_ADMIN_SESSION_AR_LST", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@pi_strAdminID", strAdminID);
            com.Parameters.AddWithValue("@pi_strIPAddr", strIPAddr);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        /// -------------------------------------------------------
        /// <summary>
        /// 로그인 성공 시 세션 리스트 Show
        /// </summary>
        /// -------------------------------------------------------
        public DataSet SearchAdminLoginSessionLst(string strAdminID, string strIPAddr)
        {
            SqlCommand com = new SqlCommand("dbo.UP_ADMIN_SESSION_HIST_AR_LST", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@pi_strAdminID", strAdminID);
            com.Parameters.AddWithValue("@pi_strIPAddr", strIPAddr);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        /// -------------------------------------------------------
        /// <summary>
        ///  로그인 실패 시 세션 리스트 Show
        /// </summary>
        /// -------------------------------------------------------
        public DataSet SearchAdminSessionFailLst(string strAdminID)
        {
            SqlCommand com = new SqlCommand("dbo.UP_ADMIN_LOGIN_FAIL_AR_LST", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@pi_strAdminID", strAdminID);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        /// -------------------------------------------------------
        /// <summary>
        ///  관리자 로그 추적 Insert
        /// </summary>
        /// -------------------------------------------------------
        public int AddAdminLogData(TAdminLog objAdminLog)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("dbo.UP_ADMIN_LOG_TX_INS", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@pi_strMenuLink", objAdminLog.MenuLink);
                com.Parameters.AddWithValue("@pi_strMethodName", objAdminLog.MethodName);
                com.Parameters.AddWithValue("@pi_strIPAddr", objAdminLog.IPAddr);
                com.Parameters.AddWithValue("@pi_strAdminID", objAdminLog.AdminID);
                pl_intRetVal = com.ExecuteNonQuery();
                return pl_intRetVal;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteAdmin(string strAdminID)
        {
            try
            {
                SqlCommand com = new SqlCommand("UP_ADMIN_DEL", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@pi_strAdminID", strAdminID);
                con.Open();
                com.ExecuteNonQuery();
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                con.Close();
            }
        }
    }
}