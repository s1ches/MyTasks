using System.Reflection;
using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});

services.AddApplication();
services.AddPersistence(configuration);
services.AddControllers();
services.AddSwaggerGen();

services.AddCors(opt
    => opt.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    })
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

try
{
    var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<NotesDbContext>();
    DbInitializer.Initialize(context);
}catch (Exception ex) 
{
    Console.WriteLine(ex.Message);
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();