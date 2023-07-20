using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrainSnakeAPI.Controllers
{
	[Route("api/scoreboard")]
	[ApiController]
	public class ScoreBoardController : ControllerBase
	{
		// Get - Score board
		[HttpGet]
		public IActionResult GetScoreBoard()
		{
			Console.WriteLine("Getting score board");
			var response = new { success = true };
			return Ok(response);
		}
	}
}
