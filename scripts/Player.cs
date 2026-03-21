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
		Status status;
		int value;
		if (!Statuses.TryGetValue(typeof(StrengthStatus), out status))
		{
			value = 0;
		}
		else
		{
			value = status.Value;
		}
		foreach (Card card in Level.Deck.Cards)
		{
			SetStrengthModifierForCards(card, value);
		}
		foreach (Card card in Level.Hand.GetChildren())
		{
			SetStrengthModifierForCards(card, value);
		}
		foreach (Card card in Level.DiscardPile.Cards)
		{
			SetStrengthModifierForCards(card, value);
		}
	}
	private void SetStrengthModifierForCards(Card card, int value)
	{
		foreach (EffectResource effect in card.Effects)
		{
			if (effect is DamageEffect)
			{
				((DamageEffect)effect).StrengthModifier = value;
			}
			if (effect is MultiDamageEffect)
			{
				((MultiDamageEffect)effect).StrengthModifier = value;
			}
			if (effect is RandomTargetApplyEffect)
			{
				EffectResource insideEffect = ((RandomTargetApplyEffect)effect).Effect;
				if (insideEffect is DamageEffect)
				{
					((DamageEffect)insideEffect).StrengthModifier = value;
				}
			}
		}
		card.UpdateDescription();
	}
}
