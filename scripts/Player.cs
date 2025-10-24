using Godot;
using System;

public partial class Player : Character
{
	[Export] SpriteFrames spriteFrames;

	public override void _Ready()
	{
		level = GetParent<Level>();
		PlayerData playerData = PlayerData.Instance;
		Init(playerData.MaxHP, spriteFrames);
		playerData.HealthChanged += (int hp) =>
		{
			Hp = hp;
		};
		base._Ready();
	}
	public override void Die()
	{
		base.Die();
		SceneManager.Instance.GoToMenu();
	}
}
