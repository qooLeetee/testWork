using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testWork.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }
        public Contact Contact { get; set; }
        public Theme Theme { get; set; }

        [ForeignKey(nameof(Contact))]
        public int ContactId { get; set; }

        [ForeignKey(nameof(Theme))]
        public int ThemeId { get; set; }

    }
}
