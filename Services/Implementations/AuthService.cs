using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Models.DTos.ResponseModel;
using exam_proctor_system.Models.Entities;
using exam_proctor_system.Repositories;
using exam_proctor_system.Services.Interfaces;

namespace exam_proctor_system.Services.Implementations
{
	public class AuthService(IRepository<User> _userRepository) : IAuthService
	{
		public async Task<BaseResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest)
		{
			var user = await _userRepository.FindAsync(x => x.Email == loginRequest.Username && x.Password == loginRequest.Password);
			if (user == null)
			{
				return new BaseResponse<LoginResponse>
				{
					IsSuccess = false,
					Message = "Invalid username or password"
				};
			}
			return new BaseResponse<LoginResponse>
			{
				IsSuccess = true,
				Data = new LoginResponse
				{
					Username = user.Email,
					Role = user.Role
				}
			};
		}
	}
}
