using Godot;

[GlobalClass]
public abstract partial class StatusResource : Resource
{
	protected Status status;
	public Texture2D StatusTexture;
	public void SetStatusInstance(Status status)
    {
		this.status = status;
    }
	public virtual void OnTurnEnd() { }
}
