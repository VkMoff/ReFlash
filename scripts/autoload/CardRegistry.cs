using System;
using Godot;
using Godot.Collections;

public partial class CardRegistry : Node
{
	public static CardRegistry Instance;
	private const string CARDS_PATH = "res://resources/cards/";

	public Dictionary<string, CardResource> Cards { get; private set; }
	public override void _Ready()
	{
		if (Instance is null)
		{
			Instance = this;
			ProcessMode = ProcessModeEnum.Always;
		}
		Cards = new();
		LoadAllCards();
	}
	private void LoadAllCards()
	{
		Cards.Clear();
		using var dir = DirAccess.Open(CARDS_PATH);
		if (dir == null)
		{
			GD.PrintErr($"Failed to open directory: {CARDS_PATH}");
			return;
		}

		ScanDirectory(dir, "");

		GD.Print($"Loaded {Cards.Count} cards from {CARDS_PATH}");
	}

	private void ScanDirectory(DirAccess dir, string currentSubPath) //Чёрная магия
	{
		dir.ListDirBegin();

		while (true)
		{
			string fileName = dir.GetNext();
			if (string.IsNullOrEmpty(fileName))
				break;

			if (fileName == "." || fileName == "..")
				continue;

			string fullPath = dir.GetCurrentDir() + "/" + fileName;

			if (dir.CurrentIsDir())
			{
				using var subDir = DirAccess.Open(fullPath);
				if (subDir != null)
					ScanDirectory(subDir, currentSubPath + "/" + fileName);
			}
			else
			{
				if (fileName.EndsWith(".tres") || fileName.EndsWith(".res"))
				{
					LoadCardFromFile(fullPath);
				}
			}
		}

		dir.ListDirEnd();
	}

	private void LoadCardFromFile(string path)
	{
		var resource = GD.Load(path);
		if (resource is CardResource cardData)
		{
			if (string.IsNullOrEmpty(cardData.Id))
			{
				GD.PrintErr($"Card at {path} has no Id! Skipping.");
				return;
			}

			if (Cards.ContainsKey(cardData.Id))
			{
				GD.PrintErr($"Duplicate card id '{cardData.Id}' from {path} and {Cards[cardData.Id].ResourcePath}");
				return;
			}

			Cards.Add(cardData.Id, cardData);
			GD.Print($"Loaded card: {cardData.Name} (ID: {cardData.Id})");
		}
		else
		{
			GD.PrintErr($"Resource at {path} is not a CardData.");
		}
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
