using Godot;
using System;
using System.Collections.Generic;

public partial class CardSelector : CanvasLayer
{
	List<Card> cards;
	HBoxContainer cardContainer;
	public event Action CardSelected;
	public override void _Ready()
	{
		cardContainer = GetNode<HBoxContainer>("%CardContainer");

		cards = new();
		cards.Add(CardRegistry.Instance["amplify"]);
		cards.Add(CardRegistry.Instance["strike"]);
		cards.Add(CardRegistry.Instance["poison"]);

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
