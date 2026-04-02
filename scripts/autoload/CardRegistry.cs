using Godot;
using Godot.Collections;

public partial class CardRegistry : Node
{
	public static CardRegistry Instance;
	public Dictionary<string, CardResource> Cards { get; private set; }

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

			PackedScene scene = GD.Load<PackedScene>("res://scenes/card.tscn");
			Card card = scene.Instantiate<Card>();
			card.Init(data);
			return card;
		}
	}
}
