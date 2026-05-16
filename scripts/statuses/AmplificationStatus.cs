using Godot;

[GlobalClass]
public partial class AmplificationStatus : StatusResource
{
    public AmplificationStatus()
    {
        StatusTexture = GD.Load<Texture2D>("res://resources/sprites/statuses/status_amlplifcation.svg");
        Name = "Услиление";
        Description = "Увеличивает силу на 1 в конце хода";
        Color = new("0BC2D6");
    }
    public override void OnTurnEnd(Status status, Character[] targets)
    {
        status.ParentCharacter.AddStatus(new StrengthStatus(), status.Value);
    }
}