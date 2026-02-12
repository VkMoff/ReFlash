using Godot;

[GlobalClass]
public partial class HourGlass : ArtifactResource
{
    public override void Init()
    {
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_hourglass.svg");
        Name = "Hourglass";
        Description = "Deal 3 Damage to all Enemies on the end of your turn";
    }

    public override void Load(Level level)
    {
        Init();
        
        level.TurnEnd += () =>
        {
            foreach (Character enemy in level.Enemies)
            {
                enemy.ChangeHP(-3);
            }
        };
    }
}