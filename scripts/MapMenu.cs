using Godot;
using System;

public partial class MapMenu : Control
{
	PackedScene levelButtonScene = GD.Load<PackedScene>("res://scenes/level_button.tscn");
	PackedScene bossButtonScene = GD.Load<PackedScene>("res://scenes/boss_button.tscn");

	RoomResource testRoom = GD.Load<RoomResource>("res://resources/encounters/rooms/test_room.tres");
	HBoxContainer levelsContainer;
	int currentLevelIdx = 0;

	public override void _Ready()
	{
		levelsContainer = GetNode<HBoxContainer>("%MapLevelsContainer");

		for (int i = 0; i < 20; i++)
		{
			VBoxContainer selectContainer = new VBoxContainer();
			levelsContainer.AddChild(selectContainer);

			LevelButton levelButton = levelButtonScene.Instantiate<LevelButton>();
			levelButton.Init(RoomTypes.EnemyRoom, testRoom);
			
			LevelButton levelButton1 = levelButtonScene.Instantiate<LevelButton>();
			levelButton1.Init(RoomTypes.EnemyRoom, testRoom);

			LevelButton shopButton2 = levelButtonScene.Instantiate<LevelButton>();
			shopButton2.Init(RoomTypes.Shop, GD.Load<ShopResource>("res://resources/encounters/shops/test_shop.tres"));

			selectContainer.AddChild(levelButton);
			selectContainer.AddChild(levelButton1);
			selectContainer.AddChild(shopButton2);
		}
		foreach (VBoxContainer vBoxContainer in levelsContainer.GetChildren())
		{
			if (vBoxContainer == levelsContainer.GetChild(0)) continue;
			foreach (LevelButton levelButton in vBoxContainer.GetChildren())
			{
				levelButton.Disabled = true;
			}
		}
		levelsContainer.AddChild(bossButtonScene.Instantiate());
	}

	public void StartEncounter(EncounterResource encounterResource)
	{
		foreach (LevelButton levelButton in levelsContainer.GetChild(currentLevelIdx).GetChildren())
		{
			levelButton.Disabled = true;
		}
		currentLevelIdx++;
		foreach (LevelButton levelButton in levelsContainer.GetChild(currentLevelIdx).GetChildren())
		{
			levelButton.Disabled = false;
		}

		if (encounterResource is RoomResource roomResource)
		{
			SceneManager.Instance.LoadLevel(roomResource);
		}
		else if (encounterResource is ShopResource)
		{
			SceneManager.Instance.LoadShop();
		}
	}
}
