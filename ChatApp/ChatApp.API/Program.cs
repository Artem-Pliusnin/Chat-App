using System.Text;
using Azure;
using Azure.AI.TextAnalytics;
using ChatApp.API.Controllers;
using ChatApp.API.Interfaces;
using ChatApp.API.Mapping;
using ChatApp.API.Services;
using ChatApp.Data;
using ChatApp.Data.Interfaces;
using ChatApp.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:4200", "https://proud-grass-0cb16e503-preview.westeurope.3.azurestaticapps.net");
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ChatDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ChatAppConnection")));

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

builder.Services.AddSignalR()
    .AddAzureSignalR(options =>
    {
        options.ConnectionString = builder.Configuration["Azure:SignalR:ConnectionString"];
    });

builder.Services.AddSingleton(serviceProvider =>
{
    var config = serviceProvider.GetRequiredService<IConfiguration>();
    var credential = new AzureKeyCredential(config["Azure:TextAnalytics:Key"]);
    var endpoint = new Uri(config["Azure:TextAnalytics:Endpoint"]);

    return new TextAnalyticsClient(endpoint, credential);
});


builder.Services.AddAutoMapper(
    cfg => { },
    typeof(AutomapperProfiles)
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageStatusRepository, MessageStatusRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<ICognitiveService, CognitiveService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chathub");

app.Run();
