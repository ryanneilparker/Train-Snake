using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TrainSnakeAPI.Data;

namespace TrainSnakeAPI.Controllers
{
	[ApiController]
	[Route("api/player")]
	public class PlayerController : ControllerBase
	{
		private readonly TrainSnakeDbContext dbContext;

		public PlayerController(TrainSnakeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet("/githubOAuth")]
		public IActionResult AuthPath()
		{
			var accessToken = HttpContext.GetTokenAsync("access_token").Result;

			// Retrieve the login name and ID claims
			var loginName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
			var id = HttpContext.User.FindFirstValue("sub");

			return Redirect("http://127.0.0.1:5500/client/markup/game.html?access_token=" + accessToken);
				 // Return the login name and ID as an IEnumerable<string>
			//return new List<string> { $"Login Name: {loginName}", $"ID: {id}" };
		}

		[HttpGet]
		[Route("login")]
		public IActionResult LoginUser()
		{
			Console.WriteLine("At login get");
			return Challenge(new AuthenticationProperties { RedirectUri = "https://localhost:7223/githubOAuth" }, "github");
		}

		//Update - Score
	}
}
