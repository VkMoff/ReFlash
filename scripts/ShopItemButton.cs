using Godot;
using System;

public partial class ShopItemButton : Button
{
	[Export] public ArtifactResource Artifact;
	[Export] public int cost;
	[Signal] public delegate void ClickedEventHandler(ArtifactResource artifact);
	public override void _Ready()
	{
		Icon = Artifact.ArtifactTexture;
		Text = cost.ToString();
		Pressed += () =>
		{
			if (PlayerData.Instance.Gold < cost)
			{
				this.ShowMessage("Недостаточно золота", GlobalPosition + Size / 2 + Vector2.Up * Size.Y / 2);
				return;
			}
			this.ShowMessage($"Куплен {Artifact.Name}", GlobalPosition + Size / 2 + Vector2.Up * Size.Y / 2);
			PlayerData.Instance.AddGold(-cost);
			EmitSignal(SignalName.Clicked, Artifact);
			Disabled = true;
		};
	}
}
