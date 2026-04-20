using Godot;
using System;
using System.Collections.Generic;

public partial class Character : VBoxContainer
{
	protected HealthBar healthBar;
	Label healthLabel;
	AnimatedSprite2D sprite;
	SpriteFrames spriteFrames;
	HBoxContainer statusContainer;
	public Level Level {get; protected set;}
	public bool IsAlive	{ get; private set; }
	public int MaxHp { get; private set; }
	public int Hp { get; protected set; }
	public Dictionary<Type, Status> Statuses { get; private set; }
	public override void _Ready() //Общая логика анимации
	{
		Statuses = [];
		statusContainer = GetNode<HBoxContainer>("StatusContainer");
		healthBar = GetNode<HealthBar>("HealthBar");
		// healthLabel = GetNode<Label>("HealthBar/Label");

		// sprite = GetNode<AnimatedSprite2D>("Control/AnimatedSprite");

		healthBar.MaxValue = MaxHp;
		healthBar.Value = Hp;

		// sprite.SpriteFrames = spriteFrames;
		// sprite.Play();

		IsAlive = true;
	}

	public void Init(int maxHp, SpriteFrames spriteFrames) //А вот это надо убрать куда подальше
	{
		MaxHp = maxHp;
		Hp = MaxHp;

		this.spriteFrames = spriteFrames;
	}

	public virtual void ChangeHP(int change) //Общая логика работы с хп
	{
		GD.Print($"{this} Changing HP {Hp} + {change}");

		if (change < 0)
		{
			this.ShowMessage($"{change}", GlobalPosition + Size / 2, duration: 2f, color: Colors.Red, outlineColor: Colors.White);
		}
		else if (change > 0)
		{
			this.ShowMessage($"{change}", GlobalPosition + Size / 2, duration: 2f, color: Colors.Green, outlineColor: Colors.DarkGreen);
		}
		Hp = Math.Min(Hp + change, MaxHp);
		healthBar.Value = Hp;
		if (Hp <= 0)
		{
			Die();
		}
	}


	public void AddStatus(StatusResource statusResource, int value)
	{
		Type statusType = statusResource.GetType();

		if (Statuses.ContainsKey(statusType))
		{
			Statuses[statusType].AddValue(value);
		}
		else
		{
			//оптимизировать
			var statusScene = GD.Load<PackedScene>("res://scenes/status.tscn");
			Status status = statusScene.Instantiate<Status>();
			status.Value = value;

			statusContainer.AddChild(status);

			status.SetResource(statusResource);

			Statuses[statusType] = status;
		}

		if (statusType == typeof(StrengthStatus))
		{
			RecalculateStrength();
		}
	}

	public virtual void RecalculateStrength()
	{

	}

	public virtual void Die()
	{
		IsAlive = false;
		this.ShowMessage("Мёртв");
		Level.CharacterDied(this);
	}
}
