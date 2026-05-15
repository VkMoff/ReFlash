using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class MemoEncounter : Control
{
	private int flippedCount = 0;
	private int tryCount = 0;
	private const int MAX_TRY_COUNT = 5;
	private List<Card> flippedCards = new();
	private List<Card> gottenCards = new();
	private GridContainer cardContainer;
	Label tryCountLabel;
	private bool isLocked = false;

	public override void _Ready()
	{
		cardContainer = GetNode<GridContainer>("GridContainer");
		tryCountLabel = GetNode<Label>("%TryCountLabel");

		tryCountLabel.Text = $"Попыток осталось: {MAX_TRY_COUNT}";

		var uniqueResources = CardRegistry.Instance.GetRandomUniqueResources(6);
		List<Card> allCards = new List<Card>();
		
		foreach (var resource in uniqueResources)
		{
			Card card1 = CardRegistry.Instance.CreateCard(resource);
			Card card2 = CardRegistry.Instance.CreateCard(resource);
			allCards.Add(card1);
			allCards.Add(card2);
		}
		
		Random rng = new Random();
		allCards = allCards.OrderBy(x => rng.Next()).ToList();
		
		foreach (var card in allCards)
		{
			cardContainer.AddChild(card);
			card.FlipToBack();
			card.Clicked += OnCardClicked;
		}
	}
	
	public async void OnCardClicked(Card card)
	{
		if (isLocked) return;
		if (!card.IsFlipped) return;
		if (tryCount >= MAX_TRY_COUNT) return;
		await card.FlipToFront();
		flippedCards.Add(card);
		flippedCount++;
		
		if (flippedCount == 2)
		{
			isLocked = true;
			await ToSignal(GetTree().CreateTimer(0.8), SceneTreeTimer.SignalName.Timeout);
			
			if (flippedCards[0].CardName == flippedCards[1].CardName)
			{
				foreach (var c in flippedCards)
				{
					c.Clicked -= OnCardClicked;
					gottenCards.Add(c);
				}
			}
			else
			{
				await Task.WhenAll(flippedCards[0].FlipToBack(), flippedCards[1].FlipToBack());
			}
			
			flippedCards.Clear();
			flippedCount = 0;
			tryCount ++;
			tryCountLabel.Text = $"Попыток осталось: {MAX_TRY_COUNT - tryCount}";

			isLocked = false;
			if (tryCount >= MAX_TRY_COUNT)
			{
				foreach (Card c in gottenCards)
				{
					PlayerData.Instance.Deck.Add(c.Clone());
				}
				SceneManager.Instance.GoToMap();
			}
		}
	}
}
