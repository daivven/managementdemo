

using System;

namespace Project.Model
{
	///========================================================================
	/// Project: 权限项目	
   	///========================================================================
	/// <summary>
	///Title: ArticleType类
	///Description: dbo.ArticleType数据表实体模型代码
	///@author daiwen
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class ArticleType
	{	
		private int _id;
		private string _typeName = String.Empty;
		private int _parentId;
		private int _orderNumber;
		
		/// <summary>
		/// 主键
		/// </summary>
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// 类别名称
		/// </summary>
		public string TypeName
		{
			get { return _typeName; }
			set { _typeName = value; }
		}

		/// <summary>
		/// 父类Id
		/// </summary>
		public int ParentId
		{
			get { return _parentId; }
			set { _parentId = value; }
		}

		/// <summary>
		/// 排序号
		/// </summary>
		public int OrderNumber
		{
			get { return _orderNumber; }
			set { _orderNumber = value; }
		}

	}
}
