using Godot;

[GlobalClass]
[System.Obsolete]
public partial class SelfDamageEffect : EffectResource
{
    [Export] private int damageValue;
    string baseDescription = "Отнимает {DamageValue} здоровья";
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

    public SelfDamageEffect()
    {
        this.Description = baseDescription;
    }
    public SelfDamageEffect(int damageValue)
    {
        this.damageValue = damageValue;
        this.Description = baseDescription;
    }

}
