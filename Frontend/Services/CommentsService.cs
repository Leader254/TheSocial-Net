using Frontend.Models;
using Frontend.Models.Comments;
using Frontend.Services.Interfaces;
using Newtonsoft.Json;

namespace Frontend.Services
{
    public class CommentsService : ICommentInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7216";

        public CommentsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<string> CreateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> GetAllComments()
        {
            var comments = await _httpClient.GetAsync($"{_baseUrl}/api/Comments");
            var content = await comments.Content.ReadAsStringAsync();
            var results  = JsonConvert.DeserializeObject<ResponseDto>(content);
            if(results.Success)
            {
                return JsonConvert.DeserializeObject<List<Comment>>(results.Data.ToString());
            }
            return new List<Comment>();
        }
    }
}
