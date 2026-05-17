using Godot;

public partial class SceneManager : Node
{
	public static SceneManager Instance
	{
		get;
		private set;
	}

	PackedScene level = GD.Load<PackedScene>("res://scenes/level.tscn");
	PackedScene startMenu = GD.Load<PackedScene>("res://scenes/ui/start_menu.tscn");
	RoomResource testRoom = GD.Load<RoomResource>("res://resources/encounters/rooms/test_room.tres");
	PackedScene mapScene = GD.Load<PackedScene>("res://scenes/map.tscn");
	MapMenu map;
	PackedScene shop = GD.Load<PackedScene>("res://scenes/shop.tscn");

	public override void _Ready()
	{
		if (Instance is null)
		{
			Instance = this;
			ProcessMode = ProcessModeEnum.Always;
		}
	}

	public void LoadLevel(RoomResource roomResource, bool isBoss = false)
	{
		map.Hide();
		map.ProcessMode = ProcessModeEnum.Disabled;
		CallDeferred(MethodName.DeferredLoadLevel, roomResource, isBoss);
	}

	private void DeferredLoadLevel(RoomResource roomResource, bool isBoss = false)
	{
		GetTree().CurrentScene.Free();
		Level nextLevel = level.Instantiate<Level>();
		GetTree().Root.AddChild(nextLevel);
		GetTree().CurrentScene = nextLevel;
		nextLevel.InitRoom(roomResource, isBoss);
	}

	public void GoToMenu()
	{
		map.QueueFree();
		GetTree().ChangeSceneToPacked(startMenu);
	}

	public void GoToMap()
	{
		if (!IsInstanceValid(map)) map = mapScene.Instantiate<MapMenu>();
		if (map.GetParent() is null)
		{
			GetTree().Root.AddChild(map);
		}
		(GetTree().CurrentScene as CanvasItem).Visible = false;
		map.MoveToFront();
		map.ProcessMode = ProcessModeEnum.Inherit;
		map.Show();
	}
	public void LoadShop()
	{
		map.Hide();
		GetTree().ChangeSceneToPacked(shop);
	}
	public void LoadEncounter(PackedScene encounter)
	{
		map.Hide();
		GetTree().ChangeSceneToPacked(encounter);
	}
}
