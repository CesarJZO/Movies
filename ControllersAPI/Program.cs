using ControllersAPI.Entities;
using ControllersAPI.Repos;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IRepo<Genre>, RepoOnMemory>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// If the app is run with the following command:
// app.Run(async context => await context.Response.WriteAsync("Hello World!"));
// The rest of the middleware is not executed.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// All middlewares starting with Use are executed in the order they are declared.
// And they do not interrupt the execution of the next middlewares.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
