using Godot;
using System;

public partial class Artifact : Control
{
	public ArtifactResource ArtifactResource { get; private set; }
	private Sprite2D sprite;
	public void SetArtifact(ArtifactResource artifactResource)
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		ArtifactResource = artifactResource;
	}
	public void UpdateTexture()
	{
		sprite.Texture = ArtifactResource.ArtifactTexture;
	}
}
