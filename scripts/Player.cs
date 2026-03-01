using Godot;
using System;

public partial class Player : Character
{
	[Export] SpriteFrames spriteFrames;

	public override void _Ready()
	{
		Level = GetParent<Level>();
		PlayerData playerData = PlayerData.Instance;
		Init(playerData.MaxHP, spriteFrames);
		GetNode<Label>("NameLabel").Text = "Игрок";
		playerData.HealthChanged += (int hp) =>
		{
			Hp = hp;
		};
		base._Ready();
	}
	public override void ChangeHP(int change)
	{
		base.ChangeHP(change);
		PlayerData.Instance.HP = Hp;
	}
	public void SetHp(int hp)
	{
		if (hp <= MaxHp && hp >= 0)
		{
			Hp = hp;
			healthBar.Value = hp;
		}
		else
		{
			GD.Print("Incorrect HP value");
		}
	}
	public override void Die()
	{
		base.Die();
		SceneManager.Instance.GoToMenu();
	}
	public override void RecalculateStrength()
	{
		foreach (Card card in Level.Deck.Cards)
		{
			foreach (EffectResource effect in card.Effects)
			{
				if (effect is DamageEffect)
				{
					((DamageEffect)effect).StrengthModifier = Statuses[typeof(StrengthStatus)].Value;
				}
				if (effect is MultiDamageEffect)
				{
					((MultiDamageEffect)effect).StrengthModifier = Statuses[typeof(StrengthStatus)].Value;
				}
			}
			card.UpdateDescription();
		}
		foreach (Card card in Level.Hand.GetChildren())
		{
			foreach (EffectResource effect in card.Effects)
			{
				if (effect is DamageEffect)
				{
					((DamageEffect)effect).StrengthModifier = Statuses[typeof(StrengthStatus)].Value;
				}
				if (effect is MultiDamageEffect)
				{
					((MultiDamageEffect)effect).StrengthModifier = Statuses[typeof(StrengthStatus)].Value;
				}
			}
			card.UpdateDescription();
		}
		foreach (Card card in Level.DiscardPile.Cards)
		{
			foreach (EffectResource effect in card.Effects)
			{
				if (effect is DamageEffect)
				{
					((DamageEffect)effect).StrengthModifier = Statuses[typeof(StrengthStatus)].Value;
				}
				if (effect is MultiDamageEffect)
				{
					((MultiDamageEffect)effect).StrengthModifier = Statuses[typeof(StrengthStatus)].Value;
				}
			}
			card.UpdateDescription();
		}
	}

}
