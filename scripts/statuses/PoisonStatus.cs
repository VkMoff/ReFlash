using Godot;

[GlobalClass]
public partial class PoisonStatus : StatusResource
{
    public PoisonStatus()
    {
        StatusTexture = GD.Load<Texture2D>("res://resources/sprites/statuses/status_poison.svg");
    }
    public override void OnTurnEnd()
    {
        status.ParentCharacter.ChangeHP(-status.Value);
        status.Value -= 1;
        
        if (status.Value <= 0)
        {
            status.Remove();
        }
    }
}