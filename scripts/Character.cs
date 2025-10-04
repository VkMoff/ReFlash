using Godot;
using System;

public partial class Character : Node2D
{
	HealthBar healthBar;
	AnimatedSprite2D sprite;
	[Export] SpriteFrames spriteFrames;
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
		healthBar = GetNode<HealthBar>("HealthBar");
		MaxHp = 100;
		healthBar.SetMaxHp(MaxHp);
		Hp = MaxHp;
		healthBar.SetHealth(Hp);
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
		sprite.SpriteFrames = spriteFrames;
		sprite.Play();
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
		GD.Print("Im DED");
	}
	public void Heal(int healValue)
	{
		Hp = Math.Min(Hp + healValue, MaxHp);
		healthBar.SetHealth(Hp);
	}
}
