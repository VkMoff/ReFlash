using System.Threading.Tasks;
using System.Collections.Generic;
using Godot;
using System;

[GlobalClass]
public partial class DamageEffect : EffectResource
{
	[Export] public int Damage { get; private set; }
	public int StrengthModifier { get; set; } = 0;
	public override async Task Execute(Character caster, Character[] targets)
	{
		foreach (Character target in targets)
		{
			GD.Print("Start");
			target.ChangeHP(Math.Min(-Damage - StrengthModifier, 0));
			if (!target.IsAlive)
			{
				GD.Print("Cont");
				continue;
			}
			foreach (var (statusType, status) in target.Statuses)
			{
				status.OnDamageReceive(target, caster);
			}
			GD.Print("End");
		}

		List<Task> animationTasks = new List<Task>();
		foreach (Character target in targets)
		{
			animationTasks.Add(PlayAnimationWithSpriteFrames(target, scale: 0.25f));
		}

		await Task.WhenAll(animationTasks);
		GD.Print("Completed");
	}


	public DamageEffect() {}
	public DamageEffect(int damage)
	{
		this.Damage = damage;
	}
	public override string GetDescription()
	{
		return $"Наносит [color=red]{Damage + StrengthModifier}[/color] урона";
	}
}
