using Godot;
using System;

public partial class StartMenu : Control
{
	public override void _Ready()
	{
	}
	public void StartGame()
	{
		SceneManager.Instance.GoToMap();
	}
	public void ExitGame()
	{
		GetTree().Quit();
	}
	public void OpenCardEditor()
	{
		SceneManager.Instance.GoToCardEditor();
	}
}
