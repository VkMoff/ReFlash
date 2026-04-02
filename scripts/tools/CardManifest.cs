using Godot;
using System;

[Tool][GlobalClass]
public partial class CardManifest : Resource
{
    [Export] public string[] CardPaths = Array.Empty<string>();
}