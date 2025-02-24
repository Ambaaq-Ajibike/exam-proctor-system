﻿using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Models.DTos.ResponseModel;

namespace exam_proctor_system.Services.Interfaces
{
	public interface IResultService
	{
		Task<ResultResponse> CalculateResult(GradeRequest gradeRequest);
	}
}
