using Godot;
using Godot.Collections;
using System;

public partial class EnemyResource : Resource
{
    [Export] public string Name {get; private set;}
    public EffectResource[][][] ActionPatterns;
    [Export] public int MaxHP;
    [Export] public SpriteFrames Animation;
}
