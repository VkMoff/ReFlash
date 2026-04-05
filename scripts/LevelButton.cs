using Godot;
using System;

public partial class LevelButton : Button
{
	static Texture2D enemyIcon = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_sword.svg");
	static Texture2D enemyIconCompleted = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_sword_completed.svg");
	static Texture2D shopIcon = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_shop.svg");
	static Texture2D shopIconCompleted = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_shop_completed.svg");
	MapMenu mapMenu;
	RoomTypes roomType;
	public EncounterResource EncounterResource { get; private set; }
	public event Action<LevelButton> RoomSelected;
	public void Init(RoomTypes roomType, EncounterResource encounterResource)
	{
		this.roomType = roomType;
		this.EncounterResource = encounterResource;
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
		Pressed += () =>
		{
			RoomSelected?.Invoke(this);
		};
	}
	public void Start()
	{
		switch (roomType)
		{
			case RoomTypes.EnemyRoom:
				Icon = enemyIconCompleted;
				break;
			case RoomTypes.Shop:
				Icon = shopIconCompleted;
				break;
			default:
				GD.Print("WTF");
				break;
		}
	}
}

public enum RoomTypes
{
	EnemyRoom,
	Shop,
	EliteEnemyRoom,
	Unknown
}
