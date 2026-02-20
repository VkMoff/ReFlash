using Godot;

[GlobalClass]
public partial class SelfDamageEffect : EffectResource
{
    [Export] private int damageValue;
    public int Damage
    {
        get
        {
            return damageValue;
        }
    }
    public override void Execute(Character caster, Character[] targets)
    {
        caster.ChangeHP(-damageValue);
    }
    public SelfDamageEffect() {}
    public SelfDamageEffect(int damageValue)
    {
        this.damageValue = damageValue;
    }

    public override string GetDescription()
	{
		return $"Отнимает [color=red]{Damage}[/color] здоровья";
	}
}
