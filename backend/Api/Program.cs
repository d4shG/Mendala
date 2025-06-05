using System.Text;
using Api.Data.Context;
using Api.Data.Entities;
using Api.Middleware.GlobalErrorHandlingMiddleware;
using Api.Repositories.CustomerRepository;
using Api.Repositories.InvoiceRepository;
using Api.Repositories.IssueRepository;
using Api.Repositories.ProductRepository;
using Api.Repositories.RefreshTokenRepository;
using Api.Repositories.StatusHistoryRepository;
using Api.Services.CustomerService;
using Api.Services.InvoiceService;
using Api.Services.IssueService;
using Api.Services.ProductService;
using Api.Services.StatusHistoryService;
using Api.Services.TokenService;
using Api.Services.UserService;
using Api.Utils.DtoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddDb(builder);
AddIdentity(builder);
AddAuthentication(builder);
AddServices(builder);
AddCookiePolicy(builder);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	
	var db = services.GetRequiredService<MendalaApiContext>();
	
	if (db.Database.IsRelational())
		db.Database.Migrate();
	
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.UseCookiePolicy();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddServices(WebApplicationBuilder webApplicationBuilder)
{
	webApplicationBuilder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
	webApplicationBuilder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
	webApplicationBuilder.Services.AddScoped<IProductRepository, ProductRepository>();
	webApplicationBuilder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
	webApplicationBuilder.Services.AddScoped<IStatusHistoryRepository, StatusHistoryRepository>();
	webApplicationBuilder.Services.AddScoped<IIssueRepository, IssueRepository>();
	webApplicationBuilder.Services.AddScoped<ICustomerService, CustomerService>();
	webApplicationBuilder.Services.AddScoped<IInvoiceService, InvoiceService>();
	webApplicationBuilder.Services.AddScoped<IProductService, ProductService>();
	webApplicationBuilder.Services.AddScoped<IStatusHistoryService, StatusHistoryService>();
	webApplicationBuilder.Services.AddScoped<IIssueService, IssueService>();
	webApplicationBuilder.Services.AddScoped<IUserService, UserService>();
	webApplicationBuilder.Services.AddScoped<ITokenService, TokenService>();
}

void AddDb(WebApplicationBuilder builder1)
{
	builder1.Services.AddDbContext<MendalaApiContext>(options =>
	{
		options.UseNpgsql(
			builder1.Configuration.GetConnectionString("DbConnection"));
	});
}

void AddIdentity(WebApplicationBuilder webApplicationBuilder1)
{
	webApplicationBuilder1.Services
		.AddIdentityCore<User>(options =>
		{
			options.SignIn.RequireConfirmedAccount = false;
			options.User.RequireUniqueEmail = true;
			options.Password.RequireDigit = false;
			options.Password.RequiredLength = 6;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireUppercase = false;
			options.Password.RequireLowercase = false;
		})
		.AddRoles<IdentityRole>()
		.AddEntityFrameworkStores<MendalaApiContext>();
}

void AddAuthentication(WebApplicationBuilder builder2)
{
	builder2.Services
		.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.Events = new JwtBearerEvents
			{
				OnMessageReceived = context =>
				{
					var token = context.Request.Cookies["AuthToken"];
					if (!string.IsNullOrEmpty(token))
						context.Token = token;

					return Task.CompletedTask;
				}
			};

			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ClockSkew = TimeSpan.Zero,
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = builder2.Configuration["ValidIssuer"],
				ValidAudience = builder2.Configuration["ValidAudience"],
				IssuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(builder2.Configuration["IssuerSigningKey"])
				),
			};
		});
}

void AddCookiePolicy(WebApplicationBuilder builder3)
{
	builder3.Services.Configure<CookiePolicyOptions>(options =>
	{
		options.HttpOnly = HttpOnlyPolicy.Always;
		options.Secure = CookieSecurePolicy.Always;
		options.MinimumSameSitePolicy = SameSiteMode.None;
	});
}