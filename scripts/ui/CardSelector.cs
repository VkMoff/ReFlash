using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CardSelector : CanvasLayer
{
	List<Card> cards;
	HBoxContainer cardContainer;
	public event Action CardSelected;
	public override void _Ready()
	{
		cardContainer = GetNode<HBoxContainer>("%CardContainer");

		cards = CardRegistry.Instance.GetRandom(3);
		foreach (Card card in cards)
		{
			cardContainer.AddChild(card);
			card.Clicked += SelectCard;
		}
	}
	private void SelectCard(Card card)
	{
		PlayerData.Instance.Deck.Add(card.Clone());
		CardSelected?.Invoke();
		QueueFree();
	}
	
	public void Pass()
	{
		Hide();
	}
}
