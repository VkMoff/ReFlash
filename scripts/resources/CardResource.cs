using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class CardResource : Resource
{
	[Export] public string Id;
	[Export] public string Name;
	[Export] public Texture2D Texture;
	[Export] public Array<EffectResource> Effects;
	[Export] public bool IsTargeted;
	[Export] public int Cost;
}
