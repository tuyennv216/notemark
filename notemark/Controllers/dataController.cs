using Microsoft.AspNetCore.Mvc;
using notemark.Interface;

namespace notemark.Controllers;

[ApiController]
[Route("[controller]")]
public class dataController : ControllerBase
{
	private readonly ILogger<dataController> _logger;
	private readonly IDataService _dataService;

	public dataController(ILogger<dataController> logger, IDataService dataService)
	{
		_logger = logger;
		_dataService = dataService;
	}

	[HttpGet("action-type-01")]
	public ActionResult GetActionType01()
	{
		var output = _dataService.GetActionType01();
		return Ok(output);
	}

	[HttpGet("action-type-2")]
	public ActionResult GetActionType2()
	{
		var output = _dataService.GetActionType2();
		return Ok(output);
	}

	[HttpGet("action-type-3")]
	public ActionResult GetActionType3()
	{
		var output = _dataService.GetActionType3();
		return Ok(output);
	}

	[HttpGet("keyboard-dictionary")]
	public ActionResult GetKeyboardDictionary()
	{
		var output = _dataService.GetKeyboardDictionary();
		return Ok(output);
	}

}
