using Godot;

public partial class ArtifactResource : Resource
{
    public Texture2D ArtifactTexture;
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public virtual void Init(Level level) { }
    public virtual void GetTexture() {}
}