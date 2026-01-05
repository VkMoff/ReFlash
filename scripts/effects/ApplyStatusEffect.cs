using System;
using Godot;

[GlobalClass]
public partial class ApplyStatusEffect : EffectResource
{
	[Export] public StatusResource StatusToApply
	{
		get;
		private set;
	}
    [Export] public int Value
    {
        get;
        private set;
    }

	public override void Execute(Character caster, Character[] targets)
	{
		foreach (Character target in targets)
		{
			target.AddStatus(StatusToApply, Value);
		}
	}

	public ApplyStatusEffect()
	{
		this.Description = "Накладывает {StatusValue} {StatusType}";
	}
    // Нужно что-то сделать с конструкторами
}
