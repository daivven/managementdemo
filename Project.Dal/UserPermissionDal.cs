
using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Project.Model;
using System.Data;
using Project.Common;


///========================================================================
/// Project: 权限系统	
/// Copyright: Copyright (c) 2007
/// Company: 
///========================================================================

namespace Project.Dal
{

	/// <summary>
	///Title: UserPermission类
	///Description: UserPermission表SQLServer数据库操作实现代码
	///@author xu
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class UserPermissionDal
	{
        /// <summary>
        /// 保存权限设置信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="menuIds">该角色拥有的菜单ID(等于'-1'则表示删除所有权限)</param>
        /// <returns>返回-1操作失败</returns>
        public int Save(int roleId, string menuIds)
        {
            SqlParameter[] parms = {
                                      new SqlParameter("@RoleId",SqlDbType.Int),
                                      new SqlParameter("@str",SqlDbType.VarChar,-1),
                                      new SqlParameter("@Return_value",SqlDbType.Int)
                                   };
            parms[0].Value = roleId;
            parms[1].Value = menuIds;
            parms[2].Direction = ParameterDirection.ReturnValue;
            int n = -1;
            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.StoredProcedure, "UP_InsertPermission", parms);
                n = Convert.ToInt32(parms[2].Value);
            }
            catch (Exception ex)
            {
                //Common.Log4NET.Log4Net.Error(ex.ToString(), ex);
            }
            return n;
        }



        /// <summary>
        /// 获取某个角色所拥有的菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色所拥有的菜单</returns>
        public List<int> GetRoleMenuIds(int roleId)
        {
            List<int> menuIds = new List<int>();
            const string sql = "select * from UserPermission where RoleId=@RoleId";
            SqlParameter parms = new SqlParameter("@RoleId", SqlDbType.Int);
            parms.Value = roleId;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, parms))
            {
                while (dr.Read())
                {
                    menuIds.Add(Convert.ToInt32(dr["MenuId"]));
                }
            }
            return menuIds;
        }
	}
}
