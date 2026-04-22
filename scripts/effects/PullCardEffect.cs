using System.Threading.Tasks;
using Godot;

[GlobalClass]
public partial class PullCardEffect : EffectResource
{
    [Export] private int cardCount;
    public int Value
    {
        get
        {
            return cardCount;
        }
    }
    public override async Task Execute(Character caster, Character[] targets)
    {
        caster.Level.PullCardFromDeck(cardCount);
        await PlayAnimationWithSpriteFrames(caster, 3);
    }

    public PullCardEffect() {}
    public PullCardEffect(int cardCount)
    {
        this.cardCount = cardCount;
    }
    
    public override string GetDescription()
	{
        string count;
        if (Value % 10 == 1) count = "карту";
        else if (Value % 10 < 5) count = "карты";
        else count = "карт";
		return $"Возьмите [color=0BC2D6]{Value}[/color] {count}";
	}
}
