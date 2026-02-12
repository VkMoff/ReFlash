using Godot;

[GlobalClass]
public partial class HourGlass : ArtifactResource
{
    public override void GetTexture()
    {
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_hourglass.svg");
    }

    public override void Init(Level level)
    {
        GetTexture();
        Name = "Hourglass";
        Description = "Deal 3 Damage to all Enemies on the end of your turn";
        level.TurnEnd += () =>
        {
            foreach (Character enemy in level.Enemies)
            {
                enemy.ChangeHP(-3);
            }
        };
    }
}