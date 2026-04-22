using System.Threading.Tasks;
using Godot;

[GlobalClass]
public partial class AddEnergyEffect : EffectResource
{
    [Export] private int energyValue;
    public int Value
    {
        get
        {
            return energyValue;
        }
    }
    public override async Task Execute(Character caster, Character[] targets)
    {
        caster.Level.AddEnergy(energyValue);
        await PlayAnimationWithSpriteFrames(caster, 3);
    }

    public AddEnergyEffect() {}
    public AddEnergyEffect(int energyValue)
    {
        this.energyValue = energyValue;
    }
    
    public override string GetDescription()
	{
		return $"Даёт [color=0BC2D6]{Value}[/color] энергии";
	}
}
