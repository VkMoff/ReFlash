using Godot;
using System;
using System.Threading.Tasks;

public abstract partial class EffectResource : Resource
{
    public virtual async Task Execute(Character caster, Character[] targets) {}
    public abstract string GetDescription(); 
    [Export] public Resource Animation;
    [Export()] public bool AppliableToCaster = false;

    protected async Task PlayAnimationWithSpriteFrames(Character target, float speed = 1, float scale = 1)
    {
        if (Animation is not SpriteFrames)
        {
            return;
        }
        AnimatedSprite2D sprite = new();
        sprite.SpriteFrames = (SpriteFrames)Animation;
        sprite.Position = target.GlobalPosition + target.Size / 2;
        target.GetTree().Root.AddChild(sprite);
        sprite.Scale *= scale;
        sprite.Play(customSpeed: speed);
        await ToSignal(sprite, AnimatedSprite2D.SignalName.AnimationFinished);
        sprite.QueueFree();
    }
}