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

		GetNode("MapLevels/LevelSelectContainer").AddChild(levelButton);
		GetNode("MapLevels/LevelSelectContainer").AddChild(shopButton);
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
