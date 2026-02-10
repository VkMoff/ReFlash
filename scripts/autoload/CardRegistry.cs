using System;
using Godot;
using Godot.Collections;

public partial class CardRegistry : Node
{
    public static CardRegistry Instance;
    public Array<Card> Cards { get; private set; }
    public override void _Ready()
    {
        if (Instance is null)
        {
            Instance = this;
            ProcessMode = ProcessModeEnum.Always;
        }

        PackedScene cardScene = GD.Load<PackedScene>("res://scenes/card.tscn");
        
        Cards =
        [
            cardScene.Instantiate<Card>().Init(
            cardName: "Удар",
            texture: GD.Load<Texture2D>("res://resources/sprites/icon.svg"),
            onplay: (caster, targets) => {
                targets[0].ChangeHP(-5);
            },
            description: "Наносит 5 урона",
            cost: 1,
            isTargeted: true
            ),
            
            cardScene.Instantiate<Card>().Init(
            cardName: "Тяжёлый удар",
            texture: GD.Load<Texture2D>("res://resources/sprites/icon_damage.svg"),
            onplay: (caster, targets) => {
                targets[0].ChangeHP(-20);
            },
            description: "Наносит 20 урона",
            cost: 2,
            isTargeted: true
            ),

            cardScene.Instantiate<Card>().Init(
            cardName: "Восстановление",
            texture: GD.Load<Texture2D>("res://resources/sprites/icon_heal.svg"),
            onplay: (caster, targets) => {
                caster.ChangeHP(5);
            },
            description: "Восстанавливает 5 ОЗ",
            cost: 1,
            isTargeted: false
            ),

            cardScene.Instantiate<Card>().Init(
            cardName: "Яд",
            texture: GD.Load<Texture2D>("res://resources/sprites/icon_poison.svg"),
            onplay: (caster, targets) => {
                targets[0].AddStatus(new PoisonStatus(), 5);
            },
            description: "Накладывает на цель 5 яда",
            cost: 1,
            isTargeted: true
            ),

            cardScene.Instantiate<Card>().Init(
            cardName: "Удар берсерка",
            texture: GD.Load<Texture2D>("res://resources/sprites/icon_berserk.svg"),
            onplay: (caster, targets) => {
                foreach (Character target in targets)
                {
                    target.ChangeHP(-20);
                }
                caster.ChangeHP(-5);
            },
            description: "Наносит 20 урона всем врагам, отнимает 5 ОЗ",
            cost: 2,
            isTargeted: false
            ),

            cardScene.Instantiate<Card>().Init(
            cardName: "Жестокий удар",
            texture: GD.Load<Texture2D>("res://resources/sprites/icon_viol.svg"),
            onplay: (caster, targets) => {
                targets[0].ChangeHP(-50);
            },
            description: "Наносит 50 урона",
            cost: 3,
            isTargeted: true
            )
        ];
    }
}