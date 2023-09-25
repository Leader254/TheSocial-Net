using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSocial_Post.Models;
using TheSocial_Post.Models.Dtos;
using TheSocial_Post.Services.IService;

namespace TheSocial_Post.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        private readonly IMapper _mapper;
        private readonly ResponseDto _response;

        // dependency injection
        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
            _response = new ResponseDto();
        }
        private string GetUserIdFromToken()
        {
            var token = Request.Headers["Authorization"].ToString();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token.Split(" ")[1]);
            return decodedToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreatePost(PostRequestDto postRequestDto)
        {
            var newPost = _mapper.Map<Post>(postRequestDto);
            var userId = GetUserIdFromToken();
            newPost.UserId = Guid.Parse(userId);
            var response = await _postService.CreatePostAsync(newPost);
            if (response != null)
            {
                _response.IsSuccess = true;
                _response.Message = response;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "Something went wrong";
            return BadRequest(_response);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            if (posts != null)
            {
                _response.Message = "";
                _response.IsSuccess = true;
                _response.Data = posts;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "Something went wrong";
            return BadRequest(_response);
        }
        // get post by id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetPostById(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post != null)
            {
                _response.Message = "";
                _response.IsSuccess = true;
                _response.Data = post;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "Something went wrong";
            return BadRequest(_response);
        }
        // delete post and authorize the owner of the post
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> DeletePost(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            var userId = GetUserIdFromToken();
            if (post != null)
            {
                if (post.UserId.ToString() != userId)
                {
                    _response.IsSuccess = false;
                    _response.Message = "You are not authorized to delete this post";
                    return BadRequest(_response);
                }
                var response = await _postService.DeletePostAsync(post);
                if (response != null)
                {
                    _response.IsSuccess = true;
                    _response.Message = "Successfully deleted";
                    return Ok(_response);
                }
                _response.IsSuccess = false;
                _response.Message = "Something went wrong";
                return BadRequest(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "Post not found";
            return BadRequest(_response);

        }

        //get all posts by user id
        [HttpGet("userId")]
        public async Task<ActionResult<ResponseDto>> GetAllPostsByUserId(Guid userId)
        {
            var posts = await _postService.GetPostsByUserIdAsync(userId);
            if (posts.Count() > 0)
            {
                _response.Message = "";
                _response.IsSuccess = true;
                _response.Data = posts;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "The user does not have any posts yet or does not exist!!";
            return BadRequest(_response);
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> UpdatePost(Guid id, PostRequestDto postRequestDto)
        {
            var post = await _postService.GetPostByIdAsync(id);
            var userId = GetUserIdFromToken();
            //check if the post exists and the UserId in the token matches the post's UserId
            if (post != null)
            {
                if (post.UserId.ToString() != userId)
                {
                    _response.IsSuccess = false;
                    _response.Message = "You are not authorized to update this post";
                    return BadRequest(_response);
                }
                //update post
                var postToUpdate = _mapper.Map(postRequestDto, post);
                var response = await _postService.UpdatePostAsync(postToUpdate);
                if (response != null)
                {
                    _response.IsSuccess = true;
                    _response.Message = "Successfully updated";
                    return Ok(_response);
                }
                _response.IsSuccess = false;
                _response.Message = "Something went wrong";
                return BadRequest(_response);
            }
            //if not authorized return an error
            _response.IsSuccess = false;
            _response.Message = "Post not found";
            return BadRequest(_response);
        }

    }
}