using Godot;
using System;
using System.Collections.Generic;

public partial class Hand : HBoxContainer
{
	public override void _Ready()
	{
		ChildEnteredTree += ChangeGap;
		ChildExitingTree += ChangeGap;
	}

	public void Add(Card card)
	{
		if (card is null) return;
		// GD.Print("adding card");
		// GD.Print("checking if card is valid:" + IsInstanceValid(card));
		// GD.Print("card added to hand: " + card);
		AddChild(card);
	}

	public void ChangeGap(Node node)
	{
		GD.Print(-2 * (int)Math.Sqrt(100 * GetChildCount()));
		AddThemeConstantOverride("separation", -2 * (int)Math.Pow(5 * GetChildCount(), 1.08));
	}
	
}
