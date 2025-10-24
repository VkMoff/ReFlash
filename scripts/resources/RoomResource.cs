using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class RoomResource : Resource
{
    [Export] public Array<EnemyResource> Enemies;
}
