using Godot;

[GlobalClass]
public abstract partial class StatusResource : Resource
{
	public Texture2D StatusTexture;
	public string Name {get; protected set;}
	public Color Color {get; protected set;}
	public virtual void OnTurnEnd(Status status, Character[] targets) { }
	public virtual void OnTurnStart(Status status, Character[] targets) { }
	public virtual void OnDamageReceive(Status status, Character receiver, Character dealer) { }
}
