using System.Threading.Tasks;
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
    public override async Task Execute(Character caster, Character[] targets)
    {
        caster.ChangeHP(healValue);
    }

    public HealEffect() {}
    public HealEffect(int healValue)
    {
        this.healValue = healValue;
    }
    
    public override string GetDescription()
	{
		return $"Восстанавливает [color=green]{Value}[/color] здоровья";
	}
}
