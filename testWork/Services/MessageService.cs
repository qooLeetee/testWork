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
                    .FirstOrDefault(c => c.Id == messageRequest.theme.id);

                if (theme != null)
                {
                    message.Theme = theme;
                }
                else
                {
                    throw new Exception("Тема не найдена"); 
                }

                message.Content = messageRequest.content;

                var contact = db.Contacts
                    .FirstOrDefault(c => c.Name == messageRequest.contact.name && c.Phone == messageRequest.contact.phone);

                if (contact != null)
                {
                    message.Contact = contact;

                    db.Messages.Add(message);
                    contact.Messages.Add(message);

                    db.SaveChanges();

                    return createMessageResponse(message);
                }
                else
                {
                    contact = contactService.createContact(messageRequest.contact);
                    db.Contacts.Attach(contact);

                    message.Contact = contact;

                    contact.Messages.Add(message);
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
                messagesOfContact.AddRange(db.Messages.Where(m => m.Contact.Id == id));
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
            response.id = message.Id;
            response.themeId = message.ThemeId;
            response.content = message.Content;
            response.contactId = message.ContactId;

            return response;
        }
    }
}