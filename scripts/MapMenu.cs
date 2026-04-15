using Godot;

public partial class MapMenu : Control
{
	PackedScene levelButtonScene = GD.Load<PackedScene>("res://scenes/level_button.tscn");
	PackedScene bossButtonScene = GD.Load<PackedScene>("res://scenes/boss_button.tscn");
	RoomResource testRoom = GD.Load<RoomResource>("res://resources/encounters/rooms/test_room.tres");
	HBoxContainer levelsContainer;
	int currentLevelIdx = 0;
	RichTextLabel levelDescription;
	LevelButton selectedLevel;
	public override void _Ready()
	{
		levelsContainer = GetNode<HBoxContainer>("%MapLevelsContainer");
		levelDescription = GetNode<RichTextLabel>("%LevelDescription");

		for (int i = 0; i < 20; i++)
		{
			VBoxContainer selectContainer = new VBoxContainer();
			selectContainer.SizeFlagsHorizontal = SizeFlags.ExpandFill;
			levelsContainer.AddChild(selectContainer);

			LevelButton levelButton = levelButtonScene.Instantiate<LevelButton>();
			levelButton.Init(RoomTypes.EnemyRoom, testRoom);
			levelButton.RoomSelected += (button) =>
			{
				selectedLevel = button;
				ShowDescription(button.EncounterResource.GetDescription());
			};
			LevelButton levelButton1 = levelButtonScene.Instantiate<LevelButton>();
			levelButton1.Init(RoomTypes.EnemyRoom, testRoom);
			levelButton1.RoomSelected += (button) =>
			{
				selectedLevel = button;
				ShowDescription(button.EncounterResource.GetDescription());
			};
			LevelButton shopButton2 = levelButtonScene.Instantiate<LevelButton>();
			shopButton2.Init(RoomTypes.Shop, GD.Load<ShopResource>("res://resources/encounters/shops/test_shop.tres"));
			shopButton2.RoomSelected += (button) => //А нафига?
			{
				selectedLevel = button;
				ShowDescription(button.EncounterResource.GetDescription());
			};
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
		Button bossButton = bossButtonScene.Instantiate<Button>();
		levelsContainer.AddChild(bossButton);
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
	public void StartEncounter()
	{
		selectedLevel.Start();
		levelDescription.Text = string.Empty;
		StartEncounter(selectedLevel.EncounterResource);
		selectedLevel = null;
	}
	public void ShowDescription(string description)
	{
		levelDescription.Text = description;
	}
}
