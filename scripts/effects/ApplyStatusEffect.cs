using System.Threading.Tasks;
using Godot;

[GlobalClass]
public partial class ApplyStatusEffect : EffectResource
{
	[Export] public StatusResource StatusToApply { get; private set; }
	[Export] public int Value { get; private set; }

	public override async Task Execute(Character caster, Character[] targets)
	{
		foreach (Character target in targets)
		{
			target.AddStatus(StatusToApply, Value);
			await PlayAnimationWithSpriteFrames(target, 3, 0.25f);
		}
	}

	public ApplyStatusEffect() {}
	public override string GetDescription()
	{
		GD.Print(StatusToApply.Color.ToHtml(false));
		return $"Накладывает [color={StatusToApply.Color.ToHtml(false)}]{Value} {StatusToApply.Name}[/color]";
	}
}
