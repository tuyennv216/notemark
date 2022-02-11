using notemark.Interface;
using notemark.Model;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace notemark.Service;

public class NotepadService : INotepadService
{
	private readonly IConfiguration _configuration;
	private readonly IDataService _dataService;

	public NotepadService(IConfiguration configuration, IDataService dataService)
	{
		_configuration = configuration;
		_dataService = dataService;
	}

	public Macro NewMacro(string name, bool control, bool alt, bool shift, int key)
	{
		var macro = new Macro(name, control, alt, shift, key);
		return macro;
	}

	public Act NewAction(int type, int message, int wParam, int lParam, string sParam)
	{
		var action = new Act(type, message, wParam, lParam, sParam);
		return action;
	}

	public Act NewAction(int type, int message, int param1, string param2)
	{
		var action = new Act();
		switch (type)
		{
			case 0:
				action = new Act { Type = type, Message = message, lParam = param1 };
				break;
			case 1:
				action = new Act { Type = type, Message = message, lParam = param1, sParam = param2 };
				break;
			case 2:
				action = new Act { Type = type, wParam = message };
				break;
			case 3:
				action = new Act { Type = type, wParam = message, lParam = param1, sParam = param2 };
				break;
		}
		return action;
	}

	public string NewMacroAsString(string name, bool control, bool alt, bool shift, int key)
	{
		var macro = NewMacro(name, control, alt, shift, key);
		return macro.ToString();
	}

	public string NewActionAsString(int type, int message, int wParam, int lParam, string sParam)
	{
		var action = NewAction(type, message, wParam, lParam, sParam);
		return action.ToString();
	}

	public string NewActionAsString(int type, int message, int param1, string param2)
	{
		var action = NewAction(type, message, param1, param2);
		return action.ToString();
	}

	public string EscapeXmlString(string str)
	{
		var escaped = SecurityElement.Escape(str);
		return escaped;
	}

	public string DescribeName(string name, string ctrl, string alt, string shift, string key)
	{
		var keyboardMapping = _dataService.GetKeyboardDictionary();
		var keyName = keyboardMapping.ContainsKey(key) ? keyboardMapping[key].Keyname : "";
		var hotkey = (ctrl == "yes" ? "Ctrl + " : "") + (alt == "yes" ? "Alt + " : "") + (shift == "yes" ? "Shift + " : "") + keyName;
		var textHotkey = hotkey.Length > 0 ? $", Hotkey: {hotkey}" : "";

		var describe = $"<!-- {name}{textHotkey} -->";

		return describe;
	}

	public string DescribeAction(string type, string message, string wParam, string lParam, string sParam)
	{
		var type01Dict = _dataService.GetActionType01AsDictionary();
		var type2Dict = _dataService.GetActionType2AsDictionary();
		var type3Dict = _dataService.GetActionType3AsDictionary();

		Data.Model.Act? act = null;
		string typeName = "";
		switch (type)
		{
			case "0":
				type01Dict.TryGetValue(message, out act);
				typeName = "Scintilla";
				break;
			case "1":
				type01Dict.TryGetValue(message, out act);
				typeName = "Scintilla with input";
				break;
			case "2":
				type2Dict.TryGetValue(wParam, out act);
				typeName = "Notepad";
				break;
			case "3":
				type3Dict.TryGetValue(message, out act);
				typeName = "Search & replace";
				break;
		}
		if (act == null) act = new();

		var describe = $"<!-- {typeName}, {act.Name} {act.Description} -->";

		return describe;
	}

	public string DescribeMacro(string encodedMacro)
	{
		var namePattern = _configuration.GetValue("custom:notepad:namePattern",
			"^(?<tab>\\s*)<Macro\\sname=\"(?<name>.+?)\"\\sCtrl=\"(?<ctrl>.+?)\"\\salt=\"(?<alt>.+?)\"\\sShift=\"(?<shift>.+?)\"\\sKey=\"(?<key>.+?)\">$");

		var actionPattern = _configuration.GetValue("custom:notepad:actionPattern",
			"^(?<tab>\\s*)<Action\\stype=\"(?<type>.+?)\"\\smessage=\"(?<message>.+?)\"\\swParam=\"(?<wParam>.+?)\"\\slParam=\"(?<lParam>.+?)\"\\ssParam=\"(?<sParam>.*?)\"\\s/>$");

		var nameRegex = new Regex(namePattern);
		var actionRegex = new Regex(actionPattern);

		var decodedMacro = Uri.UnescapeDataString(encodedMacro);
		var lines = Regex.Split(decodedMacro, "\n|\r\n");
		var strBuilder = new StringBuilder();

		Match match;
		string savedLine = "";
		Array.ForEach(lines, line =>
		{
			if ((match = nameRegex.Match(line)).Success)
			{
				var nameDescribe = DescribeName(
					match.Groups["name"].Value,
					match.Groups["ctrl"].Value,
					match.Groups["alt"].Value,
					match.Groups["shift"].Value,
					match.Groups["key"].Value);

				if (nameDescribe != savedLine)
					strBuilder.AppendLine($"{match.Groups["tab"].Value}{nameDescribe}");
			}

			if ((match = actionRegex.Match(line)).Success)
			{
				var actionDescribe = DescribeAction(
					match.Groups["type"].Value,
					match.Groups["message"].Value,
					match.Groups["wParam"].Value,
					match.Groups["lParam"].Value,
					match.Groups["sParam"].Value);

				if (actionDescribe != savedLine)
					strBuilder.AppendLine($"{match.Groups["tab"].Value}{actionDescribe}");
			}

			savedLine = line;
			strBuilder.AppendLine(line);
		});

		return strBuilder.ToString();
	}

}
