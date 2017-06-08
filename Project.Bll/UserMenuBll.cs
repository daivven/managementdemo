
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
	
	///Title: UserMenu类
	///Description: UserMenu业务逻辑操作类
	///@author xu
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class UserMenuBll
	{
		private readonly UserMenuDal dal = new UserMenuDal();
		
		#region 基本方法

         /// <summary>
        /// 获取顶级菜单列表
        /// </summary>
        /// <returns>顶级菜单列表</returns>
        public List<UserMenu> GetTopList()
        {
            return dal.GetTopList();
        }

        /// <summary>
        /// 对菜单按排序号进行排序
        /// </summary>
        /// <param name="list">待排序的</param>
        /// <returns>排序后的</returns>
        public List<UserMenu> GetOrderedList(List<UserMenu> list)
        {
            if (list != null && list.Count > 0)
            {
                List<UserMenu> newList = new List<UserMenu>();
                List<UserMenu> parentList = list.FindAll(delegate(UserMenu menu) { return menu.ParentId == 0; });
                for (int i = 0; i < parentList.Count; i++)
                {
                    newList.Add(parentList[i]);
                    newList.AddRange(list.FindAll(delegate(UserMenu menu) { return menu.ParentId == parentList[i].MenuId; }));
                }
                return newList;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取全部要菜单
        /// </summary>
        /// <returns>信息</returns>
        public List<UserMenu> GetList()
        {
            return dal.GetList();
        }


		/// <summary>
		/// 在dbo.UserMenu中新增一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被插入数据的实体对象</param>
		/// <param name="trans">事务参数，没有请填null</param>
		/// <returns>影响行数</returns>
		public int Insert(UserMenu model,SqlTransaction trans)
		{
            return dal.Insert(model, trans);
		}
		
		/// <summary>
		/// 在dbo.UserMenu表中完整更新一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被更新数据的实体对象</param>
		/// <param name="trans">事务参数，没有请填null</param>
		/// <returns>影响行数</returns>
		public int Update(UserMenu model,SqlTransaction trans)
		{
			return dal.Update(model,trans);	
		}
		
		/// <summary>
		/// 在dbo.UserMenu中删除一条记录,支持数据库事务
		/// </summary>
		/// <param name="id">主键</param>
		/// <param name="trans">事务参数，没有请填null</param>
		/// <returns>所影响的行数</returns>
		public int Delete(int id,SqlTransaction trans)
		{
			return dal.Delete(id,trans);	
		}
		
		
		/// <summary>
		/// 根据主键获取dbo.UserMenu中的一条记录
		/// </summary>
		/// <param name="id">主键id</param>
		/// <returns>实体模型</returns>
		public UserMenu  GetModel(int id)
		{
			return dal.GetModel(id);
		}

        /// <summary>
        /// 获取所有菜单及菜单对应角色的信息 菜单left join角色权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色所拥有的菜单</returns>
        public List<UserMenu> GetMenuRoleList(int roleId)
        {
            return dal.GetMenuRoleList(roleId);
        }

        /// <summary>
        /// 获取角色所拥有的所有菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色所拥有的菜单</returns>
        public List<UserMenu> GetRoleMenuList(string roleId)
        {
            return dal.GetRoleMenuList(roleId);
        }

        /// <summary>
        /// 获取某个角色所拥有的菜单地址列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色所拥有的菜单地址列表</returns>
        public List<string> GetRoleMenuAddress(string roleId)
        {
            return dal.GetRoleMenuAddress(roleId);
        }

		#endregion
	}
}
