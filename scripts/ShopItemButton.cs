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
			this.ShowMessage(artifact.Name, GlobalPosition + Size / 2 + Vector2.Up * Size.Y / 2);
			//Добавить проверку на количество золота
			EmitSignal(SignalName.Clicked, artifact);
		};
	}
}
