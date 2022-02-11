using Newtonsoft.Json;
using notemark.Interface;
using notemark.Data.Model;
using notemark.Masks;

namespace notemark.Service;

public class DataService : IDataService
{
	private readonly IConfiguration _configuration;

	private readonly Act[] _actionsType01;
	private readonly Act[] _actionsType2;
	private readonly Act[] _actionsType3;
	private readonly Dictionary<string, Act> _actionsType01AsDictionary;
	private readonly Dictionary<string, Act> _actionsType2AsDictionary;
	private readonly Dictionary<string, Act> _actionsType3AsDictionary;
	private readonly Dictionary<string, KeyInfo> _keyboardDict;

	public DataService(IConfiguration configuration)
	{
		_configuration = configuration;

		// Load Action type 01
		string type01File = File.ReadAllText(@"Data/actions_type01.json");
		_actionsType01 = JsonConvert.DeserializeObject<Act[]>(type01File) ?? Array.Empty<Act>();
		// Load Action type 2
		string type2File = File.ReadAllText(@"Data/actions_type2.json");
		_actionsType2 = JsonConvert.DeserializeObject<Act[]>(type2File) ?? Array.Empty<Act>();
		// Load Action type 3
		string type3File = File.ReadAllText(@"Data/actions_type3.json");
		_actionsType3 = JsonConvert.DeserializeObject<Act[]>(type3File) ?? Array.Empty<Act>();
		// Load key info
		string keymappingFile = File.ReadAllText(@"Data/keyboard_code.json");
		_keyboardDict = JsonConvert.DeserializeObject<Dictionary<string, KeyInfo>>(keymappingFile) ?? new();

		// Set references data
		_actionsType01AsDictionary = MaskDictionary.FromArray(_actionsType01, item => item.Code);
		_actionsType2AsDictionary = MaskDictionary.FromArray(_actionsType2, item => item.Code);
		_actionsType3AsDictionary = MaskDictionary.FromArray(_actionsType3, item => item.Code);
	}

	public Act[] GetActionType01()
	{
		return _actionsType01;
	}

	public Act[] GetActionType2()
	{
		return _actionsType2;
	}

	public Act[] GetActionType3()
	{
		return _actionsType3;
	}

	public Dictionary<string, Act> GetActionType01AsDictionary()
	{
		return _actionsType01AsDictionary;
	}

	public Dictionary<string, Act> GetActionType2AsDictionary()
	{
		return _actionsType2AsDictionary;
	}

	public Dictionary<string, Act> GetActionType3AsDictionary()
	{
		return _actionsType3AsDictionary;
	}

	public Dictionary<string, KeyInfo> GetKeyboardDictionary()
	{
		return _keyboardDict;
	}
}
