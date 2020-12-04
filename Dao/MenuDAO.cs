using BillAdmin.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

///===============================================================
/// <summary>
/// FileName : MenuDAO.cs
/// Description : 메뉴/권한 관리 탭
/// Copyright : 2020 by PayLetter Inc. All rights reserved.
/// Author : mh_kim@payletter.com, 2020-11-06
/// Modify History : Just Created.
/// </summary>
///================================================================
namespace BillAdmin.DataBaseAccess
{
    public class MenuDAO
    {
        #region DB연결

        #endregion
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        int pl_intRetVal = 0;

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Menu Group 전체 리스트
        /// </summary>
        /// 
        /// <returns></returns>
        //--------------------------------------------------------------------------------------
        public DataSet ShowMgroupDataALL()
        {
           
            SqlCommand com = new SqlCommand("dbo.UP_ADMIN_MENUGROUP_NT_INS", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@po_intRetVal", 4);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
         
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Menu Group 추가
        /// </summary>
        /// 
        /// <returns></returns>
        //--------------------------------------------------------------------------------------
        public int AddMgroupData(TAdminMenuGroup objAdminMenuGroup)
        {
            try
            {
                SqlCommand com = new SqlCommand("UP_MENUGROUP_INFO_ADD", con);

                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@MenuGroupName", objAdminMenuGroup.MenuGroupName);
                com.Parameters.AddWithValue("@MenuGroupSort", objAdminMenuGroup.MenuGroupSort);
                com.Parameters.AddWithValue("@MenuGroupIcon", objAdminMenuGroup.MenuGroupIcon);
                com.Parameters.AddWithValue("@UseFlag", objAdminMenuGroup.UseFlag);
                com.Parameters.AddWithValue("@AdminID", objAdminMenuGroup.AdminID);
                con.Open();
                pl_intRetVal = com.ExecuteNonQuery();
                return pl_intRetVal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
            finally
            {
                con.Close();
            }
           
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Menu Group 삭제
        /// </summary>
        /// 
        /// <returns></returns>
        //--------------------------------------------------------------------------------------
        public void DeleteMgroupData(int intMgroupNo)
        {
            try
            {
                SqlCommand com = new SqlCommand("UP_ADMIN_MENUGROUP_NT_INS", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@pi_MenuGroupNo", intMgroupNo);
                com.Parameters.AddWithValue("@po_intRetVal", 3);
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
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Show Menu 전체
        /// </summary>
        /// 
        /// <returns></returns>
        //--------------------------------------------------------------------------------------
        public void ShowMenuDataAll(DataTable dtAdminMenu)
        {
            SqlCommand com = new SqlCommand("dbo.UP_MENU_INFO_GET_ALL", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@pl_intUseStateCode", 1);
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dtAdminMenu);
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Menu 관리
        /// </summary>
        /// 
        /// <returns></returns>
        //--------------------------------------------------------------------------------------
        public void ShowMenuDataManage(DataTable dtAdminMenu, int intMenuGroupNo)
        {
            SqlCommand com = new SqlCommand("dbo.UP_MENU_INFO_GET_ID", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@pl_strMenuGroupNo", intMenuGroupNo);
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dtAdminMenu);
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Menu 삽입
        /// </summary>
        /// 
        /// <returns></returns>
        //--------------------------------------------------------------------------------------
        public int AddMenuData(TAdminMenu objAdminMenu)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("dbo.UP_MENU_INFO_ADD", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@pi_MenuGroupNo", objAdminMenu.MenuGroupNo);
                com.Parameters.AddWithValue("@pi_MenuName", objAdminMenu.MenuName);
                com.Parameters.AddWithValue("@pi_MenuENName", objAdminMenu.MenuENName);
                com.Parameters.AddWithValue("@pi_MenuLink", objAdminMenu.MenuLink);
                com.Parameters.AddWithValue("@pi_MenuSort", objAdminMenu.MenuSort);
                com.Parameters.AddWithValue("@pi_MenuDesc", objAdminMenu.MenuDesc);
                com.Parameters.AddWithValue("@pi_MenuENDesc", objAdminMenu.MenuENDesc);
                com.Parameters.AddWithValue("@pi_UseStateCode", objAdminMenu.UseStateCode);
                com.Parameters.AddWithValue("@pi_AdminID", "payletter");
                pl_intRetVal = com.ExecuteNonQuery();
                return pl_intRetVal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Menu 수정
        /// </summary>
        /// 
        /// <returns></returns>
        //--------------------------------------------------------------------------------------
        public int UpdMenuData(TAdminMenu objAdminMenu)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("dbo.UP_MENU_INFO_UPD", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@pi_MenuNo", objAdminMenu.MenuNo);
                com.Parameters.AddWithValue("@pi_MenuName", objAdminMenu.MenuName);
                com.Parameters.AddWithValue("@pi_MenuENName", objAdminMenu.MenuENName);
                com.Parameters.AddWithValue("@pi_MenuLink", objAdminMenu.MenuLink);
                com.Parameters.AddWithValue("@pi_UseStateCode", objAdminMenu.UseStateCode);
                com.Parameters.AddWithValue("@pi_MenuDesc", objAdminMenu.MenuDesc);
                com.Parameters.AddWithValue("@pi_MenuENDesc", objAdminMenu.MenuENDesc);


                pl_intRetVal = com.ExecuteNonQuery();
                return pl_intRetVal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Menu 삭제
        /// </summary>
        /// 
        /// <returns></returns>
        //--------------------------------------------------------------------------------------
        public void DeleteMenuData(int intMenuNo)
        {
            try
            {
                SqlCommand com = new SqlCommand("dbo.UP_MENU_INFO_DEL", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@pi_MenuNo", intMenuNo);

                con.Open();
                com.ExecuteNonQuery();
                return ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ;
            }
            finally
            {
                con.Close();
            }
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Menu Role Show
        /// </summary>
        /// 
        /// <returns></returns>
        //--------------------------------------------------------------------------------------
        public DataSet ShowMenuRoleData()
        {
            SqlCommand com = new SqlCommand("dbo.UP_ADMIN_MENU_ROLE_LST", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

    }
}