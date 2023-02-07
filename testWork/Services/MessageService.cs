using Microsoft.EntityFrameworkCore;
using testWork.Controllers.DTO.Requests;
using testWork.Controllers.DTO.Responses;
using testWork.Models;

namespace testWork.Services
{
    public class MessageService
    {
        ThemeService themeService = new ThemeService();
        ContactService contactService = new ContactService();

        public MessageResponse createMessage(MessageRequest messageRequest)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var message = new Message();

                var theme = db.Themes
                 .Where(c => c.Id == messageRequest.themeId)
                 .FirstOrDefault();

                if (theme != null)
                {
                    message.Theme = theme;
                }

                message.Content = messageRequest.Content;

                var contact = db.Contacts
                .Where(c => c.Name == messageRequest.Contact.Name && c.Phone == messageRequest.Contact.Phone)
                .FirstOrDefault();

                if (contact != null)
                {
                    message.Contact = contact;

                    db.Messages.Add(message);
                    contact.Message.Add(message);

                    db.SaveChanges();

                    return createMessageResponse(message);
                }
                else
                {
                    contact = contactService.createContact(messageRequest.Contact);
                    db.Contacts.Attach(contact);

                    message.Contact = contact;

                    contact.Message.Add(message);
                    db.SaveChanges();

                    return createMessageResponse(message);
                }
            }
        }
        // Возвращает список сообщений по id контакта который их оставил
        public List<MessageResponse> getMessagesByContactId(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Message> messagesOfContact = new List<Message>();
                messagesOfContact.AddRange(db.Messages.Where(m => m.ContactId == id));
                List<MessageResponse> messageResponses = new List<MessageResponse>();
                foreach (var message in messagesOfContact) {
                    messageResponses.Add(createMessageResponse(message));
                }
                return messageResponses;
            }
        }
        // Возвращает список сообщений по id темы сообщения
        public List<MessageResponse> getMessagesByThemeId(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Message> messagesOfContact = new List<Message>();
                messagesOfContact.AddRange(db.Messages.Where(m => m.ThemeId == id));
                List<MessageResponse> messageResponses = new List<MessageResponse>();
                foreach (var message in messagesOfContact)
                { 
                    messageResponses.Add(createMessageResponse(message));
                }
                return messageResponses;
            }
        }

        private MessageResponse createMessageResponse(Message message)
        {
            MessageResponse response = new MessageResponse();
            response.Id = message.Id;
            response.ThemeId = message.ThemeId;
            response.Content = message.Content;
            response.ContactId = message.ContactId;

            return response;
        }
    }
}