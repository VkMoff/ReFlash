using Godot;
using System;

public partial class StartMenu : Control
{
	private SettingsMenu settingsMenu;
	public override void _Ready()
	{
		settingsMenu = GetNode<SettingsMenu>("%SettingsMenu");
	}
	public void StartGame()
	{
		Visible = false;
		SceneManager.Instance.GoToMap();
	}
	public void ExitGame()
	{
		GetTree().Quit();
	}
	public void ShowSettings()
	{
		settingsMenu.Visible = true;
		settingsMenu.ProcessMode = ProcessModeEnum.Always;
	}
}
