using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{
	public void Continue()
	{
		GetTree().Paused = false;
		QueueFree();
	}
	public void ExitToMenu()
	{
		SceneManager.Instance.GoToMenu();
		GetTree().Paused = false;
		QueueFree();
	}
	public void ExitToDesktop()
	{
		GetTree().Quit();
	}
}
