using Godot;
using System;

[GlobalClass]
public abstract partial class EffectResource : Resource
{
    public abstract void Execute(Character caster, Character[] targets);
}