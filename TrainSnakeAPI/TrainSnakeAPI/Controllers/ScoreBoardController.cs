using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text.Json;
using TrainSnakeAPI.Data;
using TrainSnakeAPI.Models;
using TrainSnakeAPI.Utilities;

namespace TrainSnakeAPI.Controllers
{
	[Route("api/scoreboard")]
	[ApiController]
	public class ScoreBoardController : ControllerBase
	{
		private readonly TrainSnakeDbContext dbContext;
		private Utilities.Utilities utils = new Utilities.Utilities();

		public ScoreBoardController(TrainSnakeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		// Get - Score board
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ScoreResult>>> GetScoreBoard()
		{
			bool isAuthorised = await utils.AuthoriseRequest(Request);

			if (!isAuthorised)
			{
				return Unauthorized();
			}

			var highestScores = await dbContext.Player
		.Select(player => new ScoreResult
		{
			playerName = player.UserName,
			playerScore = dbContext.GameLog
						.Where(gameLog => gameLog.PlayerId == player.Id)
						.OrderByDescending(gameLog => gameLog.Score)
						.Select(gameLog => gameLog.Score)
						.FirstOrDefault()
		})
		.ToListAsync();

			return Ok(highestScores);
		}
	}
}
