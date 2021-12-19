using AutoMapper;
using System.Security.Cryptography;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Models;
using DataAccessLayer;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUoWFactory uowFactory, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = uowFactory.CreateUoW();
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<UserInfoDTO>> GetUsersInfoAsync()
        {
            var usersInfo = await _unitOfWork.User.GetAllAsync();

            return _mapper.Map<List<UserInfoDTO>>(usersInfo);
        }

        public async Task<UserInfoDTO> GetUserInfoByIdAsync(int id)
        {
            var userInfo = await _unitOfWork.User.GetByIdAsync(id);

            return _mapper.Map<UserInfoDTO>(userInfo);
        }

        public async Task<LoginRes?> AuthenticateAsync(LoginReq request)
        {
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null) return null;
            
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            var token = GenerateJsonWebToken(user);

            return new LoginRes(user, token); ;
        }

        public async Task<LoginRes?> RegisterAsync(UserDTO userModel)
        {
            CreatePasswordHash(userModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Id = userModel.Id,
                Email = userModel.Email,
                Name = userModel.Name, 
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return await AuthenticateAsync(new LoginReq
                         {
                             Email = userModel.Email,
                             Password = userModel.Password
                         });
        }

        private string GenerateJsonWebToken(User user)
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.Email) };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("Secret").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                            claims: claims,
                            signingCredentials: credentials,
                            expires: DateTime.Now.AddDays(1));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();

            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);

            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
