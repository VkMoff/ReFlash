using Godot;
using System;

public partial class FailMenu : CanvasLayer
{
	public void GoToMenu()
	{
		SceneManager.Instance.GoToMenu();
		QueueFree();
	}
}
