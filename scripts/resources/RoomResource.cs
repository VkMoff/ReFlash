using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class RoomResource : EncounterResource
{
    [Export] public Array<EnemyResource> Enemies;
    [Export] public int Difficulty;
    public override string GetDescription()
    {
        if (Difficulty < 10) return RoomDifficultyDescription.EASY;
        else if (Difficulty < 20) return RoomDifficultyDescription.NORMAL;
        else if (Difficulty < 30) return RoomDifficultyDescription.HARD;
        else return RoomDifficultyDescription.ELITE;
    }
}
