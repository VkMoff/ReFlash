using Godot;

[GlobalClass]
public partial class BarbedWire : ArtifactResource
{
    public BarbedWire()
    {
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_barbedwire.svg");
        Name = "Колючая проволока";
        Description = "Give 3 spikes at the beginning of battle";
    }

    public override void Load(Level level)
    {
        level.BattleStart += () =>
        {
            level.Player.AddStatus(new SpikesStatus(), 3);
        };
    }
}