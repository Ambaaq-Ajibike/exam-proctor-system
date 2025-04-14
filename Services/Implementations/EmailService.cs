using brevo_csharp.Api;
using brevo_csharp.Client;
using brevo_csharp.Model;
using exam_proctor_system.Models.Entities;
using exam_proctor_system.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace exam_proctor_system.Services.Implementations
{
	public class EmailService : IEmailService
	{
		private readonly string _apiKey;
		private readonly IConfiguration _configuration;
		private readonly TransactionalEmailsApi _apiInstance;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
			_apiKey = _configuration["EmailSettings:apikey"] ?? "";
            if (Configuration.Default.ApiKey.ContainsKey("api-key"))
            {
                Configuration.Default.ApiKey["api-key"] = _apiKey;
			}
            else
            {
				Configuration.Default.ApiKey.Add("api-key", _apiKey);
			}
			_apiInstance = new TransactionalEmailsApi();
		}

		public async Task SendEmailAsync(
			string toEmail,
			string toName,
			string subject,
			string htmlContent)
		{
			try
			{
				var sender = new SendSmtpEmailSender(_configuration["EmailSettings:SenderDisplayName"], _configuration["EmailSettings:SenderEmail"]);
				var to = new SendSmtpEmailTo(toEmail, toName);
				var toList = new List<SendSmtpEmailTo> { to };

				var sendSmtpEmail = new SendSmtpEmail(
					sender: sender,
					to: toList,
					htmlContent: htmlContent,
					subject: subject);

				var result = await _apiInstance.SendTransacEmailAsync(sendSmtpEmail);
				Console.WriteLine("Email sent successfully. Message ID: " + result.MessageId);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error sending email: " + e.Message);
				throw;
			}
		}

		public string BuildVerificationPasscodeEmail(string passcode, string recipientName, List<Exam> upcomingExams)
		{
			var examsHtml = upcomingExams.Any()
				? string.Join("", upcomingExams.Select(exam =>
					$@"<div style='margin-bottom: 15px;'>
                <p style='margin: 5px 0; font-weight: 600;'>{exam.Name}</p>
                <p style='margin: 5px 0; color: #555;'>
                    Date: {exam.StartTime:MMMM dd, yyyy} | 
                    Duration: {exam.Duration}
                </p>
            </div>"))
				: "<p>No upcoming exams scheduled</p>";

			return $@"
<!DOCTYPE html>
<html>
<head>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
    <style>
        body {{
            font-family: 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
            line-height: 1.6;
            color: #333333;
            margin: 0;
            padding: 0;
            background-color: #f5f7fa;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
        }}
        .header {{
            background-color: #0f172a;
            padding: 30px 20px;
            text-align: center;
            border-radius: 8px 8px 0 0;
        }}
        .header h1 {{
            color: white;
            margin: 0;
            font-size: 24px;
        }}
        .content {{
            background-color: white;
            padding: 30px;
            border-radius: 0 0 8px 8px;
            box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
        }}
        .passcode {{
            font-size: 32px;
            font-weight: bold;
            letter-spacing: 2px;
            color: #583bbf;
            text-align: center;
            margin: 25px 0;
            padding: 15px 0;
            background-color: #f8f5ff;
            border-radius: 6px;
        }}
        .exam-section {{
            background-color: #f0f4f8;
            padding: 15px;
            border-radius: 6px;
            margin: 20px 0;
        }}
        .footer {{
            text-align: center;
            margin-top: 30px;
            color: #666666;
            font-size: 14px;
        }}
        .role-badge {{
            display: inline-block;
            padding: 4px 8px;
            background-color: #e0e7ff;
            color: #583bbf;
            border-radius: 4px;
            font-size: 12px;
            font-weight: 600;
            margin-left: 8px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Verification Passcode</h1>
        </div>
        <div class='content'>
            <p>Dear {recipientName}</p>
            
            <p>Your verification passcode is:</p>
            
            <div class='passcode'>{passcode}</div>
            
            <p>This code will expire in 15 minutes. Please use it to authenticate your access.</p>
            
            {(upcomingExams.Any() ? $@"
            <div class='exam-section'>
                <h3 style='margin-top: 0;'>Your Upcoming Exams:</h3>
                {examsHtml}
            </div>" : "")}
            
            <p><strong>Important:</strong></p>
            <ul>
                <li>Never share this code with anyone</li>
                <li>The code is valid for one-time use only</li>
                <li>If you didn't request this, please contact support immediately</li>
            </ul>
            
            <div class='footer'>
                <p>Best regards,<br>Exam Management System</p>
                <p style='margin-top: 20px; font-size: 12px; color: #999999;'>
                    © {DateTime.Now.Year} Academic Services Platform. All rights reserved.
                </p>
            </div>
        </div>
    </div>
</body>
</html>";
		}
	}
}
