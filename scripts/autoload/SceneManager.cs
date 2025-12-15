using Godot;

public partial class SceneManager : Node
{
	public static SceneManager Instance
	{
		get;
		private set;
	}

	PackedScene level = GD.Load<PackedScene>("res://scenes/level.tscn");
	PackedScene startMenu = GD.Load<PackedScene>("res://scenes/start_menu.tscn");
	RoomResource testRoom = GD.Load<RoomResource>("res://resources/encounters/rooms/test_room.tres");
	PackedScene map = GD.Load<PackedScene>("res://scenes/map.tscn");
	PackedScene shop = GD.Load<PackedScene>("res://scenes/shop.tscn");

	public override void _Ready()
	{
		if (Instance is null)
		{
			Instance = this;
			ProcessMode = ProcessModeEnum.Always;
		}
	}

	public void LoadLevel(RoomResource roomResource)
	{
		CallDeferred(MethodName.DeferredLoadLevel, roomResource);
	}

	private void DeferredLoadLevel(RoomResource roomResource)
	{
		GetTree().CurrentScene.Free();
		Level nextLevel = level.Instantiate<Level>();
		GetTree().Root.AddChild(nextLevel);
		GetTree().CurrentScene = nextLevel;
		nextLevel.InitRoom(roomResource);
	}

	public void GoToMenu()
	{
		GetTree().ChangeSceneToPacked(startMenu);
	}

	public void GoToMap()
	{
		GetTree().ChangeSceneToPacked(map);
	}
	public void LoadShop()
		{
				GetTree().ChangeSceneToPacked(shop);
		}
}
