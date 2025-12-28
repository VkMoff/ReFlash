using Godot;

[GlobalClass]
public partial class DamageEffect : EffectResource
{
	[Export] public int Damage
	{
		get;
		private set;
	}

	public override void Execute(Character caster, Character[] targets)
	{
		foreach (Character target in targets)
		{
			target.ChangeHP(-Damage);
			foreach (var (statusType, status) in target.Statuses)
            {
                status.OnDamageReceive(target, caster);
            }
		}
	}

	public DamageEffect()
	{
		
	}
	public DamageEffect(int damage)
	{
		this.Damage = damage;
	}
}
