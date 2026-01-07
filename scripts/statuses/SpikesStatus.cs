using Godot;

[GlobalClass]
public partial class SpikesStatus : StatusResource
{
    public SpikesStatus()
    {
        StatusTexture = GD.Load<Texture2D>("res://resources/sprites/statuses/status_spikes.svg");
        Name = "Шипы";
    }
    public override void OnDamageReceive(Status status, Character receiver, Character dealer)
    {
        dealer.ChangeHP(-status.Value);
    }
}