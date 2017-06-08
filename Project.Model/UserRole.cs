

using System;

namespace Project.Model
{
	///========================================================================
	/// Project: 权限项目	
   	///========================================================================
	/// <summary>
	///Title: UserRole类
	///Description: dbo.UserRole数据表实体模型代码
	///@author xu
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class UserRole
	{	
		private int _roleId;
		private string _roleName = String.Empty;
		private string _roleDesc = String.Empty;
		
		/// <summary>
		/// 主键
		/// </summary>
		public int RoleId
		{
			get { return _roleId; }
			set { _roleId = value; }
		}

		/// <summary>
		/// 角色名称
		/// </summary>
		public string RoleName
		{
			get { return _roleName; }
			set { _roleName = value; }
		}

		/// <summary>
		/// 角色描述
		/// </summary>
		public string RoleDesc
		{
			get { return _roleDesc; }
			set { _roleDesc = value; }
		}

	}
}
