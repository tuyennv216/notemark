namespace notemark.Model;

public class Act
{
	public int Type { get; set; }
	public int Message { get; set; }
	public int wParam { get; set; }
	public int lParam { get; set; }
	public string sParam { get; set; } = string.Empty;

	public Act() { }
	public Act(int type, int message, int wParam, int lParam, string sParam)
	{
		Type = type;
		Message = message;
		this.wParam = wParam;
		this.lParam = lParam;
		this.sParam = sParam;
	}

	public override string ToString()
	{
		var str = $"<Action type=\"{Type}\" message=\"{Message}\" wParam=\"{wParam}\" lParam=\"{lParam}\" sParam=\"{sParam}\" />";
		return str;
	}
}
