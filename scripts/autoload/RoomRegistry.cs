using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class RoomRegistry : Node
{
	public static RoomRegistry Instance;
	private List<RoomResource> rooms = new();
	private List<RandomEncounterResource> encounters = new();
	public readonly ShopResource SHOP = GD.Load<ShopResource>("res://resources/encounters/shops/test_shop.tres");
	public override void _Ready()
	{
		Instance = this;
		ProcessMode = ProcessModeEnum.Always;

		rooms.Add(GD.Load<RoomResource>("res://resources/encounters/rooms/watcher_room.tres"));
		rooms.Add(GD.Load<RoomResource>("res://resources/encounters/rooms/shot_room.tres"));
		rooms.Add(GD.Load<RoomResource>("res://resources/encounters/rooms/test_room.tres"));
		rooms.Add(GD.Load<RoomResource>("res://resources/encounters/rooms/guardian_room.tres"));
		rooms.Add(GD.Load<RoomResource>("res://resources/encounters/rooms/guardians_room.tres"));
		rooms.Add(GD.Load<RoomResource>("res://resources/encounters/rooms/scavenger_room.tres"));
		rooms.Add(GD.Load<RoomResource>("res://resources/encounters/rooms/scavenger_stool_room.tres"));
		rooms.Add(GD.Load<RoomResource>("res://resources/encounters/rooms/scorpispikes_room.tres"));

		encounters.Add(GD.Load<RandomEncounterResource>("res://resources/encounters/memo.tres"));
	}
	public RoomResource GetRoom(int fromDifficulty, int toDifficulty)
	{
		Random r = new();
		RoomResource[] selectedRooms = rooms.Where(r => (r.Difficulty >= fromDifficulty) && (r.Difficulty <= toDifficulty)).ToArray();
		if (selectedRooms.Length == 0) return rooms[0];
		return selectedRooms[r.Next() % selectedRooms.Length];
	}
	public RandomEncounterResource GetEncounter()
	{
		return encounters[0]; //ЗАГЛУШКА
	}
}
