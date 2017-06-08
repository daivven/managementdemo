
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
	///Title: UserMenu类
	///Description: UserMenu表SQLServer数据库操作实现代码
	///@author xu
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 2012-10-9
	///@date 
	/// </summary>
	public class UserMenuDal
	{		
		#region 数据库基本操作方法

        /// <summary>
        /// 获取顶级菜单列表
        /// </summary>
        /// <returns>顶级菜单列表</returns>
        public List<UserMenu> GetTopList()
        {
            List<UserMenu> list = new List<UserMenu>();
            string sql = "select * from UserMenu where ParentId=0 order by MenuOrder asc ";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    UserMenu model = new UserMenu();
                    model.MenuId = dr.GetInt32(0);
                    model.MenuName = dr["MenuName"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }

		/// <summary>
		/// 在dbo.UserMenu中新增一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被插入数据的实体对象</param>
		/// <param name="trans">事务参数</param>
		/// <returns>影响行数</returns>
		public int Insert(UserMenu model,SqlTransaction trans)
		{
			const string sql = "INSERT INTO UserMenu (MenuName,MenuAddress,ParentId,MenuOrder,IsNavigation) VALUES (@MenuName,@MenuAddress,@ParentId,@MenuOrder,@IsNavigation)";
			
			SqlParameter[]parms = GetParms(model);
			
			int n = 0;
            try
            {
                if (trans == null)
                    n = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, sql, parms);
                else
                    n = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parms);
            }
            catch (Exception ex)
            {
                //Common.Log4NET.Log4Net.Error(ex.ToString(), ex);
            }
            return n;
		}
		
		
		private SqlParameter[] GetParms(UserMenu model)
		{
			SqlParameter[] parms = {
				new SqlParameter("@MenuId", SqlDbType.Int, 4),			
				new SqlParameter("@MenuName", SqlDbType.VarChar, 50),			
				new SqlParameter("@MenuAddress", SqlDbType.VarChar, 100),			
				new SqlParameter("@ParentId", SqlDbType.Int, 4),			
				new SqlParameter("@MenuOrder", SqlDbType.Int, 4),			
				new SqlParameter("@IsNavigation", SqlDbType.Int, 4)			
				};
				
			parms[0].Value = model.MenuId;	
			parms[1].Value = model.MenuName;	
			parms[2].Value = model.MenuAddress;	
			parms[3].Value = model.ParentId;	
			parms[4].Value = model.MenuOrder;	
			parms[5].Value = model.IsNavigation;	
			return parms;
		}


		/// <summary>
		/// 在dbo.UserMenu表中完整更新一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被更新数据的实体对象</param>
		/// <param name="trans">事务参数</param>
		/// <returns>影响行数</returns>
		public int Update(UserMenu model,SqlTransaction trans)
		{
		    const string sql = "UPDATE UserMenu SET MenuName=@MenuName, MenuAddress=@MenuAddress, ParentId=@ParentId, MenuOrder=@MenuOrder, IsNavigation=@IsNavigation WHERE 1=1  AND MenuId=@MenuId";
				
			SqlParameter[]parms = GetParms(model);
			
			int n = 0;
            try
            {
                if (trans == null)
                    n = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, sql, parms);
                else
                    n = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parms);
            }
            catch (Exception ex)
            {
                //Common.Log4NET.Log4Net.Error(ex.ToString(), ex);
            }
            return n;
		}
			
		
		/// <summary>
		/// 在dbo.UserMenu中删除一条记录,支持数据库事务
		/// </summary>
		/// <param name="id">主键</param>
		/// <param name="trans">事务参数</param>
		/// <returns>所影响的行数</returns>
		public int Delete(int id,SqlTransaction trans)
		{
		    const string sql ="DELETE FROM UserMenu  WHERE 1=1  AND MenuId=@MenuId";
			
		    SqlParameter[] parms = {						
							new SqlParameter("@MenuId",SqlDbType.Int,4)
		    };
			parms[0].Value = id;
			int n = 0;
            try
            {
                if (trans == null)
                    n = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, sql, parms);
                else
                    n = SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parms);
            }
            catch (Exception ex)
            {
                //Common.Log4NET.Log4Net.Error(ex.ToString(), ex);
            }
            return n;
		}		
		
		/// <summary>
		/// 根据主键获取dbo.UserMenu中的一条记录
		/// </summary>
		/// <param name="id">主键id</param>
		/// <returns>实体模型</returns>
		public UserMenu  GetModel(int id)
		{  
			const string sql="SELECT * FROM UserMenu WHERE  MenuId=@MenuId ";		   
		    SqlParameter[] parms = {						
							new SqlParameter("@MenuId",SqlDbType.Int,4)
		    };
		   
			parms[0].Value = id;
			UserMenu model = null;
			
			using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, parms))
            {
                if (dr.Read())
                {
                    model = LoadModel(dr);
                }
            }
            return model;
		}


        /// <summary>
        /// 获取全部要菜单
        /// </summary>
        /// <returns>信息</returns>
        public List<UserMenu> GetList()
        {
            List<UserMenu> list = new List<UserMenu>();
            string sql = "select * from UserMenu order by ParentId,MenuOrder asc ";

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    list.Add(LoadModel(dr));
                }
            }
            return list;
        }
		
        /// <summary>
        /// 装载dbo.UserMenu实体
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>返回对象</returns>
		private UserMenu LoadModel(IDataReader dr)
		{
				UserMenu model = new UserMenu();
					model.MenuId	 = Convert.ToInt32(dr["MenuId"]);
					model.MenuName	 = dr["MenuName"].ToString();
					model.MenuAddress	 = dr["MenuAddress"].ToString();
					model.ParentId	 = Convert.ToInt32(dr["ParentId"]);
					model.MenuOrder	 = Convert.ToInt32(dr["MenuOrder"]);
					model.IsNavigation	 = Convert.ToInt32(dr["IsNavigation"]);
					return model;
		}


        /// <summary>
        /// 获取所有菜单及菜单对应角色的信息 菜单left join角色权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色所拥有的菜单</returns>
        public List<UserMenu> GetMenuRoleList(int roleId)
        {
            List<UserMenu> list = new List<UserMenu>();
            string sql = string.Format(@"select t1.MenuId,t1.MenuName,t1.ParentId,t1.MenuOrder,t1.IsNavigation,t2.MenuId as roleHas from UserMenu t1
                            left join (select distinct(MenuId) from UserPermission where roleId in({0})  ) t2 
                            on t1.MenuId=t2.MenuId order by t1.parentId,t1.MenuOrder", roleId);

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    UserMenu model = new UserMenu();
                    model.MenuId = dr.GetInt32(0);
                    model.MenuName = dr["MenuName"].ToString();
                    model.ParentId = Convert.ToInt32(dr["ParentId"]);
                    model.MenuOrder = Convert.ToInt32(dr["MenuOrder"]);
                    model.IsNavigation = Convert.ToInt32(dr["IsNavigation"]);
                    model.RoleHas = dr["RoleHas"] == DBNull.Value ? 0 : Convert.ToInt32(dr["RoleHas"]);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取角色所拥有的所有菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色所拥有的菜单</returns>
        public List<UserMenu> GetRoleMenuList(string roleId)
        {
            List<UserMenu> list = new List<UserMenu>();
            string sql = string.Format(@"select t1.MenuId,t1.MenuName,t1.ParentId,t1.MenuOrder,t1.MenuAddress,t1.IsNavigation from UserMenu t1
                        inner join (select distinct(MenuId) from UserPermission where roleId in({0})  ) t2 
                        on t1.MenuId=t2.MenuId order by t1.parentId,t1.MenuOrder", roleId);

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    UserMenu model = new UserMenu();
                    model.MenuId = dr.GetInt32(0);
                    model.MenuName = dr["MenuName"].ToString();
                    model.ParentId = Convert.ToInt32(dr["ParentId"]);
                    model.MenuOrder = Convert.ToInt32(dr["MenuOrder"]);
                    model.MenuAddress = dr["MenuAddress"].ToString();
                    model.IsNavigation = Convert.ToInt32(dr["IsNavigation"]);
                    list.Add(model);
                }
            }
            return list;
        }


        /// <summary>
        /// 获取某个角色所拥有的菜单地址列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色所拥有的菜单地址列表</returns>
        public List<string> GetRoleMenuAddress(string roleId)
        {
            List<string> list = new List<string>();
            string sql = string.Format(@"select t1.MenuAddress from UserMenu t1 inner join (select distinct(MenuId) from UserPermission where RoleId in ({0}) ) t2 
                            on t1.MenuId=t2.MenuId ",roleId);

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    list.Add(dr["MenuAddress"].ToString().Trim());
                }
            }
            return list;
        }

		#endregion
	}
}
