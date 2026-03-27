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
    public List<Artifact> Artifacts { get; set; } = new List<Artifact>();

    public event Action<int> HealthChanged;
    public event Action<int> GoldChanged;

    PackedScene artifactScene;

    public override void _Ready()
    {
        if (Instance is null)
        {
            Instance = this;
            ProcessMode = ProcessModeEnum.Always;
        }

        Reset();

        artifactScene = GD.Load<PackedScene>("res://scenes/artifact.tscn");

        InstantiateArtifact(new StrangeMask());
        // InstantiateArtifact(new PoisonSpray());
    }


    public void AddGold(int amount)
    {
        Gold += amount;
        GoldChanged?.Invoke(Gold);
    }

    public void InstantiateArtifact(ArtifactResource artifactResource)
    {
        Artifact artifact = artifactScene.Instantiate<Artifact>();;
        artifact.SetArtifact(artifactResource);
        Artifacts.Add(artifact);
    }

    public void Die()
    {
        GD.Print("GAME OVER");
        Reset();
    }

    public void Reset()
    {
        HP = MaxHP;
        Deck = new();
        Gold = 0;
        var cardRegistry = CardRegistry.Instance;

        Deck.Add(cardRegistry["strike"]);
        Deck.Add(cardRegistry["heavy_strike"]);
        Deck.Add(cardRegistry["heal"]);
        Deck.Add(cardRegistry["poison"]);
        Deck.Add(cardRegistry["berserk_strike"]);
        Deck.Add(cardRegistry["violent_strike"]);
        Deck.Add(cardRegistry["double_strike"]);
        Deck.Add(cardRegistry["random_damage"]);
        Deck.Add(cardRegistry["amplify"]);
    }

}
