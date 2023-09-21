using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheSocial_Auth.Context;
using TheSocial_Auth.Models;
using TheSocial_Auth.Models.Dtos;
using TheSocial_Auth.Services.IService;

namespace TheSocial_Auth.Services
{
    public class UserService : IUserInterface
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;

        //constructor
        public UserService(UserManager<ApplicationUser> userManager, AppDbContext context, RoleManager<IdentityRole> roleManager, ITokenGenerator tokenGenerator, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }
        public async Task<bool> AssignRole(string email, string role)
        {
            //get user by email
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
            //check if user is null
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
                //assign role to user
                await _userManager.AddToRoleAsync(user, role);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _context.ApplicationUsers.ToListAsync();
        }

        public async Task<LoginResponseDto> LoginUser(LoginRequestDto loginRequestDto)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());
            //check valid password
            var result = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (!result || user == null)
            {
                new LoginResponseDto();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenGenerator.GenerateToken(user, roles);

            var loggedUser = new LoginResponseDto()
            {
                User = _mapper.Map<UserDto>(user),
                Token = token
            };
            return loggedUser;
        }

        public async Task<string> RegisterUser(RegisterUserDto registerUserDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerUserDto);
            try
            {
                var result = await _userManager.CreateAsync(user, registerUserDto.Password);
                if (result.Succeeded)
                {
                    //return nothing if success
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
