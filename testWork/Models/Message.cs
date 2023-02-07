using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testWork.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string? Content { get; set; }
        public Contact Contact { get; set; }
        public Theme Theme { get; set; }

        public int? ContactId { get; set; }
        public int? ThemeId { get; set; }
    }
}
