using testWork.Models;

namespace testWork.Controllers.DTO.Requests
{
    public class MessageRequest
    {
        public string content { get; set; }
        public ContactRequest contact { get; set; }
        public ThemeRequest theme { get; set; }
    }
}
