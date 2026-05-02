using Godot;
using System;

public partial class SettingsMenu : CanvasLayer
{
	public bool DebugMode { get; private set; }

	public override void _Ready()
	{
		GetNode<CheckButton>("%DebugModeCheck").SetPressedNoSignal(Convert.ToBoolean(SettingsManager.Instance.GetSetting("debug_mode").AsString())); //Variant.ToBool() не работал
	}

	public void SetDebugMode(bool enabled)
	{
		DebugMode = enabled;
		SettingsManager.Instance.SetSetting("debug_mode", enabled.ToString());
	}
	public void Close()
	{
		Visible = false;
		ProcessMode = ProcessModeEnum.Disabled;
	}
	public void Save()
	{
		SettingsManager.Instance.Save();
	}
}
