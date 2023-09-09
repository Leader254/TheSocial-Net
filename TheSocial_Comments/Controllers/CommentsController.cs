using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        // create comment
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateComment(CommentRequestDto commentRequestDto)
        {
            var newComment = _mapper.Map<Comment>(commentRequestDto);
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteComment(Guid commentId)
        {
            var comment = await _commentInterface.GetCommentByIdAsync(commentId);
            if (comment != null)
            {
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> UpdateComment(Guid id, CommentRequestDto commentRequestDto)
        {
            var comment = await _commentInterface.GetCommentByIdAsync(id);
            if (comment == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Comment not found";
                return BadRequest(_response);
            }
            var updatedComment = _mapper.Map(commentRequestDto, comment);
            var response = await _commentInterface.UpdateCommentAsync(updatedComment);
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
        // get all comments by post id
        [HttpGet("GetAllCommentsByPostId/{id}")]
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
    }
}
