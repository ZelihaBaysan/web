using AnimeTracker.Application.DTOs.Auth;
using AnimeTracker.Application.Wrappers.Results;

namespace AnimeTracker.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDto registerDto);
        Task<IDataResult<TokenDto>> LoginAsync(LoginDto loginDto);
    }
}