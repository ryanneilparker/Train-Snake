using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
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

			if (existingPlayer == null)
			{
				Player tempPlayer = new Player() { UserName = loginName, CreatedDate = DateTime.Now };

				dbContext.Add(tempPlayer);
				dbContext.SaveChanges();
			}

			return Redirect("markup/game.html?access_token=" + accessToken + "&playerName=" + loginName);
		}

		[HttpGet]
		[Route("login")]
		public IActionResult LoginUser()
		{
			Console.WriteLine("At login get");
      return Challenge(new AuthenticationProperties { RedirectUri = "/githubOAuth" }, "github");
		}

		[HttpGet("score")]
		public async Task<ActionResult> GetHighestPlayerScoreAsync(string playerName)
		{
			bool isAuthorised = await utils.AuthoriseRequest(Request);

			if (!isAuthorised)
			{
				return Unauthorized();
			}

			var player = dbContext.Player.FirstOrDefault(p => p.UserName == playerName);

			if (player == null)
			{
				return BadRequest();
			}

			int score = dbContext.GameLog.Where(p => p.PlayerId == player.Id).Select(s => s.Score).Max();

			var responseObject = new
			{
				score
			};

			// Serialize the response object to a JSON string
			var jsonResponse = JsonSerializer.Serialize(responseObject);

			return Ok(jsonResponse);
		}

		[HttpPost("score")]
		public async Task<ActionResult> UpdateUserScore(ScoreResult scoreResult)
		{
			bool isAuthorised = await utils.AuthoriseRequest(Request);

			if (!isAuthorised)
			{
				return Unauthorized();
			}
			// Add logic to update the user's score
			var player = dbContext.Player.FirstOrDefault(p => p.UserName == scoreResult.playerName);

			if (player == null)
			{
				return BadRequest();
			}

			var gameLog = new GameLog()
			{
				PlayerId = player.Id,
				Score = scoreResult.playerScore,
				CreatedDate = DateTime.Now
			};

			await dbContext.GameLog.AddAsync(gameLog);

			await dbContext.SaveChangesAsync();

			return NoContent();
		}
	}
}
