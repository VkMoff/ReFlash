using Godot;
using System;
using System.Collections.Generic;

public partial class Level : Control
{
	public Deck Deck { get; private set; }
	public Hand Hand { get; private set; }
	public Player Player { get; private set; }
	public List<Character> Enemies { get; private set; }
	public Character TargetEnemy { get; set; }
	public int Energy { get; private set; }
	public int DefaultHandSize { get; private set; }
	public DiscardPile DiscardPile { get; private set; }
	private Label energyLabel;
	private Button pullCardButton, endTurnButton, reloadSceneButton;
	private RoomResource roomResource;
	private HBoxContainer enemyContainer;
	private HBoxContainer artifactContainer;
	private PackedScene artifactScene;
	private TopPanelUI topPanelUi;
	public event Action TurnStart, TurnEnd, BattleStart;


	public override void _Ready()
	{
		enemyContainer = GetNode<HBoxContainer>("EnemyContainer");
		artifactContainer = GetNode<HBoxContainer>("ArtifactContainer");
		Deck = GetNode<Deck>("Deck");
		Hand = GetNode<Hand>("Hand");
		Player = GetNode<Player>("Player");
		DiscardPile = GetNode<DiscardPile>("DiscardPile");

		artifactScene = GD.Load<PackedScene>("res://scenes/artifact.tscn");
		

		//Кнопки
		pullCardButton = GetNode<Button>("PullCardButton");
		endTurnButton = GetNode<Button>("EndTurnButton");
		reloadSceneButton = GetNode<Button>("ReloadSceneButton");
		//Добавление сигналов
		pullCardButton.Pressed += PullCardFromDeck;
		endTurnButton.Pressed += EndTurn;
		reloadSceneButton.Pressed += RestartScene;

		Enemies = new();

		Energy = 3;
		energyLabel = GetNode<Label>("EnergyContainer/Energy/EnergyLabel");
		UpdateEnergyLabel();
		DefaultHandSize = 5;
		PullCardFromDeck(DefaultHandSize);

		topPanelUi = GetNode<TopPanelUI>("TopPanelUI"); 
		Player.SetHp(PlayerData.Instance.HP);//такое себе
	}
	public void InitRoom(RoomResource roomResource)
	{
		this.roomResource = roomResource;
		foreach (EnemyResource enemyResource in roomResource.Enemies)
		{
			Enemies.Add(EnemyFactory.CreateEnemy(enemyResource));
		}
		foreach (Enemy enemy in Enemies)
		{
			enemyContainer.AddChild(enemy);
		}

		//test
		// Enemies[0].AddStatus(new PoisonStatus(), 5);
		// Player.AddStatus(new SpikesStatus(), 3);
		// Player.AddStatus(new RegenStatus(), 10);
		
		foreach (Artifact artifact in PlayerData.Instance.Artifacts)
		{
			artifactContainer.AddChild(artifact);
			artifact.ArtifactResource.Load(this);
			artifact.UpdateTexture();
		}
		// Artifact testArtifact = artifactScene.Instantiate<Artifact>();
		// artifactContainer.AddChild(testArtifact);
		// testArtifact.SetArtifact(new HourGlass());
		// testArtifact.ArtifactResource.Init(this);
		// testArtifact.UpdateTexture();

		BattleStart?.Invoke();
		TurnStart?.Invoke();
	}
	public void Discard(Card card)
	{
		Hand.RemoveChild(card);
		DiscardPile.Add(card);
	}
	public void PullCardFromDeck()
	{
		PullCardFromDeck(1);
	}
	public void PullCardFromDeck(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Hand.Add(Deck.Pull());
		}
	}
	public bool RefillDeck()
	{
		if (DiscardPile.Cards.Count <= 0) return false;
		foreach (Card card in DiscardPile.Cards)
		{
			Deck.Push(card);
		}
		DiscardPile.Clear();
		DiscardPile.UpdateCount();
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

	//?
	private List<Character> pendingRemovals = new();


	public void EndTurn()
	{
		foreach (Card card in Hand.GetChildren())
		{
			card.Discard();
		}

		TurnEnd?.Invoke();

		foreach (var (statusType, status) in Player.Statuses)
		{
			status.OnTurnEnd([Player]);
		}

		foreach (Enemy enemy in Enemies)
		{
			if (!enemy.IsAlive) continue;

			foreach (var (statusType, status) in enemy.Statuses)
			{
				status.OnTurnEnd([enemy]);
			}

		}

		foreach (var character in pendingRemovals)
		{
			Enemies.Remove(character);
		}
		pendingRemovals.Clear();

		if (Enemies.Count == 0)
		{
			Win();
		}

		var tween = CreateTween(); //переделывать под асинхронность...
		foreach (Enemy enemy in Enemies)
		{
			if (!enemy.IsAlive) continue;

			tween.TweenCallback(Callable.From(() => enemy.ExecuteNextAction()));

			tween.TweenInterval(1f);
		}





		Energy = 3;
		UpdateEnergyLabel();
		PullCardFromDeck(DefaultHandSize);

		TurnStart?.Invoke();
	}
	public void CharacterDied(Character character)
	{
		if (character is Enemy)
		{
			pendingRemovals.Add(character);
			character.Visible = false;
		}
		if (pendingRemovals.Count == Enemies.Count)
		{
			Win();
		}
		// character.QueueFree();
	}
	public void Win()
	{
		GD.Print("LEVEL COMPLETED!");
		PlayerData.Instance.AddGold(50);
		SceneManager.Instance.GoToMap();
	}
	public void GoToMap()
	{
		SceneManager.Instance.GoToMap();
	}
	public void RestartScene()
	{
		QueueFree();
		SceneManager.Instance.LoadLevel(roomResource);
	}
	public override void _ExitTree() {
		foreach (Artifact artifact in artifactContainer.GetChildren())
		{
			artifactContainer.RemoveChild(artifact);
		}
		base._ExitTree();
	}
}
