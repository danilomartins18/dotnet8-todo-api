using Microsoft.AspNetCore.Mvc;

namespace Ping.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PingController(ILogger<PingController> logger) : ControllerBase
	{
		private readonly ILogger<PingController> _logger = logger;

		[HttpGet]
		public ActionResult<string> GetAll()
		{
			 string podName = Environment.GetEnvironmentVariable("HOSTNAME") ?? "Unknown";
			
			_logger.LogInformation("Pinged by pod {PodName}!", podName);
			return Ok($"Pinged by pod {podName}!");
		}
	}
}