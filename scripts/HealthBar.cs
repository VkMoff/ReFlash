using Godot;
using System;
using System.Threading;

[Obsolete("Я не знал о существовании TextureProgressBar, когда писал это")]
public partial class HealthBar : PanelContainer
{
	private ColorRect bar;
	private int maxHp = 100; //ТЕСТ
	private Label label;
	public override void _Ready()
	{
		bar = GetNode<ColorRect>("Bar");
		label = GetNode<Label>("Label");
	}
	public void SetMaxHp(int maxHp)
	{
		this.maxHp = maxHp;
		
	}
	public void SetHealth(int hp)
	{
		bar.Color = new("#ff0000");
		bar.CustomMinimumSize = new((float)hp / maxHp * Size.X, 20);
		label.Text = $"{hp}/{maxHp}";
	}
}
