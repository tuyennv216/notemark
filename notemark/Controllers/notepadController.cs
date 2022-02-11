using Microsoft.AspNetCore.Mvc;
using notemark.Interface;

namespace notemark.Controllers;

[ApiController]
[Route("[controller]")]
public class notepadController : ControllerBase
{
	private readonly ILogger<notepadController> _logger;
	private readonly INotepadService _notepadService;

	public notepadController(ILogger<notepadController> logger, INotepadService notepadService)
	{
		_logger = logger;
		_notepadService = notepadService;
	}

	[HttpGet("new-macro")]
	public ActionResult NewMacro(string name, bool control, bool alt, bool shift, int key)
	{
		var output = _notepadService.NewMacro(name, control, alt, shift, key);
		return Ok(output);
	}

	[HttpGet("new-action")]
	public ActionResult NewAction(int type, int message, int param1, string param2)
	{
		var output = _notepadService.NewAction(type, message, param1, param2);
		return Ok(output);
	}

	[HttpGet("new-action-full")]
	public ActionResult NewActionFull(int type, int message, int wParam, int lParam, string sParam)
	{
		var output = _notepadService.NewAction(type, message, wParam, lParam, sParam);
		return Ok(output);
	}

	[HttpGet("new-macro-string")]
	public ActionResult NewMacroAsString(string name, bool control, bool alt, bool shift, int key)
	{
		var output = _notepadService.NewMacroAsString(name, control, alt, shift, key);
		return Ok(output);
	}

	[HttpGet("new-action-string")]
	public ActionResult NewActionShortString(int type, int message, int param1, string param2)
	{
		var output = _notepadService.NewAction(type, message, param1, param2);
		return Ok(output);
	}

	[HttpGet("new-action-full-string")]
	public ActionResult NewActionAsString(int type, int message, int wParam, int lParam, string sParam)
	{
		var output = _notepadService.NewActionAsString(type, message, wParam, lParam, sParam);
		return Ok(output);
	}

	[HttpGet("escape-xml-string")]
	public ActionResult EscapeXmlString(string str)
	{
		var output = _notepadService.EscapeXmlString(str);
		return Ok(output);
	}

	[HttpGet("describe-name")]
	public ActionResult DescribeName(string name, string ctrl, string alt, string shift, string key)
	{
		var output = _notepadService.DescribeName(name, ctrl, alt, shift, key);
		return Ok(output);
	}

	[HttpGet("describe-action")]
	public ActionResult DescribeAction(string type, string message, string wParam, string lParam, string sParam)
	{
		var output = _notepadService.DescribeAction(type, message, wParam, lParam, sParam);
		return Ok(output);
	}

	[HttpPost("describe-macro")]
	public ActionResult DescribeMacro([FromBody]string encodedMacro)
	{
		var output = _notepadService.DescribeMacro(encodedMacro);
		return Ok(output);
	}
}
