

using System;

namespace Project.Model
{
	///========================================================================
	/// Project: 权限项目	
   	///========================================================================
	/// <summary>
	///Title: Article类
	///Description: dbo.Article数据表实体模型代码
	///@author xu
	///@version 1.0.0.0
	///@date 2012-9-21
	///@modify 
	///@date 
	/// </summary>
	public class Article
	{	
		private int _id;
		private int _typeId;
		private string _title = String.Empty;
		private string _author = String.Empty;
		private string _fromWhere = String.Empty;
		private string _zhaiYao = String.Empty;
		private string _imageUrl = String.Empty;
		private string _contentInfo = String.Empty;
		private int _isTop;
		private int _isCommed;
		private DateTime _addTime = DateTime.Now;
		private string _metaKeys = String.Empty;
		private string _metaDes = String.Empty;
		
		/// <summary>
		/// 主键
		/// </summary>
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// 所属类别
		/// </summary>
		public int TypeId
		{
			get { return _typeId; }
			set { _typeId = value; }
		}

		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		/// <summary>
		/// 作者
		/// </summary>
		public string Author
		{
			get { return _author; }
			set { _author = value; }
		}

		/// <summary>
		/// 来源
		/// </summary>
		public string FromWhere
		{
			get { return _fromWhere; }
			set { _fromWhere = value; }
		}

		/// <summary>
		/// 摘要
		/// </summary>
		public string ZhaiYao
		{
			get { return _zhaiYao; }
			set { _zhaiYao = value; }
		}

		/// <summary>
		/// 摘要图片
		/// </summary>
		public string ImageUrl
		{
			get { return _imageUrl; }
			set { _imageUrl = value; }
		}

		/// <summary>
		/// 内容
		/// </summary>
		public string ContentInfo
		{
			get { return _contentInfo; }
			set { _contentInfo = value; }
		}

		/// <summary>
		/// 是否置顶
		/// </summary>
		public int IsTop
		{
			get { return _isTop; }
			set { _isTop = value; }
		}

		/// <summary>
		/// 是否推荐
		/// </summary>
		public int IsCommed
		{
			get { return _isCommed; }
			set { _isCommed = value; }
		}

		/// <summary>
		/// 发布时间
		/// </summary>
		public DateTime AddTime
		{
			get { return _addTime; }
			set { _addTime = value; }
		}

		/// <summary>
		/// Meta关键字
		/// </summary>
		public string MetaKeys
		{
			get { return _metaKeys; }
			set { _metaKeys = value; }
		}

		/// <summary>
		/// Meta描述
		/// </summary>
		public string MetaDes
		{
			get { return _metaDes; }
			set { _metaDes = value; }
		}

	}
}
