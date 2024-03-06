using REST.Middleware;
using REST.Service.Implementation;
using REST.Service.Interface;
using REST.Storage.Common;
using REST.Storage.InMemoryDb;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<DbStorage, InMemoryDbContext>();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services
    .AddScoped<IAuthorService, AuthorService>()
    .AddScoped<IMarkerService, MarkerService>()
    .AddScoped<IPostService, PostService>()
    .AddScoped<ITweetService, TweetService>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();
app.UseURLLog();
app.MapControllers();
app.Run();
