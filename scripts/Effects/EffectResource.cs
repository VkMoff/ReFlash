using Godot;
using System;

[GlobalClass][Obsolete("Для создания карт теперь используется реестр карт")]
public abstract partial class EffectResource : Resource
{
    public string Description
    {
        get;
        protected set;
    }
    public abstract void Execute(Character caster, Character[] targets);
}