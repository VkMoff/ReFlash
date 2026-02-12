using Godot;
using System;

public partial class ShopItemButton : Button
{
	[Export] public ArtifactResource artifact;
	[Signal] public delegate void ClickedEventHandler(ArtifactResource artifact);
	public override void _Ready()
	{
		artifact.Init();
		Icon = artifact.ArtifactTexture;
		Pressed += () =>
		{
			//Добавить проверку на количество золота
			EmitSignal(SignalName.Clicked, artifact);
		};
	}
}
