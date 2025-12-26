using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public partial class Character : Control
{
	HealthBar healthBar;
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

		healthBar = GetNode<HealthBar>("HealthBar");
		sprite = GetNode<AnimatedSprite2D>("Control/AnimatedSprite");

		healthBar.SetMaxHp(MaxHp);
		healthBar.SetHealth(Hp);
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
		Hp = Math.Min(Hp + change, MaxHp);
		if (Hp <= 0)
		{
			Die();
		}
		healthBar.SetHealth(Hp);
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
			var statusScene = GD.Load<PackedScene>("res://scenes/status.tscn");
			Status status = statusScene.Instantiate<Status>();
			status.SetResource(statusResource);
			status.Value = value;

			statusContainer.AddChild(status);

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
