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
	public Character Player
	{
		get;
		private set;
	}
	public List<Character> Enemies
	{
		get;
		private set;
	}
	DiscardPile discardPile;
	
	public override void _Ready()
	{
		Deck = GetNode<Deck>("Deck");
		Hand = GetNode<Hand>("Hand");
		Player = GetNode<Character>("Character");
		discardPile = GetNode<DiscardPile>("DiscardPile");
		Enemies = new();
		Enemies.Add(GetNode<Character>("Enemy"));
		Enemies.Add(GetNode<Character>("Enemy2"));
		PullCardFromDeck(5);
	}

	public void Discard(Card card)
	{
		discardPile.Add(card);
	}

	public void PullCardFromDeck(int count = 1)
	{
		for (int i = 0; i < count; i++)
		{
			Hand.Add(Deck.Pull());
		}
	}

	public bool RefillDeck()
	{
		if (discardPile.Cards.Count <= 0) return false;
		foreach (Card card in discardPile.Cards)
		{
			Deck.Push(card);
		}
		discardPile.Clear();
		discardPile.UpdateCount();
		Deck.Shuffle();
		return true;
	}
}
