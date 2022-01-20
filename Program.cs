using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Configuration;
using DataAccessLayer.Models;
using Microsoft.OpenApi.Models;

var allowedOrigins = new[] { "http://localhost:3000" };
const string allowSpecificOrigins = "_AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
// Add services to the container.
builder.Services.AddSingleton<IMapper>(mapper);
builder.Services.AddScoped<IFilmService, FilmService>()
                .AddScoped<IUoWFactory, UoWFactory>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IGenreFilmService, GenreFilmService>();
builder.Services.AddScoped<IFilmPersonService,FilmPersonService>();
builder.Services.AddScoped<IMarkService, MarkService>();
builder.Services.AddScoped<IMarkService, MarkService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                }); // Not needed - delete
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowSpecificOrigins,
                      corsPolicyBuilder =>
                      {
                          corsPolicyBuilder.WithOrigins(allowedOrigins)
                                           .AllowCredentials()
                                           .AllowAnyHeader()
                                           .AllowAnyMethod();
                      });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "FilmLibrary", Version = "v1" });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();
app.UseCors(allowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();