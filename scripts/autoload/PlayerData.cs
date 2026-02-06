using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerData : Node
{
    public static PlayerData Instance { get; private set; }
    public List<Card> Deck { get; private set; }
    public int MaxHP { get; set; } = 100;
    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = Math.Clamp(value, 0, MaxHP);
            if (hp == 0)
            {
                Die();
            }
            HealthChanged?.Invoke(hp);
        }
    }
    public int Gold { get; set; } = 0;
    public List<Artifact> Relics { get; set; } = new List<Artifact>();

    public event Action<int> HealthChanged;
    public event Action<int> GoldChanged;

    public override void _Ready()
    {
        if (Instance is null)
        {
            Instance = this;
            ProcessMode = ProcessModeEnum.Always;
        }

        HP = MaxHP;
        Deck = new();

        PackedScene cardScene = GD.Load<PackedScene>("res://scenes/card.tscn");
        
        foreach (Card card in CardRegistry.Instance.Cards)
        {
            Deck.Add(card.Clone());
            Deck.Add(card.Clone());
        }


        // Deck.Add(CardFactory.CreateCard(damageResource));
        // Deck.Add(CardFactory.CreateCard(damageResource));
        // Deck.Add(CardFactory.CreateCard(healResource));
        // Deck.Add(CardFactory.CreateCard(healResource));
        // Deck.Add(CardFactory.CreateCard(heavyAttackResource));
        // Deck.Add(CardFactory.CreateCard(heavyAttackResource));
        // Deck.Add(CardFactory.CreateCard(berserkAttackResource));
        // Deck.Add(CardFactory.CreateCard(berserkAttackResource));
        // Deck.Add(CardFactory.CreateCard(violetAttack));
        // Deck.Add(CardFactory.CreateCard(poison));
        // Deck.Add(CardFactory.CreateCard(poison));

    }


    public void AddGold(int amount)
    {
        Gold += amount;
        GoldChanged?.Invoke(Gold);
    }

    public void Die()
    {
        GD.Print("GAME OVER");
    }

}
