using System.Threading.Tasks;
using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class RandomTargetApplyEffect : EffectResource
{
	[Export] public EffectResource Effect { get; private set; }
    [Export] public int Count;
	public override async Task Execute(Character caster, Character[] targets)
	{
        for (int i = 0; i < Count; i++)
        {
            await Effect.Execute(caster, [targets[GD.Randi() % targets.Length]]); //проклято
        }
	}


	public RandomTargetApplyEffect() {}
	public RandomTargetApplyEffect(EffectResource effect, int count)
	{
		this.Count = count;
	}
	public override string GetDescription()
	{
		return $"{Effect.GetDescription()} случайному врагу {Count} {(((Count > 1) && (Count < 5)) ? "раза" : "раз")}";
	}
}
