using Godot;

[GlobalClass]
public partial class DamageEffect : EffectResource
{
    [Export] private int damage;

    public override void Execute(Character caster, Character[] targets)
    {
        foreach (Character target in targets)
        {
            target.DealDamage(damage);
        }
    }

    public DamageEffect()
    {
        
    }
    public DamageEffect(int damage)
    {
        this.damage = damage;
    }
}