using Godot;

[GlobalClass]
public partial class BarbedWire : ArtifactResource
{
    public override void Init()
    {
        ArtifactTexture = GD.Load<Texture2D>("res://resources/sprites/artifacts/artifact_barbedwire.svg");
        Name = "Barbed Wire";
        Description = "Give 3 spikes at the beginning of battle";
    }

    public override void Load(Level level)
    {
        Init();
        
        level.BattleStart += () =>
        {
            level.Player.AddStatus(new SpikesStatus(), 3);
        };
    }
}