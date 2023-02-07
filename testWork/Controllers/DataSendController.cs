using Microsoft.AspNetCore.Mvc;
using testWork.Controllers.DTO.Requests;
using testWork.Controllers.DTO.Responses;
using testWork.Models;
using testWork.Services;

namespace testWork.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    
    public class DataSendController : Controller
    {
        ThemeService themeService = new ThemeService();
        ContactService contactService = new ContactService();
        MessageService messageService = new MessageService();

        [HttpGet]
        [Route("themes")]
        public List<ThemeResponse> getThemes()
        {
            return themeService.getThemeList();
        }

        [HttpPost]
        [Route("themes")]
        public void createTheme([FromBody] ThemeRequest themeRequest)
        {
            themeService.createTheme(themeRequest);
        }

        [HttpPost]
        [Route("contacts")]
        public void createContact([FromBody] ContactRequest contactRequest)
        {
            contactService.createContact(contactRequest);
        }

        [HttpGet]
        [Route("contacts")]
        public List<ContactResponse> getContacts()
        {
            return contactService.getContactList();
        }

        [HttpPost]
        [Route("messages")]
        public MessageResponse createMessage([FromBody] MessageRequest messageRequest)
        {
            return messageService.createMessage(messageRequest);
        }

        [HttpGet]
        [Route("contacts/{id}/messages")]
        public List<MessageResponse> getMessagesByContactId([FromRoute] int id)
        {
            return messageService.getMessagesByContactId(id);
        }

        [HttpGet]
        [Route("themes/{id}/messages")]
        public List<MessageResponse> getMessagesByThemeId([FromRoute] int id)
        {
            return messageService.getMessagesByThemeId(id);
        }

    }
}