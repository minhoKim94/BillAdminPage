using BillAdmin.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
///===============================================================
/// <summary>
/// FileName : AccountDAO.cs
/// Description : 로그인/회원가입 데이터 접근 클래스
/// Copyright : 2020 by PayLetter Inc. All rights reserved.
/// Author : mh_kim@payletter.com, 2020-11-13
/// Modify History : Just Created.
/// </summary>
///================================================================
namespace BillAdmin.DataBaseAccess
{
    public class AccountDAO
    {
        private int pl_intCodeReturn  = 0;
        private int pl_intLoginResult = 0;

        #region DB연결
        #endregion
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        
        #region Pwd 암호화
        #endregion
        AESEncrypt pl_objAes = new AESEncrypt();

        public object Covert { get; private set; }
        

        /// -------------------------------------------------------
        /// <summary>
        /// 로그인 수신 및 체크
        /// </summary>
        /// <returns> 0:정상, <> 0 비정상 </returns>
        /// -------------------------------------------------------
        public int LoginAdminInfo(string strAdminId, string strAdminPwd, string strIPAddr)
        {
            string strEncript = pl_objAes.Encrypt(strAdminPwd);
            try
            {
                SqlCommand com = new SqlCommand("dbo.UP_ADMIN_SESSION_TX_INS", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@pi_strAdminID", strAdminId);
                com.Parameters.AddWithValue("@pi_strAdminPwd", strEncript);
                com.Parameters.AddWithValue("@pi_strIPAddr", strIPAddr);
                SqlParameter objSqlParam = new SqlParameter();
                objSqlParam.ParameterName = "@po_intRetVal";
                objSqlParam.SqlDbType = SqlDbType.Int;
                objSqlParam.Direction = ParameterDirection.Output;
                com.Parameters.Add(objSqlParam);
                con.Open();
                com.ExecuteNonQuery();
                pl_intLoginResult = Convert.ToInt32(objSqlParam.Value);
                return pl_intLoginResult;
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
        /// -------------------------------------------------------
        /// <summary>
        /// 로그 아웃
        /// </summary>
        /// -------------------------------------------------------
        public void LogOutMgmt()
        {
            try
            {
                SqlCommand com = new SqlCommand("dbo.UP_ADMIN_SESSION_LOGOUT_NT_UPD", con);
                com.CommandType = CommandType.StoredProcedure;
                con.Open();
                com.ExecuteNonQuery();
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
            finally
            {
                con.Close();
            }
        }
        /// -------------------------------------------------------
        /// <summary>
        /// 관리자 정보 조회 
        /// </summary>
        /// -------------------------------------------------------
        public DataSet ShowAdminDetailInfo(string strAdminId)
        {
            SqlCommand com = new SqlCommand("dbo.UP_ADMIN_DETAIL_INFO_GET", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@pl_strAdminID", strAdminId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet SearchAdminDetailInfo(string strAdminID)
        {
            SqlCommand com = new SqlCommand("dbo.UP_GET_SEARCH_ADMINUSER_LIST_INFO", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@pi_strAdminID", strAdminID);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        /// -------------------------------------------------------
        /// <summary>
        /// 관리자 패스워드 수정
        /// </summary>
        /// -------------------------------------------------------
        public void UpdateAdminPwd(string strAdminId, TAdminMgmt objAdminMgmt)
        {
            try
            {
                SqlCommand com = new SqlCommand("dbo.UP_ADMIN_PWD_UPDATE", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@pi_strAdminID", strAdminId);
                com.Parameters.AddWithValue("@pi_strAdminPwd", objAdminMgmt.AdminPwd);
                con.Open();
                com.ExecuteNonQuery();
                return;

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet SelectAdminId(string strAdminId)
        {
            SqlCommand com = new SqlCommand("dbo.UP_ADMIN_ID_GET", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@pi_strAdminID", strAdminId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        /// -------------------------------------------------------
        /// <summary>
        /// 관리자 회원가입 
        /// </summary>
        /// <returns> 0:정상, <> 0 비정상 </returns>
        /// -------------------------------------------------------
        public int RegisterAdminInfo(TAdminMgmt objAdminMgmt)
        {
            try
            {
                SqlCommand com = new SqlCommand("dbo.UP_ADMIN_REGISTER_INFO", con);

                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@pi_strAdminID",       objAdminMgmt.AdminID);
                com.Parameters.AddWithValue("@pi_strAdminPwd",      pl_objAes.Encrypt(objAdminMgmt.AdminPwd));
                com.Parameters.AddWithValue("@pi_strAdminName",     objAdminMgmt.AdminName);
                com.Parameters.AddWithValue("@pi_strDept",          objAdminMgmt.Dept);
                com.Parameters.AddWithValue("@pi_strPosition",      objAdminMgmt.Position);
                com.Parameters.AddWithValue("@pi_strOfficePhoneNo", objAdminMgmt.OfficePhoneNo);
                com.Parameters.AddWithValue("@pi_strMPhoneNo",      objAdminMgmt.MPhoneNo);
                com.Parameters.AddWithValue("@pi_strEmailAddr",     objAdminMgmt.EmailAddr);
                com.Parameters.AddWithValue("@pi_strHAdminID",      objAdminMgmt.HAdminID);

                SqlParameter objSqlParam = new SqlParameter();
                objSqlParam.ParameterName = "@po_intRetVal";
                objSqlParam.SqlDbType = SqlDbType.Int;
                objSqlParam.Direction = ParameterDirection.Output;
                com.Parameters.Add(objSqlParam);
                con.Open();
                return pl_intCodeReturn;
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
    }
}