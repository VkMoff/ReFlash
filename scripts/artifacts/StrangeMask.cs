using Godot;

[GlobalClass]
public partial class StrangeMask : ArtifactResource
{
    public override void Init()
    {
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_strangemask.png");
        Name = "Странная маска";
        Description = "В начале битвы возьмите на 2 карты больше";
    }

    public override void Load(Level level)
    {
        Init();
        
        level.BattleStart += () =>
        {
            level.PullCardFromDeck(2);
        };
    }
}