using Azure;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace exam_proctor_system.Services.Implementations
{
	public class AIAnalyzerService
	{
		public async Task<bool> DetectSuspiciousMovement(string frameBase64)
		{
			var faceClient = new FaceClient(
	new ApiKeyServiceClientCredentials("<AZURE_KEY>")
)
			{ Endpoint = "<AZURE_ENDPOINT>" };

			// Convert base64 to byte[]
			var bytes = Convert.FromBase64String(frameBase64.Split(',')[1]);
			using var stream = new MemoryStream(bytes);

			// Detect face with head pose attributes
			IList<DetectedFace> faces = await faceClient.Face.DetectWithStreamAsync(
				stream,
				returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.HeadPose },
				detectionModel: DetectionModel.Detection03,
				recognitionModel: RecognitionModel.Recognition04
			);

			if (faces.Count == 0) return false;

			var headPose = faces[0].FaceAttributes.HeadPose;
			// Check for head turning (adjust thresholds)
			if (Math.Abs(headPose.Yaw) > 20 || Math.Abs(headPose.Pitch) > 15)
				return true;

			return false;
		}
	}
}
