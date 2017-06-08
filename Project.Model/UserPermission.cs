

using System;

namespace Project.Model
{
	///========================================================================
	/// Project: 权限项目	
   	///========================================================================
	/// <summary>
	///Title: UserPermission类
	///Description: dbo.UserPermission数据表实体模型代码
	///@author xu
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class UserPermission
	{	
		private int _id;
		private int _roleId;
		private int _menuId;
		
		/// <summary>
		/// 主键
		/// </summary>
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// 角色Id
		/// </summary>
		public int RoleId
		{
			get { return _roleId; }
			set { _roleId = value; }
		}

		/// <summary>
		/// 菜单Id
		/// </summary>
		public int MenuId
		{
			get { return _menuId; }
			set { _menuId = value; }
		}

	}
}
