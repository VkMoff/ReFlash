using Godot;

[GlobalClass]
public partial class PoisonSpray : ArtifactResource
{
    public PoisonSpray()
    {
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_poisonspray.svg");
        Name = "Распылитель яда";
        Description = "Apply 1 poison on every enemy in the end of turn";
    }

    public override void Load(Level level)
    {
        level.TurnStart += () =>
        {
            foreach (Character enemy in level.Enemies)
            {
                enemy.AddStatus(new PoisonStatus(), 2);
            }
        };
    }
}