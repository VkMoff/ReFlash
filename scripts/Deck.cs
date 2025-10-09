using Godot;
using System;
using System.Collections.Generic;

public partial class Deck : Control
{
	Random random = new();
	PackedScene card;
	Label label;

	Level level;
	public List<Card> Cards
	{
		get;
		private set;
	}

	public override void _Ready()
	{
		Cards = new();
		card = GD.Load<PackedScene>("res://scenes/card.tscn");

		CardResource damageResource = GD.Load<CardResource>("res://resources/cards/Attack.tres");
		CardResource healResource = GD.Load<CardResource>("res://resources/cards/Heal.tres");
		CardResource heavyAttackResource = GD.Load<CardResource>("res://resources/cards/HeavyAttack.tres");
		CardResource berserkAttackResource = GD.Load<CardResource>("res://resources/cards/BerserkAttack.tres");
		CardResource violetAttack = GD.Load<CardResource>("res://resources/cards/Violet.tres");

		CardFactory cardFactory = new();

		Cards.Add(cardFactory.CreateCard(damageResource));
		Cards.Add(cardFactory.CreateCard(damageResource));
		Cards.Add(cardFactory.CreateCard(damageResource));
		Cards.Add(cardFactory.CreateCard(healResource));
		Cards.Add(cardFactory.CreateCard(healResource));
		Cards.Add(cardFactory.CreateCard(healResource));
		Cards.Add(cardFactory.CreateCard(heavyAttackResource));
		Cards.Add(cardFactory.CreateCard(heavyAttackResource));
		Cards.Add(cardFactory.CreateCard(heavyAttackResource));
		Cards.Add(cardFactory.CreateCard(berserkAttackResource));
		Cards.Add(cardFactory.CreateCard(berserkAttackResource));
		Cards.Add(cardFactory.CreateCard(violetAttack));

		Shuffle();

		label = GetNode<Label>("Label");
		level = GetParent<Level>();
	}
	
	public Card Pull()
	{
		if (Cards.Count <= 0)
		{
			if (!level.RefillDeck()) return null;
		}
		Card ret = Cards[Cards.Count - 1];
		Cards.Remove(ret);
		label.Text = Cards.Count.ToString();
		if (Cards.Count == 0)
		{
			Visible = false;
		}
		return ret;
	}

	public void Push(Card card)
	{
		Cards.Add(card);
		Visible = true;
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
