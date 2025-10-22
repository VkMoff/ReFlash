using Godot;
using System;
using System.Collections.Generic;

public partial class DiscardPile : Control
{
	public List<Card> Cards
	{
		get;
		private set;
	}
	private Label label;
	public override void _Ready()
	{
		Cards = new();
		label = GetNode<Label>("Label");
		UpdateCount();
	}
	public override void _ExitTree()
	{
		foreach (Card card in Cards)
		{
			card.QueueFree();
		}
		base._ExitTree();
	}
	public void UpdateCount()
	{
		label = GetNode<Label>("Label");

		// GD.Print("label: " + label);
		// GD.Print("cards: " + Cards.Count);
		label.Text = Cards.Count.ToString();
		if (Cards.Count <= 0)
		{
			Visible = false;
		}
	}

	public void Add(Card card)
	{
		Cards.Add(card);
		UpdateCount();
		Visible = true;
	}
	public void Clear()
	{
		Cards.Clear();
	}
}
