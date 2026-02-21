using System.Threading.Tasks;
using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class DamageEffect : EffectResource
{
	[Export] public int Damage { get; private set; }
	public override async Task Execute(Character caster, Character[] targets)
	{
		foreach (Character target in targets)
		{
			target.ChangeHP(-Damage);
			foreach (var (statusType, status) in target.Statuses)
			{
				status.OnDamageReceive(target, caster);
			}
		}

		List<Task> animationTasks = new List<Task>();
		foreach (Character target in targets)
		{
			animationTasks.Add(PlayAnimationWithSpriteFrames(target, scale: 0.25f));
		}

		await Task.WhenAll(animationTasks);
	}


	public DamageEffect() {}
	public DamageEffect(int damage)
	{
		this.Damage = damage;
	}
	public override string GetDescription()
	{
		return $"Наносит [color=red]{Damage}[/color] урона";
	}
}
