using Godot;

[GlobalClass]
public partial class MultiDamageEffect : EffectResource
{
	[Export] public int Damage { get; private set; }
    [Export] public int Count { get; private set; }
	public override void Execute(Character caster, Character[] targets)
	{
		foreach (Character target in targets)
		{
            var tween = target.CreateTween(); //заменить чёрную магию на асинхронность
            for (int i = 0; i < Count; i++)
            {
                int index = i;
                tween.TweenCallback(Callable.From(() => {
                    target.ChangeHP(-Damage);
                    foreach (var status in target.Statuses.Values)
                        status.OnDamageReceive(target, caster);
                }));
                if (i < Count - 1)
                    tween.TweenInterval(0.5f);
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
		return $"Наносит [color=red]{Damage}[/color] урона {Count} раз";
	}
}
