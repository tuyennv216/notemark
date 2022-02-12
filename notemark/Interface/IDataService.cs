using notemark.Data.Model;

namespace notemark.Interface;

public interface IDataService
{
	public Act[] GetActionType01();
	public Act[] GetActionType2();
	public Act[] GetActionType3();
	public Dictionary<string, Act> GetActionType01AsDictionary();
	public Dictionary<string, Act> GetActionType2AsDictionary();
	public Dictionary<string, Act> GetActionType3AsDictionary();
	public Dictionary<string, KeyInfo> GetKeyboardDictionary();

	public string GetEndpoints();
}
