using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using TrainSnakeAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TrainSnakeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddAuthentication("cookie")
	.AddCookie("cookie")
	.AddOAuth("github", options =>
{
	options.SignInScheme = "cookie";
	options.ClientId = Environment.GetEnvironmentVariable("ClientId") ?? "error";
	options.ClientSecret = Environment.GetEnvironmentVariable("ClientSecret") ?? "error";
	options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
	options.TokenEndpoint = "https://github.com/login/oauth/access_token";
	options.CallbackPath = "/oauth/github-cb";
	options.UserInformationEndpoint = "https://api.github.com/user";
	options.SaveTokens = true;

	options.ClaimActions.MapJsonKey("sub", "id");
	options.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");
	options.Events.OnCreatingTicket = async ctx =>
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, ctx.Options.UserInformationEndpoint);
		request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ctx.AccessToken);
		using var response = await ctx.Backchannel.SendAsync(request); // Use await here
		var user = await response.Content.ReadFromJsonAsync<JsonElement>();
		ctx.RunClaimActions(user);
	};
});

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.WithOrigins(Environment.GetEnvironmentVariable("FrontEnd_URL") ?? "error")
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
