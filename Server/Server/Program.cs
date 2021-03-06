
using DtoKit.Demo;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("config.json");
DtoKit.Demo.Setup.Configure(builder.Services);
builder.Services.AddMvc();
builder.Services.AddTransient<IDemoController, DemoController>();

var app = builder.Build();

app.MapControllers();

app.Use(async (HttpContext context, RequestDelegate next) => 
{
    Console.WriteLine(context.Request.Path);
    next?.Invoke(context);
});

app.Run();
