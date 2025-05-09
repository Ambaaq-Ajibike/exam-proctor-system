using Microsoft.AspNetCore.SignalR;

namespace exam_proctor_system.Services.Implementations
{
    public class StreamingHub : Hub
    {
        // Receiving an offer from a candidate
        public Task SendOffer(string candidateId, RTCSessionDescriptionInitDto offer)
            => Clients.Group("Admin")
                      .SendAsync("ReceiveOffer", candidateId, offer);

        // Receiving an answer back from admin (if you use a distinct DTO)
        public Task SendAnswer(string candidateId, RTCSessionDescriptionAnswerDto answer)
            => Clients.Client(candidateId)
                      .SendAsync("ReceiveAnswer", answer);

        // Receiving ICE candidates
        public Task SendIceCandidate(string candidateId, RTCIceCandidateInitDto candidate)
            => Clients.Group("Admin")
                      .SendAsync("ReceiveIceCandidate", candidateId, candidate);
        public override Task OnConnectedAsync()
        {
            //if (Context.User.IsInRole("Admin"))
                Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
            return base.OnConnectedAsync();
        }
    }
    /// <summary>
    /// Mirrors the browser’s RTCSessionDescriptionInit dictionary.
    /// Used to send SDP offers/answers via SignalR.
    /// </summary>
    public class RTCSessionDescriptionInitDto
    {
        /// <summary>
        /// The type of the SDP, either "offer" or "answer".
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Session Description Protocol (SDP) blob.
        /// </summary>
        public string Sdp { get; set; }
    }

    /// <summary>
    /// Identical shape to RTCSessionDescriptionInit —
    /// useful if you want a semantically distinct class for answers.
    /// </summary>
    public class RTCSessionDescriptionAnswerDto
    {
        public string Type { get; set; }
        public string Sdp { get; set; }
    }

    /// <summary>
    /// Mirrors the browser’s RTCIceCandidateInit dictionary.
    /// Used to send ICE candidates via SignalR.
    /// </summary>
    public class RTCIceCandidateInitDto
    {
        /// <summary>
        /// The candidate-attribute returned by the ICE server.
        /// </summary>
        public string Candidate { get; set; }

        /// <summary>
        /// The value of the “sdpMid” in the SDP, or null.
        /// </summary>
        public string SdpMid { get; set; }

        /// <summary>
        /// The value of the “sdpMLineIndex” in the SDP, or null.
        /// </summary>
        public int? SdpMLineIndex { get; set; }
    }


}
