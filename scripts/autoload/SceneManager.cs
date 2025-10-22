using Godot;
using Godot.Collections;
using System;
using System.Formats.Tar;

public partial class SceneManager : Node
{
	public static SceneManager Instance
	{
		get;
		private set;
	}

	PackedScene level = GD.Load<PackedScene>("res://scenes/level.tscn");
	PackedScene startMenu = GD.Load<PackedScene>("res://scenes/start_menu.tscn");

	public override void _Ready()
	{
		if (Instance is null)
		{
			Instance = this;
			ProcessMode = ProcessModeEnum.Always;
		}

	}

	public void LoadLevel()
	{
		GetTree().ChangeSceneToPacked(level);
	}

	public void GoToMenu()
	{
		GetTree().ChangeSceneToPacked(startMenu);
	}

}
