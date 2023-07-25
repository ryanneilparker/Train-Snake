using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

namespace TrainSnakeAPI.Utilities
{
	public class Utilities
	{
		public string GetAccessToken(HttpRequest request)
		{
			string accessToken = "";
			if (request.Headers.TryGetValue("Authorization", out var authHeaderValues))
			{
				// Convert the StringValues to a single string
				string authHeader = authHeaderValues.ToString();

				if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
				{
					accessToken = authHeader.Substring(7);
				}
			}

			return accessToken;
		}
		public async Task<bool> AuthoriseRequest(HttpRequest request)
		{
			bool isAuthorised = false;

			try
			{
				// Get the access token from the request's authorization header
				string accessToken = GetAccessToken(request);

				// Make a request to GitHub's user API to validate the access token
				using (var httpClient = new HttpClient())
				{
					httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
					httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", accessToken);

					var response = await httpClient.GetAsync("https://api.github.com/user");

					if (response.IsSuccessStatusCode)
					{
						//var userJson = await response.Content.ReadAsStringAsync();
						isAuthorised = true;
					}
					else
					{
						// Failed to validate the access token
						isAuthorised = false;
					}
				}
			}
			catch (Exception)
			{
				// Error occurred during the token verification process
				isAuthorised = false;
				return isAuthorised;
			}

			return isAuthorised;
		}
	}
}
