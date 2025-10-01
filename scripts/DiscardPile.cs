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
	public void UpdateCount()
	{
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
