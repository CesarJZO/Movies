using ControllersAPI.Entities;
using ControllersAPI.Repos;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddResponseCaching();
builder.Services.AddScoped<IRepo<Genre>, RepoOnMemory>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.Use(async (context, next) => {
    using var swapStream = new MemoryStream();

    var originalResponseBody = context.Response.Body;
    context.Response.Body = swapStream;
    
    await next.Invoke();

    swapStream.Seek(0, SeekOrigin.Begin);
    string responseBody = new StreamReader(swapStream).ReadToEnd();
    swapStream.Seek(0, SeekOrigin.Begin);

    await swapStream.CopyToAsync(originalResponseBody);
    context.Response.Body = originalResponseBody;

    app.Logger.LogInformation("Response: {RB}", responseBody);
});

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

app.UseRouting();

app.UseResponseCaching();

app.UseAuthorization();

// app.UseEndpoints(endpoints => endpoints.MapControllers());
app.MapControllers();

app.Run();
