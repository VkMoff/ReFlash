using Godot;
public class CardFactory
{
    PackedScene cardScene = GD.Load<PackedScene>("res://scenes/card.tscn");

    public Card CreateCard(CardResource cardResource)
    {
        Card card = cardScene.Instantiate<Card>();
        card.Init(cardResource);
        return card;
    }
}