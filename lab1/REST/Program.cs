using REST.Service.Implementation;
using REST.Service.Interface;
using REST.Storage;
using REST.Storage.InMemoryDb;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<DbStorage, InMemoryDbContext>();
builder.Services
    .AddScoped<IAuthorService, AuthorService>()
    .AddScoped<IMarkerService, MarkerService>()
    .AddScoped<IPostService, PostService>()
    .AddScoped<ITweetService, TweetService>();


var app = builder.Build();
app.MapControllers();
app.Run();
