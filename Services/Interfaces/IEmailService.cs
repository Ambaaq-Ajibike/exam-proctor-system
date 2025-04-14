using exam_proctor_system.Models.Entities;

namespace exam_proctor_system.Services.Interfaces
{
	public interface IEmailService
	{
		Task SendEmailAsync(
			string toEmail,
			string toName,
			string subject,
			string htmlContent);
		string BuildVerificationPasscodeEmail(string passcode, string recipientName, List<Exam> upcomingExams);
	}
}
