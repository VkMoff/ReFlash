using Godot;

[GlobalClass]
public partial class PoisonSpray : ArtifactResource
{
    public override void GetTexture()
    {
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_poisonspray.svg");
    }

    public override void Init(Level level)
    {
        GetTexture();
        Name = "Poison Spray";
        Description = "Apply 1 poison on every enemy in the end of turn";
        
        level.TurnStart += () =>
        {
            foreach (Character enemy in level.Enemies)
            {
                enemy.AddStatus(new PoisonStatus(), 2);
            }
        };
    }
}