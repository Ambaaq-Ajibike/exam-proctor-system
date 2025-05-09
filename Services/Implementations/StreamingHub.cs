using Microsoft.AspNetCore.SignalR;

namespace exam_proctor_system.Services.Implementations
{
	public class StreamingHub : Hub
	{
		public async Task SendOffer(string adminId, string offer)
		{
			await Clients.Client(adminId).SendAsync("ReceiveOffer", Context.ConnectionId, offer);
		}

		public async Task SendAnswer(string candidateId, string answer)
		{
			await Clients.Client(candidateId).SendAsync("ReceiveAnswer", answer);
		}

		public async Task SendCandidate(string toConnectionId, string candidate)
		{
			await Clients.Client(toConnectionId).SendAsync("ReceiveCandidate", candidate);
		}

		public override Task OnConnectedAsync()
		{
			// Optional: track role by query string and map connection IDs
			return base.OnConnectedAsync();
		}
	}

}
