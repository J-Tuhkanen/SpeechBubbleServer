using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using SpeechBubble.Server.Data;
using SpeechBubble.Server.Hubs;
using SpeechBubble.Server.Models;
using SpeechBubble.Server.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddResponseCompression(options =>
{
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) .AddCookie();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=GM05NRB0\\SQLEXPRESS;Database=Chat;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"));
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 0;
})
.AddSignInManager<SignInManager<User>>()
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(config =>
{
    config.Events = new JwtBearerEvents
    {
        OnTokenValidated = async ctx =>
        {
            var authenticationService = ctx.HttpContext.RequestServices.GetRequiredService<IAuthenticationService>();
            var userId = ctx.Principal.Identity.Name;
            var user = await authenticationService.GetByIdAsync(userId);

            if (user == null)
            {
                // Return unauthorized if user no longer exists
                ctx.Fail("Unauthorized");
            }
        },
        OnMessageReceived = context =>
        {
            var queries = context.Request.Query;

            StringValues authHeaders = context.Request.Headers["Authorization"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;

            if (authHeaders.Any() && (path.StartsWithSegments("/chathub")))
            {
                // Read the token out of the query string
                context.Token = authHeaders.First().Split(" ").Last();
            }
            return Task.CompletedTask;
        }
    };
    config.RequireHttpsMetadata = true;
    config.SaveToken = false;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5ur04327154k3284pok5dh34851umf134+0658324905ds232103495vum98034u589d34lu583421k+u589sd234u53210dlk5u90231s4u59+3l4218590+d34f")),
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        RequireExpirationTime = true,
        ValidateLifetime = true
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chathub/{roomId}");

app.Run();
