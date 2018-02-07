
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
	///Title: ArticleType类
	///Description: ArticleType表SQLServer数据库操作实现代码
	///@author daiwen
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class ArticleTypeDal
	{		
		#region 数据库基本操作方法
		

		/// <summary>
		/// 在dbo.ArticleType中新增一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被插入数据的实体对象</param>
		/// <param name="trans">事务参数</param>
		/// <returns>影响行数</returns>
		public int Insert(ArticleType model,SqlTransaction trans)
		{
			const string sql = "INSERT INTO ArticleType (TypeName,ParentId,OrderNumber) VALUES (@TypeName,@ParentId,@OrderNumber)";
			
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
		
		
		private SqlParameter[] GetParms(ArticleType model)
		{
			SqlParameter[] parms = {
				new SqlParameter("@Id", SqlDbType.Int, 4),			
				new SqlParameter("@TypeName", SqlDbType.VarChar, 40),			
				new SqlParameter("@ParentId", SqlDbType.Int, 4),			
				new SqlParameter("@OrderNumber", SqlDbType.Int, 4)			
				};
				
			parms[0].Value = model.Id;	
			parms[1].Value = model.TypeName;	
			parms[2].Value = model.ParentId;	
			parms[3].Value = model.OrderNumber;	
			return parms;
		}


		/// <summary>
		/// 在dbo.ArticleType表中完整更新一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被更新数据的实体对象</param>
		/// <param name="trans">事务参数</param>
		/// <returns>影响行数</returns>
		public int Update(ArticleType model,SqlTransaction trans)
		{
		    const string sql = "UPDATE ArticleType SET TypeName=@TypeName, ParentId=@ParentId, OrderNumber=@OrderNumber WHERE 1=1  AND Id=@Id";
				
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
		/// 在dbo.ArticleType中删除一条记录,支持数据库事务
		/// </summary>
		/// <param name="id">主键</param>
		/// <param name="trans">事务参数</param>
		/// <returns>所影响的行数</returns>
		public int Delete(int id,SqlTransaction trans)
		{
		    const string sql ="DELETE FROM ArticleType  WHERE 1=1  AND Id=@Id";
			
		    SqlParameter[] parms = {						
							new SqlParameter("@Id",SqlDbType.Int,4)
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
		/// 根据主键获取dbo.ArticleType中的一条记录
		/// </summary>
		/// <param name="id">主键id</param>
		/// <returns>实体模型</returns>
		public ArticleType  GetModel(int id)
		{  
			const string sql="SELECT * FROM ArticleType WHERE  Id=@Id ";		   
		    SqlParameter[] parms = {						
							new SqlParameter("@Id",SqlDbType.Int,4)
		    };
		   
			parms[0].Value = id;
			ArticleType model = null;
			
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
        /// 装载dbo.ArticleType实体
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>返回对象</returns>
		private ArticleType LoadModel(IDataReader dr)
		{
				ArticleType model = new ArticleType();
					model.Id	 = Convert.ToInt32(dr["Id"]);
					model.TypeName	 = dr["TypeName"].ToString();
					model.ParentId	 = Convert.ToInt32(dr["ParentId"]);
					model.OrderNumber	 = Convert.ToInt32(dr["OrderNumber"]);
					return model;
		}
		#endregion
	}
}
