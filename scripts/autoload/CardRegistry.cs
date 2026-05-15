using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class CardRegistry : Node
{
	public static CardRegistry Instance;
	public Godot.Collections.Dictionary<string, CardResource> Cards { get; private set; }
	private PackedScene cardScene = GD.Load<PackedScene>("res://scenes/card.tscn");

	[Export] private CardManifest manifest = GD.Load<CardManifest>("res://resources/card_manifest.tres");

	public override void _Ready()
	{
		Instance = this;
		ProcessMode = ProcessModeEnum.Always;
		Cards = new();
		LoadAllCardsFromManifest();
	}

	private void LoadAllCardsFromManifest()
	{
		if (manifest == null || manifest.CardPaths.Length == 0)
		{
			GD.PrintErr("Card manifest is missing or empty!");
			return;
		}

		foreach (string path in manifest.CardPaths)
		{
			var resource = GD.Load<CardResource>(path);
			if (resource != null && !string.IsNullOrEmpty(resource.Id))
			{
				if (Cards.ContainsKey(resource.Id))
				{
					GD.PrintErr($"Duplicate card id: {resource.Id}");
					continue;
				}
				Cards[resource.Id] = resource;
				GD.Print($"Loaded card: {resource.Name} (ID: {resource.Id})");
			}
		}
		GD.Print($"Loaded {Cards.Count} cards from manifest.");
	}

	public Card this[string id]
	{
		get
		{
			if (!Cards.TryGetValue(id, out var data))
			{
				GD.PrintErr($"Card with id '{id}' not found!");
				return null;
			}
			
			Card card = cardScene.Instantiate<Card>();
			card.Init(data);
			return card;
		}
	}

	public List<Card> GetRandom(int count = 1)
	{
		var rng = new RandomNumberGenerator();
		Array<CardResource> resources = new();
		List<Card> returned = new();
		for (int i = 0; i < count; i++)
		{
			CardResource cardResource;
			do
			{
				cardResource = Cards.Values.ToArray()[rng.Randi() % Cards.Count];
			}
			while (resources.Contains(cardResource));
			resources.Add(cardResource);
		}
		for (int i = 0; i < count; i++)
		{
			Card card = cardScene.Instantiate<Card>();
			returned.Add(card.Init(resources[i]));
		}
		
		return returned;
	}

	public List<CardResource> GetRandomUniqueResources(int count)
    {
        if (count > Cards.Count) count = Cards.Count;
        var shuffled = Cards.Values.OrderBy(x => GD.Randi()).Take(count).ToList();
        return shuffled;
    }

    public Card CreateCard(CardResource resource)
    {
        Card card = cardScene.Instantiate<Card>();
        card.Init(resource);
        return card;
    }

}
