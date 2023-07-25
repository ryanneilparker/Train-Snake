using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TrainSnakeAPI.Utilities;

namespace TrainSnakeAPI.Controllers
{
	[Route("api/scoreboard")]
	[ApiController]
	public class ScoreBoardController : ControllerBase
	{
		private Utilities.Utilities utils = new Utilities.Utilities();

		// Get - Score board
		[HttpGet]
		public async Task<IActionResult> GetScoreBoard()
		{
			bool isAuthorised = await utils.AuthoriseRequest(Request);

			var responseObject = new
			{
				success = isAuthorised,
				message = isAuthorised ? "User authenticated" : "User not authenticated"
			};

			// Serialize the response object to a JSON string
			var jsonResponse = JsonSerializer.Serialize(responseObject);

			// Return the JSON response with appropriate status code
			if (isAuthorised)
			{
				return Ok(jsonResponse); // 200 OK
			}
			else
			{
				return Unauthorized(jsonResponse); // 401 Unauthorized
			}
		}
	}
}
