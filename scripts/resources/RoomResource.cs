using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class RoomResource : EncounterResource
{
    [Export] public Array<EnemyResource> Enemies;
    [Export] public int Difficulty;
}
