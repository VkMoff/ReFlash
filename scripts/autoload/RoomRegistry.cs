using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class RoomRegistry : Node
{
	public static RoomRegistry Instance;
	private List<RoomResource> rooms = new();
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
	}
	public RoomResource GetRoom(int fromDifficulty, int toDifficulty)
	{
		Random r = new();
		RoomResource[] selectedRooms = rooms.Where(r => (r.Difficulty >= fromDifficulty) && (r.Difficulty <= toDifficulty)).ToArray();
		return selectedRooms[r.Next() % selectedRooms.Length];
	}
}
