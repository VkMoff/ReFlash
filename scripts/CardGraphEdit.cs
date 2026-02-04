using Godot;
using System;

public partial class CardGraphEdit : GraphEdit
{
	public override void _Ready() {
		ConnectionRequest += (_, _, _, _) => 
		{
			GD.Print(Connections);
		};
	}
	public void PrintConnections()
	{
		GD.Print(Connections);
	}
	public void DebugExecute()
	{
		ulong startTime = Time.GetTicksUsec();
		CardInterpreter.StartSequence(GetNode<CardGraphNode>("Start"));
		GD.Print($"Time elapsed {Time.GetTicksUsec() - startTime}us");
	}
}
