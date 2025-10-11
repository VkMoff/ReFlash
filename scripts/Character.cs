using Godot;
using System;

public partial class Character : Control
{
	HealthBar healthBar;
	AnimatedSprite2D sprite;
	SpriteFrames spriteFrames;
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
	public override void _Ready() //Общая логика анимации
	{
		level = (Level)GetTree().CurrentScene;
		healthBar = GetNode<HealthBar>("HealthBar");
		sprite = GetNode<AnimatedSprite2D>("Control/AnimatedSprite");
		
		healthBar.SetMaxHp(MaxHp);
		healthBar.SetHealth(Hp);
		sprite.SpriteFrames = spriteFrames;
		sprite.Play();

		IsAlive = true;
	}

	// public Character(int maxHp) //Заглушка. Передавать EnemyResource
	// {
	// 	MaxHp = maxHp;
	// 	Hp = MaxHp;
	// 	healthBar.SetMaxHp(MaxHp);
	// 	healthBar.SetHealth(Hp);
	// }

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

	public void Die()
	{
		IsAlive = false;
		GD.Print("Im DED");
		level.CharacterDied(this);
	}
}
