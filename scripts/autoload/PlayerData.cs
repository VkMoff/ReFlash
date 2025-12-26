using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerData : Node
{
    public static PlayerData Instance
    {
        get;
        private set;
    }

    public List<Card> Deck
    {
        get;
        private set;
    }
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
    public List<string> Relics { get; set; } = new List<string>(); //ЗАГЛУШКА

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

        CardResource damageResource = GD.Load<CardResource>("res://resources/cards/Attack.tres"); //Вынести загрузку в синглтон? Мб словарь карт?
        CardResource healResource = GD.Load<CardResource>("res://resources/cards/Heal.tres");
        CardResource heavyAttackResource = GD.Load<CardResource>("res://resources/cards/HeavyAttack.tres");
        CardResource berserkAttackResource = GD.Load<CardResource>("res://resources/cards/BerserkAttack.tres");
        CardResource violetAttack = GD.Load<CardResource>("res://resources/cards/Violet.tres");
        CardResource poison = GD.Load<CardResource>("res://resources/cards/Poison.tres");

        CardFactory cardFactory = new();

        Deck.Add(CardFactory.CreateCard(damageResource));
        Deck.Add(CardFactory.CreateCard(damageResource));
        Deck.Add(CardFactory.CreateCard(healResource));
        Deck.Add(CardFactory.CreateCard(healResource));
        Deck.Add(CardFactory.CreateCard(heavyAttackResource));
        Deck.Add(CardFactory.CreateCard(heavyAttackResource));
        Deck.Add(CardFactory.CreateCard(berserkAttackResource));
        Deck.Add(CardFactory.CreateCard(berserkAttackResource));
        Deck.Add(CardFactory.CreateCard(violetAttack));
        Deck.Add(CardFactory.CreateCard(poison));
        Deck.Add(CardFactory.CreateCard(poison));

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
