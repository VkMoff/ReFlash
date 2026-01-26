using Godot;
using System;

public partial class MapMenu : Control
{
	PackedScene levelButtonScene = GD.Load<PackedScene>("res://scenes/level_button.tscn");
	public override void _Ready()
	{
		LevelButton levelButton = levelButtonScene.Instantiate<LevelButton>();
		levelButton.Init(RoomTypes.EnemyRoom, GD.Load<RoomResource>("res://resources/encounters/rooms/test_room.tres"));

		LevelButton shopButton = levelButtonScene.Instantiate<LevelButton>();
		shopButton.Init(RoomTypes.Shop, GD.Load<ShopResource>("res://resources/encounters/shops/test_shop.tres"));

		LevelButton shopButton2 = levelButtonScene.Instantiate<LevelButton>();
		shopButton2.Init(RoomTypes.Shop, GD.Load<ShopResource>("res://resources/encounters/shops/test_shop.tres"));

		GetNode("LevelsBackground/MapLevels/LevelSelectContainer").AddChild(levelButton);
		GetNode("LevelsBackground/MapLevels/LevelSelectContainer").AddChild(shopButton);
		GetNode("LevelsBackground/MapLevels/LevelSelectContainer").AddChild(shopButton2);
	}

	public void StartEncounter(EncounterResource encounterResource)
	{
		if (encounterResource is RoomResource)
		{
			SceneManager.Instance.LoadLevel(encounterResource as RoomResource);
		}
		else if (encounterResource is ShopResource)
		{
			SceneManager.Instance.LoadShop();
		}
	}
}
