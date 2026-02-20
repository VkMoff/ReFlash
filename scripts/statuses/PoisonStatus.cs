using Godot;

[GlobalClass]
public partial class PoisonStatus : StatusResource
{
	public PoisonStatus()
	{
		StatusTexture = GD.Load<Texture2D>("res://resources/sprites/statuses/status_poison.svg");
		Name = "Яд";
		Color = new("01E101");
	}
	public override void OnTurnEnd(Status status, Character[] targets)
	{
		foreach (Character target in targets)
		{
			target.ChangeHP(-status.Value);
		}
		status.Value -= 1;
		
		if (status.Value <= 0)
		{
			// GD.Print($"Poison calling status.Remove");
			status.Remove();
		}
	}
}
