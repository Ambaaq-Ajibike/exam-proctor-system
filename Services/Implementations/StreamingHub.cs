using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace exam_proctor_system.Services.Implementations
{
	public class StreamingHub : Hub
	{
		private static readonly Dictionary<string, string> CandidateConnections = new();
		private static readonly Dictionary<string, string> CandidateExams = new(); // Track candidate's exam

		public Task RegisterCandidate(string candidateId, string examId)
		{
			CandidateConnections[candidateId] = Context.ConnectionId;
			CandidateExams[candidateId] = examId;
			return Task.CompletedTask;
		}

		public async Task SendOffer(string candidateId, object offer)
		{
			// Send to admin group for this specific exam
			if (CandidateExams.TryGetValue(candidateId, out var examId))
			{
				await Clients.Group($"Admin-{examId}").SendAsync("ReceiveOffer", candidateId, offer);
			}
		}

		public async Task SendAnswer(string candidateId, object answer)
		{
			if (CandidateConnections.TryGetValue(candidateId, out var connectionId))
			{
				await Clients.Client(connectionId).SendAsync("ReceiveAnswer", answer);
			}
		}

		public async Task SendIceCandidate(string candidateId, object candidate)
		{
			if (CandidateExams.TryGetValue(candidateId, out var examId))
			{
				await Clients.Group($"Admin-{examId}").SendAsync("ReceiveIceCandidate", candidate, candidateId);
			}
		}

		public async Task JoinExamAdminGroup(string examId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, $"Admin-{examId}");
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			// Clean up when a candidate disconnects
			var candidateId = CandidateConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
			if (candidateId != null)
			{
				CandidateConnections.Remove(candidateId);
				CandidateExams.Remove(candidateId);
			}

			await base.OnDisconnectedAsync(exception);
		}
	}
}