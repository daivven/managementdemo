
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
	///Title: UserInfo类
	///Description: UserInfo表SQLServer数据库操作实现代码
	///@author xu
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class UserInfoDal
	{		
		#region 数据库基本操作方法

        /// <summary>
        /// 根据条件分页查询信息
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="whereSql">分页查询条件</param>
        /// <param name="recordCount">out参数，当前条件下的总记录数</param>
        /// <returns>分页查询后的信息</returns>
        public DataTable GetList(int pageIndex, int pageSize, string whereSql, out int recordCount)
        {
            DataTable dt = null;
            int startIndex = pageIndex <= 1 ? 1 : ((pageIndex - 1) * pageSize + 1);
            int endIndex = startIndex + pageSize - 1;
            SqlParameter[] parms = { 
                                       new SqlParameter("@StartIndex",SqlDbType.Int),
                                       new SqlParameter("@EndIndex",SqlDbType.Int),
                                       new SqlParameter("@WhereSql",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@RecordCount",SqlDbType.Int)
                                   };
            parms[0].Value = startIndex;
            parms[1].Value = endIndex;
            parms[2].Value = whereSql;
            parms[3].Direction = ParameterDirection.Output;
            dt = SQLHelper.GetDataTable(CommandType.StoredProcedure, "UP_UserInfo_List_select", parms);
            recordCount = Convert.ToInt32(parms[3].Value);
            return dt;
        }


		/// <summary>
		/// 在dbo.UserInfo中新增一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被插入数据的实体对象</param>
		/// <param name="trans">事务参数</param>
		/// <returns>影响行数</returns>
		public int Insert(UserInfo model,SqlTransaction trans)
		{
			const string sql = "INSERT INTO UserInfo (UserName,UserPwd,RoleId,[Desc],IsLock) VALUES (@UserName,@UserPwd,@RoleId,@Desc,@IsLock)";
			
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
		
		
		private SqlParameter[] GetParms(UserInfo model)
		{
			SqlParameter[] parms = {
				new SqlParameter("@UserId", SqlDbType.Int, 4),			
				new SqlParameter("@UserName", SqlDbType.VarChar, 50),			
				new SqlParameter("@UserPwd", SqlDbType.VarChar, 50),			
				new SqlParameter("@RoleId", SqlDbType.VarChar,1000),			
				new SqlParameter("@Desc", SqlDbType.VarChar, 50),			
				new SqlParameter("@IsLock", SqlDbType.Int, 4)			
				};
				
			parms[0].Value = model.UserId;	
			parms[1].Value = model.UserName;	
			parms[2].Value = model.UserPwd;	
			parms[3].Value = model.RoleId;	
			if(model.Desc == null)
				parms[4].Value =  DBNull.Value;
			else
				parms[4].Value = model.Desc;
			parms[5].Value = model.IsLock;	
			return parms;
		}


		/// <summary>
		/// 在dbo.UserInfo表中完整更新一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被更新数据的实体对象</param>
		/// <param name="trans">事务参数</param>
		/// <returns>影响行数</returns>
		public int Update(UserInfo model,SqlTransaction trans)
		{
		    const string sql = "UPDATE UserInfo SET UserName=@UserName, UserPwd=@UserPwd, RoleId=@RoleId, [Desc]=@Desc, IsLock=@IsLock WHERE 1=1  AND UserId=@UserId";
				
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
		/// 在dbo.UserInfo中删除一条记录,支持数据库事务
		/// </summary>
		/// <param name="id">主键</param>
		/// <param name="trans">事务参数</param>
		/// <returns>所影响的行数</returns>
		public int Delete(int id,SqlTransaction trans)
		{
		    const string sql ="DELETE FROM UserInfo  WHERE 1=1  AND UserId=@UserId";
			
		    SqlParameter[] parms = {						
							new SqlParameter("@UserId",SqlDbType.Int,4)
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
		/// 根据主键获取dbo.UserInfo中的一条记录
		/// </summary>
		/// <param name="id">主键id</param>
		/// <returns>实体模型</returns>
		public UserInfo  GetModel(int id)
		{  
			const string sql="SELECT * FROM UserInfo WHERE  UserId=@UserId ";		   
		    SqlParameter[] parms = {						
							new SqlParameter("@UserId",SqlDbType.Int,4)
		    };
		   
			parms[0].Value = id;
			UserInfo model = null;
			
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
        /// 装载dbo.UserInfo实体
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>返回对象</returns>
		private UserInfo LoadModel(IDataReader dr)
		{
			UserInfo model = new UserInfo();
            model.UserId = Convert.ToInt32(dr["UserId"]);
            model.UserName = dr["UserName"].ToString();
            model.UserPwd = dr["UserPwd"].ToString();
            model.RoleId = dr["RoleId"].ToString();
            if (dr["Desc"] != DBNull.Value)
                model.Desc = dr["Desc"].ToString();
            model.IsLock = Convert.ToInt32(dr["IsLock"]);
			return model;
		}


        /// <summary>
        /// 根据用户名和密码查询用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>用户信息</returns>
        public UserInfo GetModel(string userName, string pwd)
        {
            UserInfo model = null;
            const string sql = "select * from UserInfo where UserName=@UserName and UserPwd=@UserPwd";
            SqlParameter[] parms = {
                                        new SqlParameter("@UserName",SqlDbType.VarChar,50),
                                        new SqlParameter("@UserPwd",SqlDbType.VarChar,50)
                                    };
            parms[0].Value = userName;
            parms[1].Value = pwd;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, sql, parms))
            {
                if (dr.Read())
                {
                    model = new UserInfo();
                    model.UserId = dr.GetInt32(0);
                    model.UserName = dr["UserName"].ToString();
                    model.UserPwd = dr["UserPwd"].ToString();
                    model.RoleId = dr["RoleId"].ToString();
                    model.Desc = dr["Desc"].ToString();
                    model.IsLock = Convert.ToInt32(dr["IsLock"]);
                }
            }
            return model;
        }
		#endregion
	}
}
