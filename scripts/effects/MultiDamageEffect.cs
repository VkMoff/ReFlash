using System;
using System.Threading.Tasks;
using Godot;

[GlobalClass]
public partial class MultiDamageEffect : EffectResource
{
	[Export] public int Damage { get; private set; }
    [Export] public int Count { get; private set; }
	[Export] public float PlaySpeed { get; private set; } = 3;
	public int StrengthModifier { get; set; } = 0;
	public float Weakness { get; set; } = 0;


	public override async Task Execute(Character caster, Character[] targets)
	{
		foreach (Character target in targets) //Реализовать одновременное нанесение урона, как в DamageEffect
		{
            for (int i = 0; i < Count; i++)
            {
				if (!caster.IsAlive) return;
				target.ChangeHP(Math.Min((int)((-Damage - StrengthModifier) * (1 - Weakness)), 0));
				await PlayAnimationWithSpriteFrames(target, PlaySpeed, 0.25f);
				foreach (var (statusType, status) in target.Statuses)
				{
					status.OnDamageReceive(target, caster);
				}
				if (!target.IsAlive) continue;
            }
		}
	}

	public MultiDamageEffect() {}
	public MultiDamageEffect(int damage, int count)
	{
		this.Damage = damage;
        this.Count = count;
	}
	public override string GetDescription()
	{
		return $"Наносит [color=red]{(Damage + StrengthModifier) * (1 - Weakness)}[/color] урона {Count} раз";
	}
}
