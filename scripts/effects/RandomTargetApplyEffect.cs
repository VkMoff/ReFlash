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
			bool hasAliveTarget = false;
			foreach (Character character in targets)
			{
				if (character.IsAlive) hasAliveTarget = true;
			}
			if (!hasAliveTarget) return;
			Character target;
			do
			{
				target = targets[GD.Randi() % targets.Length];
				GD.Print(target.IsAlive);
			}
			while (!target.IsAlive);
            await Effect.Execute(caster, [target]); //проклято
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
