using Microsoft.Extensions.FileProviders;
using Api.Extensions;
using Api.Middleware;
using Api.Data;
using Microsoft.EntityFrameworkCore;

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


using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
try
{
    var context =services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();  
    await Seed.SendUser(context);
}
catch (Exception ex)
{
    var logger =services.GetService<ILogger<Program>>();
    logger.LogError(ex,"An error ocurred during migrations.");
    
}


app.Run();
