using Godot;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
	Button pullCardButton, endTurnButton, reloadSceneButton;
	public DiscardPile DiscardPile
	{
		get;
		private set;
	}
	RoomResource roomResource;
	HBoxContainer enemyContainer;

	public override void _Ready()
	{
		enemyContainer = GetNode<HBoxContainer>("EnemyContainer");
		Deck = GetNode<Deck>("Deck");
		Hand = GetNode<Hand>("Hand");
		Player = GetNode<Character>("Player");
		DiscardPile = GetNode<DiscardPile>("DiscardPile");

		//Кнопки
		pullCardButton = GetNode<Button>("PullCardButton");
		endTurnButton = GetNode<Button>("EndTurnButton");
		reloadSceneButton = GetNode<Button>("ReloadSceneButton");
		//Добавление сигналов
		pullCardButton.Pressed += PullCardFromDeck;
		endTurnButton.Pressed += EndTurn;
		reloadSceneButton.Pressed += RestartScene;

		Enemies = new();

		Player.AddStatus(new RegenStatus(), 10);

		Energy = 3;
		energyLabel = GetNode<Label>("EnergyLabel");
		UpdateEnergyLabel();
		DefaultHandSize = 5;
		PullCardFromDeck(DefaultHandSize);
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

		foreach (var (statusType, status) in Player.Statuses)
		{
			status.OnTurnEnd();
		}

		foreach (Enemy enemy in Enemies)
		{
			if (!enemy.IsAlive) continue;

			foreach (var (statusType, status) in enemy.Statuses)
			{
				status.OnTurnEnd();
			}

		}

		foreach (Enemy enemy in Enemies)
		{
			if (!enemy.IsAlive) continue;

			enemy.ExecuteNextAction();
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

		Energy = 3;
		UpdateEnergyLabel();
		PullCardFromDeck(DefaultHandSize);
	}
	public void CharacterDied(Character character)
	{
		if (character is Enemy)
		{
			pendingRemovals.Add(character);
		}
		if (pendingRemovals.Count == Enemies.Count)
		{
			Win();
		}
		character.QueueFree();
	}
	public void Win()
	{
		GD.Print("LEVEL COMPLETED!");
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
}
