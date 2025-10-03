using Godot;

[GlobalClass]
public partial class HealEffect : EffectResource
{
    [Export] private int healValue;
    public override void Execute(Character caster, Character[] targets)
    {
        caster.Heal(healValue);
    }

}
