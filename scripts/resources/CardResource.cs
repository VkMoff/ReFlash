using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class CardResource : Resource
{
	[Export] public string Name;
	[Export] public string Description;
	[Export] public Texture2D Texture;
	[Export] public Array<EffectResource> Effects;
	[Export] public bool IsTargeted;
	[Export] public int Cost;
}
