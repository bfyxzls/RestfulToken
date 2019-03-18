using System;
using System.Collections.Generic;
using System.Net;

namespace MvcApplication2.Utils
{
	/// <summary>
	/// api统一返回封装
	/// </summary>
	public class ReponseEntity<T> where T : class
	{
		/// <summary>
		/// 分页参数
		/// </summary>
		public class PageEntity
		{
			/// <summary>
			/// 总页数
			/// </summary>
			/// <value>The page count.</value>
			public int PageCount { get; set; }
			/// <summary>
			/// 一页的记录数
			/// </summary>
			/// <value>The size of the page.</value>
			public int PageSize { get; set; }
			/// <summary>
			/// 当前页码
			/// </summary>
			/// <value>The index of the page.</value>
			public int PageIndex { get; set; }
		}
		/// <summary>
		/// 分页参数
		/// </summary>
		/// <value>The page parameters.</value>
		public PageEntity PageParams { get; set; }
		/// <summary>
		/// 数据对象
		/// </summary>
		/// <value>The data.</value>
		public T Data { get; set; }
		/// <summary>
		/// 状态码
		/// </summary>
		/// <value>The code.</value>
		public HttpStatusCode Code { get; set; }
		/// <summary>
		/// 返回消息
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; set; }
		/// <summary>
		/// 错误列表
		/// </summary>
		/// <value>The errors.</value>
		public List<string> Errors { get; set; }

		/// <summary>
		/// 成功消息
		/// </summary>
		/// <returns>The ok.</returns>
		/// <param name="message">Message.</param>
		public ReponseEntity<T> Ok(string message)
		{
			return new ReponseEntity<T>
			{
				Message = message,
				Code = HttpStatusCode.OK
			};
		}

		/// <summary>
		/// 成功并返回数据
		/// </summary>
		/// <returns>The ok.</returns>
		/// <param name="data">Data.</param>
		public ReponseEntity<T> Ok(T data, PageEntity pageEntity = null)
		{
			return new ReponseEntity<T>
			{
				Message = "成功",
				Code = HttpStatusCode.OK,
				Data = data,
				PageParams = pageEntity
			};
		}

		/// <summary>
		/// 返回消息
		/// </summary>
		/// <returns>The ok.</returns>
		/// <param name="code">Code.</param>
		/// <param name="message">Message.</param>
		public ReponseEntity<T> Ok(HttpStatusCode code, string message)
		{
			return new ReponseEntity<T>
			{
				Message = message,
				Code = code,
				Data = null
			};
		}

		/// <summary>
		/// 错误返回
		/// </summary>
		/// <returns>The error.</returns>
		/// <param name="errors">Errors.</param>
		public ReponseEntity<T> Error(List<string> errors)
		{
			return new ReponseEntity<T>
			{
				Message = "操作失败",
				Code = HttpStatusCode.InternalServerError,
				Data = null,
				Errors = errors
			};
		}
	}
}
