using Godot;
using System;

public partial class DiscardPile : Control
{
	public override void _Ready() {
		SetCount(0);
	}
	public void SetCount(int count)
	{
		GetNode<Label>("Label").Text = count.ToString();
		if (count > 0)
		{
			Visible = true;
		}
		else
		{
			Visible = false;
		}
	}
}
