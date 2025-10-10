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
		protected set;
	}
	public override void _Ready() //Общая логика анимации
	{
		level = GetParent<Level>();
		healthBar = GetNode<HealthBar>("HealthBar");
		sprite = GetNode<AnimatedSprite2D>("Control/AnimatedSprite");
		sprite.SpriteFrames = spriteFrames;
		sprite.Play();
		IsAlive = true;
	}

	public Character() //Вот это будет использоваться при процедурной генерации уровней
		{
				
		}

	public void Init(int maxHp) //А вот это надо убрать куда подальше
	{
		MaxHp = maxHp;
		Hp = MaxHp;
		healthBar.SetMaxHp(MaxHp);
		healthBar.SetHealth(Hp);
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
