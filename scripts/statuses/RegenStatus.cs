using Godot;

[GlobalClass]
public partial class RegenStatus : StatusResource
{
    public RegenStatus()
    {
        StatusTexture = GD.Load<Texture2D>("res://resources/sprites/statuses/status_regen.svg");
        Name = "Регенерация";
    }
    public override void OnTurnEnd(Status status, Character[] targets)
    {
        //избыточно?
        status.ParentCharacter.ChangeHP(status.Value);
        status.Value -= 1;

        if (status.Value <= 0)
        {
            status.Remove();
        }
    }
}