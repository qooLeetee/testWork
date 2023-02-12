using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace testWork.Controllers.DTO.Responses
{
    public class ThemeResponse
    {
        public int id { get; set; }
        public string title { get; set; }
    }
}