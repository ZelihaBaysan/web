using AnimeTracker.Application.DTOs.Auth;
using AnimeTracker.Application.Interfaces.Services;
using AnimeTracker.Application.Wrappers.Results;
using AnimeTracker.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AnimeTracker.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IResult> RegisterAsync(RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
                return new ErrorResult("Şifreler birbiriyle uyuşmuyor.");

            var user = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                CreatedDate = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
                return new SuccessResult("Kullanıcı kaydı başarıyla oluşturuldu.");

            var errors = string.Join(", ", result.Errors.Select(x => x.Description));
            return new ErrorResult($"Kayıt başarısız: {errors}");
        }

        public async Task<IDataResult<TokenDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return new ErrorDataResult<TokenDto>("Kullanıcı bulunamadı.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return new ErrorDataResult<TokenDto>("E-posta veya şifre hatalı.");

            // JWT Token Üretimi
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!)
                }),
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"])),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenDto = new TokenDto
            {
                AccessToken = tokenHandler.WriteToken(token),
                Expiration = tokenDescriptor.Expires.Value
            };

            return new SuccessDataResult<TokenDto>(tokenDto, "Giriş başarılı.");
        }
    }
}