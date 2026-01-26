using Godot;
using System;

public partial class TopPanelUI : Control
{
	private Label healthLabel, goldLabel;
	public override void _Ready()
	{
		healthLabel = GetNode<Label>("HBoxContainer/HealthLabel");
		goldLabel = GetNode<Label>("HBoxContainer/MoneyLabel");

		ChangeHP(PlayerData.Instance.HP);
		ChangeGold(PlayerData.Instance.Gold);

		PlayerData.Instance.GoldChanged += ChangeGold;
		PlayerData.Instance.HealthChanged += ChangeHP;
	}
	public override void _ExitTree() //Фиксит ошибку с удалением сцены карты, но если оптимизировать меню карты, можно упростить
	{
		base._ExitTree();
		PlayerData.Instance.GoldChanged -= ChangeGold;
		PlayerData.Instance.HealthChanged -= ChangeHP;
	}

	public void ChangeHP(int health) 
	{
		healthLabel.Text = $"{health}/{PlayerData.Instance.MaxHP} ";
	}
	public void ChangeGold(int gold)
	{
		goldLabel.Text = gold.ToString();
	}
}
