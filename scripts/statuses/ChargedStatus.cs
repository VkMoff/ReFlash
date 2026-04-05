using Godot;

[GlobalClass]
public partial class ChargedStatus : StatusResource
{
    public ChargedStatus()
    {
        StatusTexture = GD.Load<Texture2D>("res://resources/sprites/statuses/status_charged.svg");
        Name = "Заряжен";
        Color = new("01E101");
    }
}