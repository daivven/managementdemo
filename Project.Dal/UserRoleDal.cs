
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
	///Title: UserRole类
	///Description: UserRole表SQLServer数据库操作实现代码
	///@author xu
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class UserRoleDal
	{		
		#region 数据库基本操作方法
		

		/// <summary>
		/// 在dbo.UserRole中新增一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被插入数据的实体对象</param>
		/// <param name="trans">事务参数</param>
		/// <returns>影响行数</returns>
		public int Insert(UserRole model,SqlTransaction trans)
		{
			const string sql = "INSERT INTO UserRole (RoleName,RoleDesc) VALUES (@RoleName,@RoleDesc)";
			
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
		
		
		private SqlParameter[] GetParms(UserRole model)
		{
			SqlParameter[] parms = {
				new SqlParameter("@RoleId", SqlDbType.Int, 4),			
				new SqlParameter("@RoleName", SqlDbType.VarChar, 50),			
				new SqlParameter("@RoleDesc", SqlDbType.VarChar, 50)			
				};
				
			parms[0].Value = model.RoleId;	
			parms[1].Value = model.RoleName;	
			parms[2].Value = model.RoleDesc;	
			return parms;
		}


		/// <summary>
		/// 在dbo.UserRole表中完整更新一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被更新数据的实体对象</param>
		/// <param name="trans">事务参数</param>
		/// <returns>影响行数</returns>
		public int Update(UserRole model,SqlTransaction trans)
		{
		    const string sql = "UPDATE UserRole SET RoleName=@RoleName, RoleDesc=@RoleDesc WHERE 1=1  AND RoleId=@RoleId";
				
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
		/// 在dbo.UserRole中删除一条记录,支持数据库事务
		/// </summary>
		/// <param name="id">主键</param>
		/// <param name="trans">事务参数</param>
		/// <returns>所影响的行数</returns>
		public int Delete(int id,SqlTransaction trans)
		{
		    const string sql ="DELETE FROM UserRole  WHERE 1=1  AND RoleId=@RoleId";
			
		    SqlParameter[] parms = {						
							new SqlParameter("@RoleId",SqlDbType.Int,4)
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
		/// 根据主键获取dbo.UserRole中的一条记录
		/// </summary>
		/// <param name="id">主键id</param>
		/// <returns>实体模型</returns>
		public UserRole  GetModel(int id)
		{  
			const string sql="SELECT * FROM UserRole WHERE  RoleId=@RoleId ";		   
		    SqlParameter[] parms = {						
							new SqlParameter("@RoleId",SqlDbType.Int,4)
		    };
		   
			parms[0].Value = id;
			UserRole model = null;
			
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
        /// 获取所有角色
        /// </summary>
        /// <returns>所有角色</returns>
        public List<UserRole> GetList()
        {
            const string sql = "SELECT * FROM UserRole";
            List<UserRole> list = new List<UserRole>();
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
        /// 装载dbo.UserRole实体
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>返回对象</returns>
		private UserRole LoadModel(IDataReader dr)
		{
				UserRole model = new UserRole();
					model.RoleId	 = Convert.ToInt32(dr["RoleId"]);
					model.RoleName	 = dr["RoleName"].ToString();
					model.RoleDesc	 = dr["RoleDesc"].ToString();
					return model;
		}
		#endregion
	}
}
