using Godot;
using System;

public partial class VictoryMenu : CanvasLayer
{
	public void GoToMap()
	{
		SceneManager.Instance.GoToMap();
		QueueFree();
	}
	public void GetGold()
	{
   		PlayerData.Instance.AddGold(50);
	}
	public void GetCard()
	{
		
	}
}
