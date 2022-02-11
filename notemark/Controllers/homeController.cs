using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace notemark.Controllers;

[ApiController]
[Route("/")]
public class homeController : ControllerBase
{
	private readonly IConfiguration _configuration;

	public homeController(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	[HttpGet("/")]
	public IActionResult Welcome()
	{
		var wellcome = _configuration.GetValue<string>("wellcome") ?? "Hello! Server is running.";
		return Ok(wellcome);
	}

	[Route("/{**unknown}")]
	[HttpGet]
	[HttpPost]
	[HttpPut]
	[HttpDelete]
	[HttpOptions]
	[HttpHead]
	[HttpPatch]
	public IActionResult Forbiden()
	{
		return StatusCode((int)HttpStatusCode.Forbidden);
	}
}
