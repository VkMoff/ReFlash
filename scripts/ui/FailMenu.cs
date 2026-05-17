using Godot;
using System;

public partial class FailMenu : CanvasLayer
{
	public override void _Ready()
	{
		GetNode<Label>("%ResultMessageLabel").Text += $"\nВы набрали {PlayerData.Instance.Score} очков";
	}
	public void GoToMenu()
	{
		SceneManager.Instance.GoToMenu();
		QueueFree();
	}
}
