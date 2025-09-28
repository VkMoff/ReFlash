using Godot;
using System;
using System.Collections.Generic;

public partial class Level : Node2D
{
	public Deck Deck
	{
		get;
		private set;
	}
	public Hand Hand
	{
		get;
		private set;
	}
	List<Card> discardPile;
	DiscardPile discardPileUI;
	public override void _Ready()
	{
		Deck = GetNode<Deck>("Deck");
		Hand = GetNode<Hand>("Hand");
		discardPile = new();
		discardPileUI = GetNode<DiscardPile>("DiscardPile");
		Hand.Add(Deck.Pull());
		Hand.Add(Deck.Pull());
		Hand.Add(Deck.Pull());

	}

	public void Discard(Card card)
	{
		discardPile.Add(card);
		discardPileUI.SetCount(discardPile.Count); //ОБЪЕДИНИТЬ
		GD.Print($"В стопке сброса {discardPile.Count} карт");
	}
}
