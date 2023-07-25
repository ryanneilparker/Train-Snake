using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TrainSnakeAPI.Data;
using TrainSnakeAPI.Models;
using TrainSnakeAPI.Utilities;

namespace TrainSnakeAPI.Controllers
{
	[ApiController]
	[Route("api/player")]
	public class PlayerController : ControllerBase
	{
		private readonly TrainSnakeDbContext dbContext;
		private Utilities.Utilities utils = new Utilities.Utilities();

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

			var existingPlayer = dbContext.Player.FirstOrDefault(p => p.UserName == loginName);

			if(existingPlayer == null)
			{
				Player tempPlayer = new Player() { UserName = loginName, CreatedDate = DateTime.Now };

				dbContext.Add(tempPlayer);
				dbContext.SaveChanges();
			}			

			return Redirect("StaticFiles/markup/game.html?access_token=" + accessToken);
		}

		[HttpGet]
		[Route("login")]
		public IActionResult LoginUser()
		{
			Console.WriteLine("At login get");
			return Challenge(new AuthenticationProperties { RedirectUri = "/githubOAuth" }, "github");
		}

		//Update - Score
		[HttpPut("score")]
		public async Task<IActionResult> UpdateUserScore()
		{
			bool isAuthorised = await utils.AuthoriseRequest(Request);

			// Add logic to update the user's score

			return BadRequest();
		}
	}
}
