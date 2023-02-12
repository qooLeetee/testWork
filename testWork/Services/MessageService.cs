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
                    .FirstOrDefault(c => c.id == messageRequest.theme.id);

                if (theme != null)
                {
                    message.theme = theme;
                }
                else
                {
                    throw new Exception("Тема не найдена"); 
                }

                message.content = messageRequest.content;

                var contact = db.Contacts
                    .FirstOrDefault(c => c.name == messageRequest.contact.name && c.phone == messageRequest.contact.phone);

                if (contact != null)
                {
                    message.contact = contact;

                    db.Messages.Add(message);
                    contact.messages.Add(message);

                    db.SaveChanges();

                    return createMessageResponse(message);
                }
                else
                {
                    contact = contactService.createContact(messageRequest.contact);
                    db.Contacts.Attach(contact);

                    message.contact = contact;

                    contact.messages.Add(message);
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
                messagesOfContact.AddRange(db.Messages.Where(m => m.contact.id == id));
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
                messagesOfContact.AddRange(db.Messages.Where(m => m.theme.id == id));
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
            response.id = message.id;
            response.themeId = message.theme.id;
            response.content = message.content;
            response.contactId = message.contact.id;

            return response;
        }
    }
}