using Frontend.Models;
using Frontend.Models.Posts;
using Frontend.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Frontend.Services
{
    public class PostService : IPostInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7050";
        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseDto> AddPostAsync(PostRequestDto newPost)
        {
            var request = JsonConvert.SerializeObject(newPost);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/Post", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (result.Success)
            {
                return result;
            }
            return new ResponseDto();
        }

        public async Task<ResponseDto> DeletePostAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Post?id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (result.Success)
            {
                return result;
            }
            return new ResponseDto();
        }

        public async Task<List<PostDto>> GetAllPostsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Post");
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.Success)
            {
                //change the object to a list
                return JsonConvert.DeserializeObject<List<PostDto>>(results.Data.ToString());
            }
            return new List<PostDto>();
        }



        public async Task<PostDto> GetPostById(Guid Postid)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Post/{Postid}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (result.Success)
            {
                return JsonConvert.DeserializeObject<PostDto>(result.Data.ToString());
            }
            return new PostDto();
        }

        public Task<IEnumerable<PostDto>> GetUserPosts(Guid UserId)
        {
            throw new NotImplementedException();
        }


        public async Task<ResponseDto> UpdatePostAsync(Guid id, PostRequestDto UpdatePost)
        {
            var request = JsonConvert.SerializeObject(UpdatePost);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/api/Post?id={id}", bodyContent);
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
