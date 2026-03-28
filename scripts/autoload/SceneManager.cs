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
	MapMenu map = GD.Load<PackedScene>("res://scenes/map.tscn").Instantiate<MapMenu>();
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
		map.Hide();
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
		map.Hide();
		GetTree().ChangeSceneToPacked(startMenu);
	}

	public void GoToMap()
	{
		if (map.GetParent() is null)
		{
			GetTree().Root.AddChild(map);
		}
		map.MoveToFront();
		map.Show();
	}
	public void LoadShop()
	{
		map.Hide();
		GetTree().ChangeSceneToPacked(shop);
	}
}
