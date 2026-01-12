using Godot;

public partial class HourGlass : ArtifactResource
{
    public override void Init(Level level)
    {
        Name = "Hourglass";
        Description = "Deal 3 Damage to all Enemies on the end of your turn";
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_hourglass.svg");
        level.TurnEnd += () =>
        {
            foreach (Character enemy in level.Enemies)
            {
                enemy.ChangeHP(-3);
            }
        };
    }
}