using Godot;
using System;

public partial class ArtifactButton : Button
{
	public ArtifactResource ArtifactResource;
	public event Action<ArtifactResource> AddArtifact;
	public override void _Pressed()
	{
		base._Pressed();
		AddArtifact?.Invoke(ArtifactResource);
		QueueFree();
	}

}
