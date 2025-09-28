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
		AddChild(card);
	}

	public void UpdatePositions()
	{

	}
}
