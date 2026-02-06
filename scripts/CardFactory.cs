using System;
using Godot;
public class CardFactory
{
    static PackedScene cardScene = GD.Load<PackedScene>("res://scenes/card.tscn");
    [Obsolete("Creating cards is now implemented by cloning cards from CardRegistry")]
    public static Card CreateCard(CardResource cardResource)
    {
        throw new NotImplementedException("Probably this should deal with mess in card registry but idk how");
    }
}