using Godot;

[GlobalClass]
public partial class StrengthStatus : StatusResource
{
	public StrengthStatus()
	{
		StatusTexture = GD.Load<Texture2D>("res://resources/sprites/enemy_actions/action_attack.svg"); //Мб нужна отдельная иконка для статуса
		Name = "Сила";
		Description = "Повышает урон от атак на X";
		Color = new("AA0000");
	}
}
