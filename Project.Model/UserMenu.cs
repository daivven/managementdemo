

using System;

namespace Project.Model
{
	///========================================================================
	/// Project: 权限项目	
   	///========================================================================
	/// <summary>
	///Title: UserMenu类
	///Description: dbo.UserMenu数据表实体模型代码
	///@author daiwen
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class UserMenu
	{	
		private int _menuId;
		private string _menuName = String.Empty;
		private string _menuAddress = String.Empty;
		private int _parentId;
		private int _menuOrder;
		private int _isNavigation;
        private int _roleHas;
        /// <summary>
        /// 指定的角色是否拥有该菜单
        /// </summary>
        public int RoleHas
        {
            get { return _roleHas; }
            set { _roleHas = value; }
        }
		/// <summary>
		/// 主键
		/// </summary>
		public int MenuId
		{
			get { return _menuId; }
			set { _menuId = value; }
		}

		/// <summary>
		/// 菜单名称
		/// </summary>
		public string MenuName
		{
			get { return _menuName; }
			set { _menuName = value; }
		}

		/// <summary>
		/// 菜单地址
		/// </summary>
		public string MenuAddress
		{
			get { return _menuAddress; }
			set { _menuAddress = value; }
		}

		/// <summary>
		/// 父级菜单Id
		/// </summary>
		public int ParentId
		{
			get { return _parentId; }
			set { _parentId = value; }
		}

		/// <summary>
		/// 菜单序号
		/// </summary>
		public int MenuOrder
		{
			get { return _menuOrder; }
			set { _menuOrder = value; }
		}

		/// <summary>
		/// 是否为左面侧导航菜单
		/// </summary>
		public int IsNavigation
		{
			get { return _isNavigation; }
			set { _isNavigation = value; }
		}

	}
}
