using System.Threading.Tasks;
using Godot;

[GlobalClass]
public partial class MultiDamageEffect : EffectResource
{
	[Export] public int Damage { get; private set; }
    [Export] public int Count { get; private set; }
	public override async Task Execute(Character caster, Character[] targets)
	{
		foreach (Character target in targets)
		{
            for (int i = 0; i < Count; i++)
            {
                target.ChangeHP(-Damage);
                await ToSignal(PlayerData.Instance.GetTree().CreateTimer(0.5), SceneTreeTimer.SignalName.Timeout);
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
