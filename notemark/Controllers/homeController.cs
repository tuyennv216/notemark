using Microsoft.AspNetCore.Mvc;
using notemark.Interface;
using System.Net;

namespace notemark.Controllers;

[ApiController]
[Route("/")]
public class homeController : ControllerBase
{
	private readonly IConfiguration _configuration;
	private readonly IDataService _dataService;

	public homeController(IConfiguration configuration, IDataService dataService)
	{
		_configuration = configuration;
		_dataService = dataService;
	}

	[HttpGet("/")]
	public IActionResult Welcome()
	{
		var wellcome = _configuration.GetValue<string>("wellcome") ?? "Hello! Server is running.";
		var endpoints = _dataService.GetEndpoints();
		var output = wellcome + Environment.NewLine + Environment.NewLine + endpoints;

		return Ok(output);
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
