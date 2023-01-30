using System.Globalization;
using System.Text;
using DentOnline.Application;
using DentOnline.Application.Utilities.Middlewares;
using DentOnline.Infrastructure;
using DentOnline.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());

// Add services to the container.
builder.Services.AddResponseCompression();

builder.Services.AddControllers().AddMvcLocalization();

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new("en-US"),
        new("tr-TR")
    };

    opt.DefaultRequestCulture = new RequestCulture("tr-TR");

    opt.SupportedCultures = supportedCultures;
    opt.SupportedUICultures = supportedCultures;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins(
            "http://localhost:3000",
            "https://localhost:3000",
            "http://188.132.128.139:5001",
            "https://188.132.128.139:5001",
            "http://ozanercan.com.tr:5001",
            "https://ozanercan.com.tr:5001")
        .AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddInfrastructureDependencies();
builder.Services.AddApplicationDependencies();
builder.Services.AddPersistenceDependencies();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors();

// Localization
var options = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(options.Value);

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCompression();

app.UseStaticFiles();

app.UseDirectoryBrowser();

// app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();