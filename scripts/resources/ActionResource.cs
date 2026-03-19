using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class ActionResource : Resource
{
	[Export] public Array<EffectResource> Effects;
}
