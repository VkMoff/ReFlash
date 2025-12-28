using Godot;

[GlobalClass]
public abstract partial class StatusResource : Resource
{
	public Texture2D StatusTexture;
	public virtual void OnTurnEnd(Status status, Character[] targets) { }
	public virtual void OnDamageReceive(Status status, Character receiver, Character dealer) { }
}
