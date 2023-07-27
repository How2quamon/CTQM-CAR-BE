using CTQM__CAR_API.CTQM_CAR.Infrastructure;
using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Repositories.Repository;
using CTQM_CAR.Service.Service.Implement;
using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR_API.Identity;
using CTQM_CAR_API.Swagger;
using CTQM_CAR_API.Validators;
using CTQM_CAR_HEADER.IRepository;
using CTQM_CAR_HEADER.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();

// JWT Token config
// Add services to the container.
builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidIssuer = config["JwtSettings:Issuer"],
		ValidAudience = config["JwtSettings:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwtsettings:Key"]!)),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateIssuerSigningKey = true,
		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero
	};
});

builder.Services.AddControllers();

builder.Services.AddDbContext<MEC_DBContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Add Policy
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy(IdentityData.AdminCustomerPolicyName, p =>
		p.RequireClaim(IdentityData.AdminCustomerClaimName, "true"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddTransient<ICarDetailService, CarDetailServiceImpl>();
builder.Services.AddTransient<ICarService, CarServiceImpl>();
builder.Services.AddTransient<ICartService, CartServiceImpl>();
builder.Services.AddTransient<ICustomerService, CustomerServiceImpl>();
builder.Services.AddTransient<IOrderService, OrderServiceImpl>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IPaypalService, PaypalServiceImpl>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IAuthenticateRepo, AuthenticateRepo>();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddTransient<ITokenService, TokenServiceImpl>();
builder.Services.AddStackExchangeRedisCache(options =>
{
	//options.Configuration = config.GetConnectionString("AzureRedisCache");
	options.Configuration = "localhost:6379, ssl=False";
	options.InstanceName = "Ram Redis Cache";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseMiddleware<TokenValidatorMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

