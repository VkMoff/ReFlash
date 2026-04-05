using Godot;
using System;

public partial class StartMenu : Control
{
	public override void _Ready()
	{
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
}
