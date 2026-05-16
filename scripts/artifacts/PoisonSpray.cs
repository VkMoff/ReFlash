using Godot;

[GlobalClass]
public partial class PoisonSpray : ArtifactResource
{
    public PoisonSpray()
    {
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_poisonspray.svg");
        Name = "Распылитель яда";
        Description = "Накладывает 2 яда на всех врагов в начале хода";
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