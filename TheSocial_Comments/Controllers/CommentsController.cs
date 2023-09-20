using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TheSocial_Comments.Models;
using TheSocial_Comments.Models.Dtos;
using TheSocial_Comments.Services.IServices;

namespace TheSocial_Comments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsInterface _commentInterface;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;

        public CommentsController(ICommentsInterface commentInterface, IMapper mapper)
        {
            _commentInterface = commentInterface;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        private string GetUserIdFromToken()
        {
            var token = Request.Headers["Authorization"].ToString();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token.Split(" ")[1]);
            return decodedToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        }
        // create comment
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> CreateComment(CommentRequestDto commentRequestDto)
        {
            //get user id from token
            var userId = GetUserIdFromToken();
            var newComment = _mapper.Map<Comment>(commentRequestDto);
            newComment.UserId = Guid.Parse(userId);
            var response = await _commentInterface.CreateCommentAsync(newComment);
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

        // get all comments
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<ResponseDto>> GetAllComments()
        {
            var comments = await _commentInterface.GetAllCommentsAsync();
            if (comments != null)
            {
                _response.Message = "";
                _response.IsSuccess = true;
                _response.Data = comments;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "Something went wrong";
            return BadRequest(_response);
        }

        // get comment by id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetCommentById(Guid id)
        {
            var comment = await _commentInterface.GetCommentByIdAsync(id);
            if (comment != null)
            {
                _response.IsSuccess = true;
                _response.Message = "";
                _response.Data = comment;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "Something went wrong";
            return BadRequest(_response);
        }

        [HttpDelete("commentId")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> DeleteComment(Guid commentId)
        {
            var comment = await _commentInterface.GetCommentByIdAsync(commentId);
            var userId = GetUserIdFromToken();
            if (comment != null)
            {
                if (comment.UserId.ToString() != userId)
                {
                    _response.IsSuccess = false;
                    _response.Message = "You are not authorized to delete this comment";
                    return BadRequest(_response);
                }
                var response = await _commentInterface.DeleteCommentAsync(comment);
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
            _response.Message = "Comment not found";
            return BadRequest(_response);
        }


        // update comment by id
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> UpdateComment(Guid id, CommentRequestDto commentRequestDto)
        {
            var comment = await _commentInterface.GetCommentByIdAsync(id);
            var userId = GetUserIdFromToken();
            //check if the user is the owner of the comment
            if (comment != null)
            {
                if (comment.UserId.ToString() != userId)
                {
                    _response.IsSuccess = false;
                    _response.Message = "You are not authorized to update this comment";
                    return BadRequest(_response);
                }
                //update comment
                var commentUpdate = _mapper.Map(commentRequestDto, comment);
                var response = await _commentInterface.UpdateCommentAsync(commentUpdate);
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
            _response.IsSuccess = false;
            _response.Message = "Comment not found";
            return BadRequest(_response);
        }
        // get all comments by post id
        [HttpGet("GetAllCommentsByPostId/{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetAllCommentsByPostId(Guid id)
        {
            var comments = await _commentInterface.GetCommentsByPostIdAsync(id);
            if (comments != null)
            {
                _response.Message = "";
                _response.IsSuccess = true;
                _response.Data = comments;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "Something went wrong";
            return BadRequest(_response);
        }
        //get all comments by user id
        [HttpGet("GetAllCommentsByUserId/{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetAllCommentsByUserId(Guid id)
        {
            var comments = await _commentInterface.GetCommentsByUserIdAsync(id);
            if (comments != null)
            {
                _response.Message = "";
                _response.IsSuccess = true;
                _response.Data = comments;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "Something went wrong";
            return BadRequest(_response);
        }
    }
}
