using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => { options.AddPolicy("Cors", policy => { policy.WithOrigins("http://localhost:4200"); }); });
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();
app.UseCors("Cors");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/*using testWork.Models;

namespace testWork
{
    class Program
    {
        static void Main(string[] args)
        {
            // добавление данных
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Theme testTheme = new Theme {title = "Здарова"};
                db.Themes.Add(testTheme);
                db.SaveChanges();
            }
            // получение данных
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var Themes = db.Themes.ToList();
                Console.WriteLine("Список объектов:");
                foreach (Theme u in Themes)
                {
                    Console.WriteLine($"{u.theme_ID}.{u.title}");
                }
            }
        }
    }
}*/