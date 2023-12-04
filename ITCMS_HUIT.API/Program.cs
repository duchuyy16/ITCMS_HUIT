using ITCMS_HUIT.API.Authenticate;
using ITCMS_HUIT.Models;
using ITCMS_HUIT.Repository.Implement;
using ITCMS_HUIT.Repository.Interfaces;
using MailKit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Services;
using Services.MailKit;
using Services.Mappers;
using System.Security.Principal;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// For Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ITCMSConnectString")));

builder.Services.AddDbContext<ITCMS_HUITContext>(options => options.UseSqlServer(configuration.GetConnectionString("ITCMSConnectString")));

// For SendMail
builder.Services.AddSingleton<MailSettings>(new MailSettings { Username = configuration["MailSettings:Username"], Password = configuration["MailSettings:Password"] });
builder.Services.AddScoped<Services.MailKit.IMailService, Services.MailKit.MailService>();

// For Automapper
builder.Services.ConfigureMapper();

builder.Services.AddScoped<IRepo, Repo>();
builder.Services.AddScoped<LopHocService>();
builder.Services.AddScoped<ChuongTrinhDaoTaoService>();
builder.Services.AddScoped<KhoaHocService>();
builder.Services.AddScoped<GiaoVienService>();
builder.Services.AddScoped<ThongTinHocVienService>();
builder.Services.AddScoped<HocVienService>();
builder.Services.AddScoped<CoSoDuLieuService>();
builder.Services.AddScoped<TrangThaiHocVienService>();
builder.Services.AddScoped<DoiTuongDangKyService>();
builder.Services.AddScoped<ChartService>();


// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(2);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
