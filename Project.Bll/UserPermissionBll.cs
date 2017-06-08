
using System;
using System.Collections.Generic;
using Project.Model;
using Project.Dal;
using System.Data.SqlClient;
namespace Project.Bll
{
	///========================================================================
	/// Project: 权限系统	
	/// Copyright: Copyright (c) 2008
	/// Company: 
   	///========================================================================
	
	///Title: UserPermission类
	///Description: UserPermission业务逻辑操作类
	///@author xu
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class UserPermissionBll
	{
		private readonly UserPermissionDal dal = new UserPermissionDal();

        /// <summary>
        /// 保存权限设置信息
        /// </summary>
        /// <param name="RoleId">角色ID</param>
        /// <param name="MenuIds">该角色拥有的菜单ID(等于'-1'则表示删除所有权限)</param>
        /// <returns>返回-1操作失败</returns>
        public int Save(int roleId, string menuIds)
        {
            return dal.Save(roleId, menuIds);
        }


        /// <summary>
        /// 获取某个角色所拥有的菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色所拥有的菜单</returns>
        public List<int> GetRoleMenuIds(int roleId)
        {
            return dal.GetRoleMenuIds(roleId);
        }
	}
}
