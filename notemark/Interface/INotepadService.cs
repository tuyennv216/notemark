using notemark.Model;

namespace notemark.Interface;

public interface INotepadService
{
	public Macro NewMacro(string name, bool control, bool alt, bool shift, int key);
	public Act NewAction(int type, int message, int wParam, int lParam, string sParam);
	public Act NewAction(int type, int message, int param1, string param2);

	public string NewMacroAsString(string name, bool control, bool alt, bool shift, int key);
	public string NewActionAsString(int type, int message, int wParam, int lParam, string sParam);
	public string NewActionAsString(int type, int message, int param1, string param2);

	public string EscapeXmlString(string str);

	public string DescribeName(string name, string ctrl, string alt, string shift, string key);
	public string DescribeAction(string type, string message, string wParam, string lParam, string sParam);
	public string DescribeMacro(string encodedMacro);
}
