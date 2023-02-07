using testWork.Models;

namespace testWork.Controllers.DTO.Requests
{
    public class MessageRequest
    {
        public string Content { get; set; }
        public ContactRequest Contact { get; set; }
        public int themeId { get; set; }
    }
}
