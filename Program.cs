using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoviesOnline.Data;
using MoviesOnline.Middlewares;
using MoviesOnline.Services.CookieServices;
using MoviesOnline.Services.HttpServices;
using MoviesOnline.Services.UserServices;
using MoviesOnline.ValidationRules;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;



builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<DataBaseContext>();
builder.Services.AddSingleton<IdGenerator>();
builder.Services.AddSingleton<HashPasswordSerivce>();
builder.Services.AddScoped<MovieConverter>();
builder.Services.AddHttpClient<MovieAPICollector>();
builder.Services.AddScoped<TokenGenerator>();
builder.Services.AddDbContext<DataBaseContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddControllers().AddFluentValidation(f =>
{
    f.RegisterValidatorsFromAssemblyContaining<AuthRegValidation>();
    f.RegisterValidatorsFromAssemblyContaining<AuthLogValidation>();
});
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseLogTiming();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
