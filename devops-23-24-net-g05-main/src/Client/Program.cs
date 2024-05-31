using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared.EyeConditions;
using Client.EyeConditions;
using Client;
using Shared.Articles;
using Client.Articles;
using Radzen;
using Client.Files;
using Client.Symptoms;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Client.Classes;
using Client.Admin.Services;
using Shared.Users.Doctors.Employees;
using Client.Team;
using Shared.Users.Team.Doctors;
using Shared.Users.Teams.Groups;
using Shared.Users.Teams.Biographies;
using Client.Admin.Components.Team;
using Shared.Users.Doctors.Availabilities;
using Client.Admin.Components.Availabilities;
using Shared.Appointments.Timeslots;
using Client.Appointment;
using Shared.Appointments;
using Client.Appointment.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRadzenComponents();
//builder.Services.AddAuthorizationCore();
//builder.Services.AddSingleton<FakeAuthenticationProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<FakeAuthenticationProvider>());
//builder.Services.AddTransient<FakeAuthorizationMessageHandler>();

builder.Services.AddHttpClient("Project.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
	.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Project.ServerAPI"));

builder.Services.AddHttpClient<PublicClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddOidcAuthentication(options =>
{
	builder.Configuration.Bind("Auth0", options.ProviderOptions);
	options.ProviderOptions.ResponseType = "code";
	options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
}).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

builder.Services.AddScoped<IEyeConditionService, EyeConditionService>();
builder.Services.AddScoped<ISymptomService, SymptomService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IBioService, BioService>();
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<ITimeslotService, TimeslotService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddHttpClient<IStorageService, AzureBlobStorageService>();
builder.Services.AddSingleton<NavService>();

await builder.Build().RunAsync();

