using testWork.Controllers.DTO;
using testWork.Controllers.DTO.Requests;
using testWork.Controllers.DTO.Responses;
using testWork.Models;

namespace testWork.Services
{

    public class ThemeService
    {
        public List<ThemeResponse> getThemeList()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var Themes = db.Themes.ToList();
                var ThemeResposes = new List<ThemeResponse>();
                foreach (var theme in Themes)
                {
                    ThemeResponse themeResponse= new ThemeResponse();
                    themeResponse.title = theme.title;
                    themeResponse.Id = theme.Id;
                    ThemeResposes.Add(themeResponse);
                }
                return ThemeResposes;
            }
        }
        
        public Theme createTheme(ThemeRequest themeRequest)
        {
            Theme theme = new Theme();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                theme.title = themeRequest.title;
                db.Themes.Add(theme);
                db.SaveChanges();
            }
            return theme;
        }
    }
}
