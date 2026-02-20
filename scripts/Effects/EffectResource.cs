using Godot;
using System;

public abstract partial class EffectResource : Resource
{
    public abstract void Execute(Character caster, Character[] targets);
    public abstract string GetDescription(); 
}