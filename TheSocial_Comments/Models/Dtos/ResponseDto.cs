namespace TheSocial_Comments.Models.Dtos
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
        public object Data { get; set; }
    }
}
