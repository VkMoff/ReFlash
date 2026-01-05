using Godot;

[GlobalClass]
public partial class HealEffect : EffectResource
{
    [Export] private int healValue;
    public int Value
    {
        get
        {
            return healValue;
        }
    }
    public override void Execute(Character caster, Character[] targets)
    {
        caster.ChangeHP(healValue);
    }

    public HealEffect()
    {
        Description = "Восстанавливает {HealValue} здоровья";
    }
    public HealEffect(int healValue)
    {
        this.healValue = healValue;
        Description = "Восстанавливает {HealValue} здоровья";
    }

}
