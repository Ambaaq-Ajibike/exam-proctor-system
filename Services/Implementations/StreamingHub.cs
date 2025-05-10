using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace exam_proctor_system.Services.Implementations
{
	public class StreamingHub : Hub
	{
		private static readonly Dictionary<string, string> CandidateIdToConnection = new();
		public Task RegisterCandidate(string candidateId)
		{
			CandidateIdToConnection[candidateId] = Context.ConnectionId;
			return Task.CompletedTask;
		}
		public async Task SendOffer(string candidateId, object offer)
		{
			await Clients.Group("Admin").SendAsync("ReceiveOffer", candidateId, offer);
		}

		public async Task SendAnswer(string candidateId, object answer)
		{
			if (CandidateIdToConnection.TryGetValue(candidateId, out var connectionId))
			{
				await Clients.Client(connectionId).SendAsync("ReceiveAnswer", answer);
			}
			await Clients.Client(candidateId).SendAsync("ReceiveAnswer", answer);
		}

		public async Task SendIceCandidate(string candidateId, object candidate)
		{
			await Clients.Group("Admin").SendAsync("ReceiveIceCandidate", candidate, candidateId);
		}

		public override async Task OnConnectedAsync()
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
			await base.OnConnectedAsync();
		}
	}
}