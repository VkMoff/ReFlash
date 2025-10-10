using Godot;
using System;

public partial class Character : Control
{
	[Export] SpriteFrames spriteFrames;
	HealthBar healthBar;
	AnimatedSprite2D sprite;
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
		private set;
	}
	public override void _Ready()
	{
		level = GetParent<Level>();
		healthBar = GetNode<HealthBar>("HealthBar");
		MaxHp = 100;
		healthBar.SetMaxHp(MaxHp);
		Hp = MaxHp;
		healthBar.SetHealth(Hp);
		sprite = GetNode<AnimatedSprite2D>("Control/AnimatedSprite");
		sprite.SpriteFrames = spriteFrames;
		sprite.Play();
		IsAlive = true;

	}

	public void ChangeHP(int change)
	{
		Hp = Math.Min(Hp + change, MaxHp);
		if (Hp <= 0)
		{
			Die();
		}
		healthBar.SetHealth(Hp);
		}

	public void Die()
	{
		IsAlive = false;
		GD.Print("Im DED");
		level.CharacterDied(this);
	}
}
