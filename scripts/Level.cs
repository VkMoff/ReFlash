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
	public Character TargetEnemy
	{
		get;
		set;
	}
	public int Energy
	{
		get;
		private set;
	}
	public int DefaultHandSize
	{
		get;
		private set;
	}
	Label energyLabel;
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
		Energy = 3;
		energyLabel = GetNode<Label>("EnergyLabel");
		UpdateEnergyLabel();
		DefaultHandSize = 5;
		PullCardFromDeck(DefaultHandSize);
	}

	public void Discard(Card card)
	{
		Hand.RemoveChild(card);
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

	public void TargetEnemyChanged(Character target)
	{
		TargetEnemy = target;
	}
	public void UpdateEnergyLabel()
	{
		energyLabel.Text = Energy.ToString();
	}
	public bool TryPlay(int cost)
	{
		if (Energy >= cost)
		{
			Energy -= cost;
			UpdateEnergyLabel();
			return true;
		}
		return false;
	}
	public void EndTurn()
	{
		foreach (Card card in Hand.GetChildren())
		{
			Discard(card);
		}
		foreach (Enemy enemy in Enemies)
		{
			if (enemy.IsAlive)
			{
				enemy.NextAttack();
			}
		}
		Energy = 3;
		UpdateEnergyLabel();
		PullCardFromDeck(DefaultHandSize);
	}
}
