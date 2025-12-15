using Godot;
using System;

public partial class LevelButton : Button
{
	Texture2D enemyIcon = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_sword.svg");
	Texture2D shopIcon = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_shop.svg");
	MapMenu mapMenu;
	RoomTypes roomType;
	EncounterResource encounterResource;
	public void Init(RoomTypes roomType, EncounterResource encounterResource)
	{
		this.roomType = roomType;
		this.encounterResource = encounterResource;
		switch (roomType)
		{
			case RoomTypes.EnemyRoom:
				Icon = enemyIcon;
				break;
			case RoomTypes.Shop:
				Icon = shopIcon;
				break;
			default:
				GD.Print("WTF");
				break;
		}
	}
	public override void _Ready()
	{
		mapMenu = GetNode<MapMenu>("../../..");
		Pressed += () =>
		{
			mapMenu.StartEncounter(encounterResource);
		};
	}
}

public enum RoomTypes
{
	EnemyRoom,
	Shop,
	EliteEnemyRoom,
	Unknown
}
