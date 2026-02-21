using Godot;
using System;
using System.Threading.Tasks;

public abstract partial class EffectResource : Resource
{
    public virtual async Task Execute(Character caster, Character[] targets) {}
    public abstract string GetDescription(); 
}