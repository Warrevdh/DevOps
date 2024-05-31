using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Persistence;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Server.Middleware;
using Microsoft.AspNetCore.Authentication;
using Server.Authentication;
using Services;
using Shared.Users.Patients;
using Shared.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Client.Classes;
using Client.Admin.Components.Team;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddTransient <IEmailSender, EmailSender>();

// Fluentvalidation
builder.Services.AddValidatorsFromAssemblyContaining<PatientDto.Mutate.Validator>();//??? For each DTO??
builder.Services.AddFluentValidationAutoValidation();


// Swagger | OAS 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "bearer"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
	// Since we subclass our dto's we need a more unique id.
	options.CustomSchemaIds(type => type.DeclaringType is null ? $"{type.Name}" : $"{type.DeclaringType?.Name}.{type.Name}");
	options.EnableAnnotations();
}).AddFluentValidationRulesToSwagger();


//Auth0
builder.Services.AddAuth0AuthenticationClient(config =>
{
	config.Domain = builder.Configuration["Auth0:Authority"];
	config.ClientId = builder.Configuration["Auth0:ClientId"];
	config.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
});

builder.Services.AddAuth0ManagementClient().AddManagementAccessToken();


// Database
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseMySql(
		builder.Configuration.GetConnectionString("MySql"),
		serverVersion)
	// The following three options help with debugging, but should
	// be changed or removed for production.
	.LogTo(Console.WriteLine, LogLevel.Information)
	.EnableSensitiveDataLogging()
	.EnableDetailedErrors()
); ;


builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.Authority = builder.Configuration["Auth0:Authority"];
	options.Audience = builder.Configuration["Auth0:ApiIdentifier"];
	options.TokenValidationParameters = new TokenValidationParameters
	{
		NameClaimType = ClaimTypes.NameIdentifier
	};
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpContextAccessor>().HttpContext.User);

builder.Services.AddControllersWithViews()
		.AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
			options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
		});
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseWebAssemblyDebugging();
}
else
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers().RequireAuthorization();
app.MapFallbackToFile("index.html");


using (var scope = app.Services.CreateScope())
{ // Require a DbContext from the service provider and seed the database.
	var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	FakeSeeder seeder = new(dbContext);
	seeder.Seed();
}

app.Run();