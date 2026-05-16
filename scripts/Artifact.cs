using Godot;
using System;

public partial class Artifact : Control
{
	public ArtifactResource ArtifactResource { get; private set; }
	private TextureRect sprite;
	public void SetArtifact(ArtifactResource artifactResource)
	{
		sprite = GetNode<TextureRect>("TextureRect");
		ArtifactResource = artifactResource;
		TooltipText = artifactResource.Description;
	}
	public void UpdateTexture()
	{
		sprite.Texture = ArtifactResource.ArtifactTexture;
	}
}
