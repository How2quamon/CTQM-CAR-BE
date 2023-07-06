using CTQM__CAR_API.CTQM_CAR.Infrastructure;
using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Repositories.Repository;
using CTQM_CAR.Service.Service.Implement;
using CTQM_CAR.Service.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MEC_DBContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICarDetailService, CarDetailServiceImpl>();
builder.Services.AddTransient<ICarService, CarServiceImpl>();
builder.Services.AddTransient<ICartService, CartServiceImpl>();
builder.Services.AddTransient<ICustomerService, CustomerServiceImpl>();
builder.Services.AddTransient<IOrderService, OrderServiceImpl>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

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

app.Run();