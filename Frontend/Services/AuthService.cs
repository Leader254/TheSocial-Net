using Frontend.Models;
using Frontend.Models.Authentication;
using Frontend.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;


namespace Frontend.Services
{
    public class AuthService : IAuthInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7047";
        //private readonly ILocalStorageService _localStorageService;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserDto>> GetUserAll()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/User");
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.Success)
            {
                //change the object to a list
                return JsonConvert.DeserializeObject<List<UserDto>>(results.Data.ToString());
            }
            return new List<UserDto>();
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var request = JsonConvert.SerializeObject(loginRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/User/login", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(content);
            if(result.Success)
            {
                return JsonConvert.DeserializeObject<LoginResponseDto>(result.Data.ToString());
            }
            return new LoginResponseDto();
        }

        public async Task<ResponseDto> Register(RegisterUserDto registerUserDto)
        {
            var request = JsonConvert.SerializeObject(registerUserDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/User/register", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (result.Success)
            {
                return result;
            }
            return new ResponseDto();
        }
    }
}
