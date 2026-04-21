using Godot;

[GlobalClass]
public partial class WeaknessStatus : StatusResource
{
	public WeaknessStatus()
	{
		StatusTexture = GD.Load<Texture2D>("res://resources/sprites/statuses/status_weakness.svg");
		Name = "Слабость";
		Color = new("0BC2D6");
	}
	public override void OnTurnEnd(Status status, Character[] targets)
	{
		status.Value -= 1;
		
		if (status.Value <= 0)
		{
			status.Remove();
			status.ParentCharacter.RecalculateStrength();
		}
	}
}
