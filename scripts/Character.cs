using Godot;
using System;

public partial class Character : Control
{
	HealthBar healthBar;
	AnimatedSprite2D sprite;
	[Export] SpriteFrames spriteFrames;
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

	public void DealDamage(int dmg)
	{
		Hp -= dmg;
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
	}
	public void Heal(int healValue)
	{
		Hp = Math.Min(Hp + healValue, MaxHp);
		healthBar.SetHealth(Hp);
	}
	
}
