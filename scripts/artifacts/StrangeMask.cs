using Godot;

[GlobalClass]
public partial class StrangeMask : ArtifactResource
{
    public StrangeMask()
    {
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_strangemask.png");
        Name = "Странная маска";
        Description = "В начале битвы возьмите на 2 карты больше";
    }

    public override void Load(Level level)
    {
        level.BattleStart += () =>
        {
            level.PullCardFromDeck(2);
        };
    }
}