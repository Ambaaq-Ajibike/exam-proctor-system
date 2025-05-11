namespace exam_proctor_system.Models.DTos.ResponseModel
{
	public class DashboardDto
	{
		public List<Metric> Metrics { get; set; } = [];
	}
	public class Metric
	{
		public string Key { get; set; }
		public int Value { get; set; }
	}
}
