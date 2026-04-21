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
		int strength;
		float weakness;
		if (!Statuses.TryGetValue(typeof(StrengthStatus), out status))
		{
			strength = 0;
		}
		else
		{
			strength = status.Value;
		}
		if (!Statuses.TryGetValue(typeof(WeaknessStatus), out status))
		{
			weakness = 0;
		}
		else
		{
			weakness = 0.25f;
		}
		foreach (Card card in Level.Deck.Cards)
		{
			SetStrengthModifierForCards(card, strength, weakness);
		}
		foreach (Card card in Level.Hand.GetChildren())
		{
			SetStrengthModifierForCards(card, strength, weakness);
		}
		foreach (Card card in Level.DiscardPile.Cards)
		{
			SetStrengthModifierForCards(card, strength, weakness);
		}
	}
	private void SetStrengthModifierForCards(Card card, int strength, float weakness)
	{
		foreach (EffectResource effect in card.Effects)
		{
			if (effect is DamageEffect damageEffect)
			{
				damageEffect.StrengthModifier = strength;
				damageEffect.Weakness = weakness;
			}
			if (effect is MultiDamageEffect multiDamageEffect)
			{
				multiDamageEffect.StrengthModifier = strength;
				multiDamageEffect.Weakness = weakness;
			}
			if (effect is RandomTargetApplyEffect)
			{
				EffectResource insideEffect = ((RandomTargetApplyEffect)effect).Effect;
				if (insideEffect is DamageEffect innerDamageEffect)
				{
					innerDamageEffect.StrengthModifier = strength;
					innerDamageEffect.Weakness = weakness;
				}
			}
		}
		card.UpdateDescription();
	}
}
