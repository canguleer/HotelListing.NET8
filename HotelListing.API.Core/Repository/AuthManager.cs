using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelListing.API.Data.Entities;

namespace HotelListing.API.Core.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthManager> _logger;
        private User _user = null!;

        private const string LoginProvider = "HotelListingApi";
        private const string RefreshToken = "RefreshToken";

        public AuthManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration, ILogger<AuthManager> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, LoginProvider, RefreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, LoginProvider, RefreshToken);
            await _userManager.SetAuthenticationTokenAsync(_user, LoginProvider, RefreshToken, newRefreshToken);
            return newRefreshToken;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            _logger.LogInformation($"Looking for user with email {loginDto.Email}");
            _user = (await _userManager.FindByEmailAsync(loginDto.Email))!;
            var isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

            if (isValidUser == false)
            {
                _logger.LogWarning($"User with email {loginDto.Email} was not found");
                return new AuthResponseDto();
            }

            var token = await GenerateToken();
            _logger.LogInformation($"Token generated for user with email {loginDto.Email} | Token: {token}");

            return new AuthResponseDto
            {
                Token = token,
                UserId = $"{_user.Id}",
                RefreshToken = await CreateRefreshToken()
            };
        }

        public async Task<IEnumerable<IdentityError>> Register(UserDto userDto)
        {
            _user = _mapper.Map<User>(userDto);
            _user.UserName = userDto.Email;
            _user.LastChanged=DateTime.UtcNow;
            _user.LastChangedBy="ZZZ";

            var result = await _userManager.CreateAsync(_user, userDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(_user, nameof(Role.RoleName.User));
            
            return result.Errors;
        }

        public async Task<AuthResponseDto?> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = (await _userManager.FindByNameAsync(username!))!;

            if ($"{_user.Id}" != request.UserId)
                return null;

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, LoginProvider, RefreshToken, request.RefreshToken!);

            if (isValidRefreshToken)
            {
                return new AuthResponseDto
                {
                    Token = await GenerateToken(),
                    UserId = $"{_user.Id}",
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);

            return new AuthResponseDto();
        }

        private async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, _user.Email!),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Email, _user.Email!),
                new ("uid", $"{_user.Id}"),
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            var data = new JwtSecurityTokenHandler().WriteToken(token);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
