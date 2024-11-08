using API_Bloodborne.Data;
using API_Bloodborne.Models;
using API_Bloodborne.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BloodborneConnection");
var connString = builder.Configuration.GetConnectionString("UsuarioConnection");


builder.Services.AddDbContext<UsuarioContext>(otps => otps.UseMySql(connString, new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddDbContext<BloodborneContext>(otps => otps.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddIdentity<Usuario, IdentityRole>().AddEntityFrameworkStores<UsuarioContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tgur4532$%#SDOGUBNYUVFAS6fgs8yfxdbas908dfgh23IPB")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
