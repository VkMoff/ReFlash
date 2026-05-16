using Godot;
using System;

public partial class LevelButton : Button
{
	static Texture2D enemyIcon = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_sword.svg");
	static Texture2D enemyIconCompleted = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_sword_completed.svg");
	static Texture2D shopIcon = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_shop.svg");
	static Texture2D shopIconCompleted = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_shop_completed.svg");
	static Texture2D randomIcon = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_random.svg");
	static Texture2D randomIconCompleted = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_random_completed.svg");
	static Texture2D eliteIcon = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_elite.svg");
	static Texture2D eliteIconCompleted = GD.Load<Texture2D>("res://resources/sprites/level_buttons/icon_level_elite_inactive.svg");
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
				if (encounterResource is RoomResource room && room.Difficulty >= 30) Icon = eliteIcon;
				else Icon = enemyIcon; 
				break;
			case RoomTypes.Shop:
				Icon = shopIcon;
				break;
			case RoomTypes.Unknown:
				Icon = randomIcon;
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
			case RoomTypes.Unknown:
				Icon = randomIconCompleted;
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
