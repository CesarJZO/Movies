using ControllersAPI;
using ControllersAPI.ApiBehavior;
using ControllersAPI.Filters;
using ControllersAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<IFileStorage, CloudFileStorage>();
// builder.Services.AddTransient<IFileStorage, LocalFileStorage>();
// builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddCors(options =>
{
    // string[]? frontendUrls = builder.Configuration.GetSection("Frontend").GetValue<string[]>("Urls");
    // var frontendUrl = builder.Configuration.GetValue<string>("Frontend:Url");
    var urls = builder.Configuration.GetSection("Frontend:Urls").Get<string[]>()
        ?? throw new InvalidOperationException("Frontend URLs not found.");

    options.AddDefaultPolicy(builder =>
        builder.WithOrigins(urls)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders(["totalAmountOfRecords"])
    );
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
    options.Filters.Add<BadRequestParser>();
}).ConfigureApiBehaviorOptions(BadRequestsBehavior.Parse);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
