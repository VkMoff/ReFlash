using Godot;

public partial class PoisonSpray : ArtifactResource
{
    public override void Init(Level level)
    {
        Name = "Poison Spray";
        Description = "Apply 1 poison on every enemy in the end of turn";
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_poisonspray.svg");
        level.TurnStart += () =>
        {
            foreach (Character enemy in level.Enemies)
            {
                enemy.AddStatus(new PoisonStatus(), 2);
            }
        };
    }
}