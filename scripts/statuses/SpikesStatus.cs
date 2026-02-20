using Godot;

[GlobalClass]
public partial class SpikesStatus : StatusResource
{
    public SpikesStatus()
    {
        StatusTexture = GD.Load<Texture2D>("res://resources/sprites/statuses/status_spikes.svg");
        Name = "Шипы";
        Color = new("6D8E92");
    }
    public override void OnDamageReceive(Status status, Character receiver, Character dealer)
    {
        dealer.ChangeHP(-status.Value);
    }
}