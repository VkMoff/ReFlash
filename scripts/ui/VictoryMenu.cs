using Godot;
using System;
using System.Collections.Generic;

public partial class VictoryMenu : CanvasLayer
{
	int goldReward;
	CardSelector cardSelector;
	VBoxContainer buttonContainer;
	
	
	public void Init(int goldReward, ArtifactResource[] artifacts = null)
	{
		this.goldReward = goldReward;
		GetNode<Button>("%AddGoldButton").Text += $" ({goldReward})";

		buttonContainer = GetNode<VBoxContainer>("%ButtonContainer");

		foreach (ArtifactResource artifact in artifacts)
		{
			GD.Print(artifact.Name);
			ArtifactButton artifactButton = new();
			
			buttonContainer.AddChild(artifactButton);
			artifactButton.Icon = artifact.ArtifactTexture;
			artifactButton.Text = artifact.Name;
			artifactButton.ArtifactResource = artifact;
			
			artifactButton.AddArtifact += PlayerData.Instance.InstantiateArtifact;
		}
		
		
	}
	public void GoToMap()
	{
		SceneManager.Instance.GoToMap();
		QueueFree();
	}
	public void GetGold()
	{
   		PlayerData.Instance.AddGold(goldReward);
	}
	public void GetCard()
	{
		if (cardSelector is null)
		{
			cardSelector = GD.Load<PackedScene>("res://scenes/ui/card_selector.tscn").Instantiate<CardSelector>();
			GetTree().Root.AddChild(cardSelector);
			cardSelector.CardSelected += () =>
			{
				GetNode<Button>("%AddCardButton").QueueFree();
			};
		}
		else
		{
			cardSelector.Show();
		}
	}
	public void GetArtifact()
	{
		
	}
}
