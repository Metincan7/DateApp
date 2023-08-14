using Microsoft.Extensions.FileProviders;
using Api.Extensions;
using Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseFileServer(new FileServerOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")), 
    RequestPath = new PathString("/node_modules"),
    EnableDirectoryBrowsing = true
});



app.UseCors(
    builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithOrigins("http://localhost:4200")
);


app.UseAuthentication();
app.UseAuthorization();
// hbhjbjnbhjnknhk
app.UseStaticFiles();
app.UseHttpsRedirection();


app.MapControllers();

app.Run();
