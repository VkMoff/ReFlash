using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class EnemyResource : Resource
{
    [Export] public string Name {get; private set;}
    [Export] public Array<EffectResource> InitialActions {get; private set;}
    [Export] public Array<ActionResource> ActionPatterns;
    [Export] public int MaxHP;
    [Export] public SpriteFrames Animation;
    [Export] public PackedScene Visualisation;
}
