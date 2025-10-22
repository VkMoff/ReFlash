using Godot;
public class CardFactory
{
    static PackedScene cardScene = GD.Load<PackedScene>("res://scenes/card.tscn");

    public static Card CreateCard(CardResource cardResource)
    {
        Card card = cardScene.Instantiate<Card>();
        card.Init(cardResource);
        return card;
    }
}