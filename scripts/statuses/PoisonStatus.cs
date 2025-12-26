using Godot;

[GlobalClass]
public partial class PoisonStatus : StatusResource
{
    public override void OnTurnEnd()
    {
        status.ParentCharacter.ChangeHP(-status.Value);
        status.Value -= 1;
    }
}