using Godot;
using System;

public partial class PullCardButton : Button
{
	private Level level;
	public override void _Ready()
	{
		level = GetParent<Level>();
	}
	
	public override void _GuiInput(InputEvent @event)
	{
		if (@event.IsActionReleased("left_click")) //ВОТ ЭТОГО ТУТ БЫТЬ НЕ ДОЛЖНО
		{
			level.PullCardFromDeck();
		}
	}
}
