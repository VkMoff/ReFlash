using Godot;
using System;

public partial class Player : Character
{
	public override void _Ready()
	{
		base._Ready();

		PlayerData playerData = PlayerData.Instance;
		Init(playerData.MaxHP);
		playerData.HealthChanged += (int hp) =>
		{
			Hp = hp;
		};
	}


}
