using System.Threading.Tasks;
using Godot;

[GlobalClass]
public partial class BurnEffect : EffectResource
{
    public BurnEffect() {}
    public override string GetDescription()
	{
		return $"Сжигается";
	}
}
