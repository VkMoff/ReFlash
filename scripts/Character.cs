using Godot;
using System;
using System.Collections.Generic;

public partial class Character : Control
{
	TextureProgressBar healthBar;
	Label healthLabel;
	AnimatedSprite2D sprite;
	SpriteFrames spriteFrames;
	HBoxContainer statusContainer;
	protected Level level;
	public bool IsAlive
	{
		get;
		private set;
	}
	public int MaxHp
	{
		get;
		private set;
	}
	public int Hp
	{
		get;
		protected set;
	}
	public Dictionary<Type, Status> Statuses
	{
		get;
		private set;
	}
	public override void _Ready() //Общая логика анимации
	{
		Statuses = [];
		statusContainer = GetNode<HBoxContainer>("StatusContainer");
		healthBar = GetNode<TextureProgressBar>("HealthBar");
		healthLabel = GetNode<Label>("HealthBar/Label");

		sprite = GetNode<AnimatedSprite2D>("Control/AnimatedSprite");

		healthBar.MaxValue = MaxHp;
		ShowHPValue();
		sprite.SpriteFrames = spriteFrames;
		sprite.Play();

		IsAlive = true;
	}

	public void Init(int maxHp, SpriteFrames spriteFrames) //А вот это надо убрать куда подальше
	{
		MaxHp = maxHp;
		Hp = MaxHp;

		this.spriteFrames = spriteFrames;
	}

	public void ChangeHP(int change) //Общая логика работы с хп
	{
		GD.Print($"{this} Changing HP {Hp} + {change}");
		Hp = Math.Min(Hp + change, MaxHp);
		ShowHPValue();
		if (Hp <= 0)
		{
			Die();
		}
	}

	public void ShowHPValue()
	{
		healthLabel.Text = $"{Hp} / {MaxHp}";
		healthBar.Value = Hp;
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
	}

	public virtual void Die()
	{
		IsAlive = false;
		GD.Print("Im DED");
		level.CharacterDied(this);
	}
}
