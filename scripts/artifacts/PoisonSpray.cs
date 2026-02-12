using Godot;

[GlobalClass]
public partial class PoisonSpray : ArtifactResource
{
    public override void Init()
    {
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_poisonspray.svg");
        Name = "Poison Spray";
        Description = "Apply 1 poison on every enemy in the end of turn";
    }

    public override void Load(Level level)
    {
        Init();
        
        
        level.TurnStart += () =>
        {
            foreach (Character enemy in level.Enemies)
            {
                enemy.AddStatus(new PoisonStatus(), 2);
            }
        };
    }
}