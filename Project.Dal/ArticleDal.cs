
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
	///Title: Article类
	///Description: Article表SQLServer数据库操作实现代码
	///@author xu
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class ArticleDal
	{		
		#region 数据库基本操作方法
		

		/// <summary>
		/// 在dbo.Article中新增一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被插入数据的实体对象</param>
		/// <param name="trans">事务参数</param>
		/// <returns>影响行数</returns>
		public int Insert(Article model,SqlTransaction trans)
		{
			const string sql = "INSERT INTO Article (TypeId,Title,Author,FromWhere,ZhaiYao,ImageUrl,ContentInfo,IsTop,IsCommed,AddTime,MetaKeys,MetaDes) VALUES (@TypeId,@Title,@Author,@FromWhere,@ZhaiYao,@ImageUrl,@ContentInfo,@IsTop,@IsCommed,@AddTime,@MetaKeys,@MetaDes)";
			
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
		
		
		private SqlParameter[] GetParms(Article model)
		{
			SqlParameter[] parms = {
				new SqlParameter("@Id", SqlDbType.Int, 4),			
				new SqlParameter("@TypeId", SqlDbType.Int, 4),			
				new SqlParameter("@Title", SqlDbType.NVarChar, 40),			
				new SqlParameter("@Author", SqlDbType.NVarChar, 20),			
				new SqlParameter("@FromWhere", SqlDbType.NVarChar, 40),			
				new SqlParameter("@ZhaiYao", SqlDbType.NVarChar, 200),			
				new SqlParameter("@ImageUrl", SqlDbType.VarChar, 200),			
				new SqlParameter("@ContentInfo", SqlDbType.NVarChar, -1),			
				new SqlParameter("@IsTop", SqlDbType.Int, 4),			
				new SqlParameter("@IsCommed", SqlDbType.Int, 4),			
				new SqlParameter("@AddTime", SqlDbType.DateTime, 8),			
				new SqlParameter("@MetaKeys", SqlDbType.NVarChar, 100),			
				new SqlParameter("@MetaDes", SqlDbType.NVarChar, 250)			
				};
				
			parms[0].Value = model.Id;	
			parms[1].Value = model.TypeId;	
			parms[2].Value = model.Title;	
			parms[3].Value = model.Author;	
			parms[4].Value = model.FromWhere;	
			if(model.ZhaiYao == null)
				parms[5].Value =  DBNull.Value;
			else
				parms[5].Value = model.ZhaiYao;
			if(model.ImageUrl == null)
				parms[6].Value =  DBNull.Value;
			else
				parms[6].Value = model.ImageUrl;
			parms[7].Value = model.ContentInfo;	
			parms[8].Value = model.IsTop;	
			parms[9].Value = model.IsCommed;	
			parms[10].Value = model.AddTime;	
			if(model.MetaKeys == null)
				parms[11].Value =  DBNull.Value;
			else
				parms[11].Value = model.MetaKeys;
			if(model.MetaDes == null)
				parms[12].Value =  DBNull.Value;
			else
				parms[12].Value = model.MetaDes;
			return parms;
		}


		/// <summary>
		/// 在dbo.Article表中完整更新一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被更新数据的实体对象</param>
		/// <param name="trans">事务参数</param>
		/// <returns>影响行数</returns>
		public int Update(Article model,SqlTransaction trans)
		{
		    const string sql = "UPDATE Article SET TypeId=@TypeId, Title=@Title, Author=@Author, FromWhere=@FromWhere, ZhaiYao=@ZhaiYao, ImageUrl=@ImageUrl, ContentInfo=@ContentInfo, IsTop=@IsTop, IsCommed=@IsCommed, AddTime=@AddTime, MetaKeys=@MetaKeys, MetaDes=@MetaDes WHERE 1=1  AND Id=@Id";
				
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
		/// 在dbo.Article中删除一条记录,支持数据库事务
		/// </summary>
		/// <param name="id">主键</param>
		/// <param name="trans">事务参数</param>
		/// <returns>所影响的行数</returns>
		public int Delete(int id,SqlTransaction trans)
		{
		    const string sql ="DELETE FROM Article  WHERE 1=1  AND Id=@Id";
			
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
		/// 根据主键获取dbo.Article中的一条记录
		/// </summary>
		/// <param name="id">主键id</param>
		/// <returns>实体模型</returns>
		public Article  GetModel(int id)
		{  
			const string sql="SELECT * FROM Article WHERE  Id=@Id ";		   
		    SqlParameter[] parms = {						
							new SqlParameter("@Id",SqlDbType.Int,4)
		    };
		   
			parms[0].Value = id;
			Article model = null;
			
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
        /// 装载dbo.Article实体
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>返回对象</returns>
		private Article LoadModel(IDataReader dr)
		{
				Article model = new Article();
					model.Id	 = Convert.ToInt32(dr["Id"]);
					model.TypeId	 = Convert.ToInt32(dr["TypeId"]);
					model.Title	 = dr["Title"].ToString();
					model.Author	 = dr["Author"].ToString();
					model.FromWhere	 = dr["FromWhere"].ToString();
					if (dr["ZhaiYao"] != DBNull.Value)
						model.ZhaiYao	 = dr["ZhaiYao"].ToString();	
					if (dr["ImageUrl"] != DBNull.Value)
						model.ImageUrl	 = dr["ImageUrl"].ToString();	
					model.ContentInfo	 = dr["ContentInfo"].ToString();
					model.IsTop	 = Convert.ToInt32(dr["IsTop"]);
					model.IsCommed	 = Convert.ToInt32(dr["IsCommed"]);
					model.AddTime	 = Convert.ToDateTime(dr["AddTime"]);
					if (dr["MetaKeys"] != DBNull.Value)
						model.MetaKeys	 = dr["MetaKeys"].ToString();	
					if (dr["MetaDes"] != DBNull.Value)
						model.MetaDes	 = dr["MetaDes"].ToString();	
					return model;
		}
		#endregion
	}
}
