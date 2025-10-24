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
	RoomResource testRoom = GD.Load<RoomResource>("res://resources/rooms/test_room.tres");

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
		CallDeferred(MethodName.DeferredLoadLevel);
	}

	private void DeferredLoadLevel()
	{
		GetTree().CurrentScene.Free();
		Level nextLevel = level.Instantiate<Level>();
		GetTree().Root.AddChild(nextLevel);
		GetTree().CurrentScene = nextLevel;
		nextLevel.InitRoom(testRoom);
	}

	public void GoToMenu()
	{
		GetTree().ChangeSceneToPacked(startMenu);
	}

}
