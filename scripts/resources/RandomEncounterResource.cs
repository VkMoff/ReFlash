using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class RandomEncounterResource : EncounterResource
{
    [Export] public PackedScene Scene;
    public override string GetDescription() {return "[font=res://resources/styles/Play-Regular.ttf][font_size=48][outline_size=2]Случайное событие";}
}
