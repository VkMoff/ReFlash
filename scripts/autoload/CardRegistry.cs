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
            "Удар",
            GD.Load<Texture2D>("res://resources/sprites/icon.svg"),
            (caster, targets) => {
                foreach (Character target in targets)
                {
                    target.ChangeHP(-5);
                }
            },
            "Наносит 5 урона",
            1,
            true
            ),
            
            cardScene.Instantiate<Card>().Init(
            "Тяжёлый удар",
            GD.Load<Texture2D>("res://resources/sprites/icon_damage.svg"),
            (caster, targets) => {
                foreach (Character target in targets)
                {
                    target.ChangeHP(-20);
                }
            },
            "Наносит 20 урона",
            2,
            true
            ),

            cardScene.Instantiate<Card>().Init(
            "Восстановление",
            GD.Load<Texture2D>("res://resources/sprites/icon_heal.svg"),
            (caster, targets) => {
                caster.ChangeHP(5);
            },
            "Восстанавливает 5 ОЗ",
            1,
            false
            )
        ];
    }
}