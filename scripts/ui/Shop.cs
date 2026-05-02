using Godot;
using System;

public partial class Shop : Control
{
	public void GoToMap()
	{
		SceneManager.Instance.GoToMap();
	}

	public void BuyArtifact(ArtifactResource artifact)
	{
		PlayerData.Instance.InstantiateArtifact(artifact);
	}
}
