using System;
using Godot;

public partial class MapMenu : Control
{
	PackedScene levelButtonScene = GD.Load<PackedScene>("res://scenes/ui/level_button.tscn");
	PackedScene bossButtonScene = GD.Load<PackedScene>("res://scenes/ui/boss_button.tscn");
	HBoxContainer levelsContainer;
	int currentLevelIdx = 0;
	RichTextLabel levelDescription;
	LevelButton selectedLevel;
	public override void _Ready()
	{
		Random r = new();
		levelsContainer = GetNode<HBoxContainer>("%MapLevelsContainer");
		levelDescription = GetNode<RichTextLabel>("%LevelDescription");

		int sinceLastShop = 0;
		int sinceLastRandom = 0;
		for (int i = 0; i < 20; i++)
		{
			VBoxContainer selectContainer = new VBoxContainer();
			selectContainer.SizeFlagsHorizontal = SizeFlags.ExpandFill;
			levelsContainer.AddChild(selectContainer);
			bool hasShop = false;
			for (int j = 0; j < 3; j++)
			{
				LevelButton levelButton = levelButtonScene.Instantiate<LevelButton>();

				if (!hasShop && (sinceLastShop > 2) && (r.NextDouble() < 0.2))
				{
					hasShop = true;
					sinceLastShop = 0;
					levelButton.Init(RoomTypes.Shop, RoomRegistry.Instance.SHOP);
				}
				else if ((r.NextDouble() < 0.3) && (sinceLastRandom > 5))
				{
					levelButton.Init(RoomTypes.Unknown, RoomRegistry.Instance.GetEncounter());
					sinceLastRandom = 0;
				}
				else
				{
					levelButton.Init(RoomTypes.EnemyRoom, RoomRegistry.Instance.GetRoom(i, i + 20));
				}	

				levelButton.RoomSelected += (button) =>
				{
					selectedLevel = button;
					ShowDescription(button.EncounterResource.GetDescription());
				};

				selectContainer.AddChild(levelButton);
			}
			if (!hasShop) sinceLastShop++;
			sinceLastRandom++;
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
		else
		{
			SceneManager.Instance.LoadEncounter(RoomRegistry.Instance.GetEncounter().Scene);
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
