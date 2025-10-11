using Godot;
using Godot.Collections;
using System;

public partial class EnemyResource : Resource
{
    public EffectResource[][][] ActionPatterns;
    [Export] public int MaxHP;
    [Export] public SpriteFrames Animation;
}
