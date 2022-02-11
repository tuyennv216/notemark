using System.Text;

namespace notemark.Model;

public class Macro
{
	public string Name { get; set; } = string.Empty;
	public bool Ctrl { get; set; }
	public bool Alt { get; set; }
	public bool Shift { get; set; }
	public int Key { get; set; }
	public List<Act> Actions { get; set; } = new();

	public Macro() { }
	public Macro(string name, bool ctrl, bool alt, bool shift, int key)
	{
		Name = name;
		Ctrl = ctrl;
		Alt = alt;
		Shift = shift;
		Key = key;
	}

	public Act AddAction(Act action)
	{
		Actions.Add(action);
		return action;
	}

	private string YesNo(bool var) => var ? "yes" : "no";
	public override string ToString()
	{
		var strBuilder = new StringBuilder();
		strBuilder.Append($"< Macro name = \"{Name}\" Ctrl = \"{YesNo(Ctrl)}\" Alt = \"{YesNo(Alt)}\" Shift = \"{YesNo(Shift)}\" Key = \"{Key}\" >");

		Actions.ForEach(action => strBuilder.Append(action.ToString()));

		strBuilder.Append("</Macro>");
		return strBuilder.ToString();
	}
}
