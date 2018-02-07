
using System;
using System.Collections.Generic;
using Project.Model;
using Project.Dal;
using System.Data.SqlClient;
using System.Data;
namespace Project.Bll
{
	///========================================================================
	/// Project: 权限系统	
	/// Copyright: Copyright (c) 2008
	/// Company: 
   	///========================================================================
	
	///Title: UserInfo类
	///Description: UserInfo业务逻辑操作类
    ///@author daiwen
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class UserInfoBll
	{
		private readonly UserInfoDal dal = new UserInfoDal();
		
		#region 基本方法
		
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
            return dal.GetList(pageIndex, pageSize, whereSql, out recordCount);
        }


		/// <summary>
		/// 在dbo.UserInfo中新增一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被插入数据的实体对象</param>
		/// <param name="trans">事务参数，没有请填null</param>
		/// <returns>影响行数</returns>
		public int Insert(UserInfo model,SqlTransaction trans)
		{
            return dal.Insert(model, trans);
		}
		
		/// <summary>
		/// 在dbo.UserInfo表中完整更新一条记录,支持数据库事务
		/// </summary>
		/// <param name="model">包含被更新数据的实体对象</param>
		/// <param name="trans">事务参数，没有请填null</param>
		/// <returns>影响行数</returns>
		public int Update(UserInfo model,SqlTransaction trans)
		{
			return dal.Update(model,trans);	
		}
		
		/// <summary>
		/// 在dbo.UserInfo中删除一条记录,支持数据库事务
		/// </summary>
		/// <param name="id">主键</param>
		/// <param name="trans">事务参数，没有请填null</param>
		/// <returns>所影响的行数</returns>
		public int Delete(int id,SqlTransaction trans)
		{
			return dal.Delete(id,trans);	
		}
		
		
		/// <summary>
		/// 根据主键获取dbo.UserInfo中的一条记录
		/// </summary>
		/// <param name="id">主键id</param>
		/// <returns>实体模型</returns>
		public UserInfo  GetModel(int id)
		{
			return dal.GetModel(id);
		}


        /// <summary>
        /// 根据用户名和密码查询用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>用户信息</returns>
        public UserInfo GetModel(string userName, string pwd)
        {
            return dal.GetModel(userName, pwd);
        }

		#endregion
	}
}
