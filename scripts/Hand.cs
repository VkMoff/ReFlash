using Godot;
using System;
using System.Collections.Generic;

public partial class Hand : HBoxContainer
{
	public override void _Ready()
	{
	}

	public void Add(Card card)
	{
		if (card is null) return;
		AddChild(card);
	}

	public void ChangeGap(Node node)
	{
		AddThemeConstantOverride("separation", -2 * (int)Math.Sqrt(100 * GetChildCount()));
	}
}
