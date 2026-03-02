using Godot;

[GlobalClass]
public partial class AccelerationStatus : StatusResource
{
    public AccelerationStatus()
    {
        StatusTexture = GD.Load<Texture2D>("res://resources/sprites/statuses/status_acceleration.svg");
        Name = "Ускорение"; // В следующем ходу возьмите на Х карт больше
        Color = new("01E101");
    }
    public override void OnTurnStart(Status status, Character[] targets)
    {
        status.ParentCharacter.Level.PullCardFromDeck(status.Value);
        status.Remove();
    }
}