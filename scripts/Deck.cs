using Godot;
using System;
using System.Collections.Generic;

public partial class Deck : Control
{
	Random random = new();
	public List<Card> Cards
	{
		get;
		private set;
	}

	public override void _Ready()
	{
		Cards = new();
		PackedScene card = GD.Load<PackedScene>("res://scenes/card.tscn");
		Cards.Add(card.Instantiate<Card>());
		Cards.Add(card.Instantiate<Card>());
		Cards.Add(card.Instantiate<Card>());
		Cards.Add(card.Instantiate<Card>());
		Cards.Add(card.Instantiate<Card>());
		Cards.Add(card.Instantiate<Card>());

		Shuffle();
	}
	
	public Card Pull()
	{
		Card ret = Cards[Cards.Count - 1];
		Cards.Remove(ret);
		GD.Print(Cards.Count);
		return ret;
	}

	public void Shuffle()
	{
		for (int i = 0; i < Cards.Count; i++)
		{
			int j = random.Next(i);
			Card tmp = Cards[i];
			Cards[i] = Cards[j];
			Cards[j] = tmp;
		}
	}
}
