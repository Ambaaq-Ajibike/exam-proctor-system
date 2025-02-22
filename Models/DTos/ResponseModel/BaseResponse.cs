﻿namespace exam_proctor_system.Models.DTos.ResponseModel
{
	public class BaseResponse<T>
	{
		public string Message { get; set; }
		public bool IsSuccess { get; set; }
		public T Data { get; set; }
	}
}
