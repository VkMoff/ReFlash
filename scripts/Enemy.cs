using Godot;
using System;
using System.Linq;
using System.Threading.Tasks;

public partial class Enemy : Character
{
	int currentActionIndex = 0;
	EffectResource[] nextActions;
	EffectResource[][][] actionPatterns;
	Sprite2D nextActionSprite;
	Label attackDamageLabel;
	Texture2D attackTexture, healTexture;
	public override void _Ready()
	{
		level = GetParent().GetParent<Level>();
		nextActionSprite = GetNode<Sprite2D>("NextActionSprite");
		attackTexture = GD.Load<Texture2D>("res://resources/sprites/enemy_actions/action_attack.svg");
		healTexture = GD.Load<Texture2D>("res://resources/sprites/enemy_actions/action_heal.svg");
		attackDamageLabel = GetNode<Label>("NextActionSprite/Label");
		base._Ready();

		actionPatterns = //ПРОКЛЯТО
		[
			[
				[new DamageEffect(10)]//дамаг
			],
			[
				[new DamageEffect(5)], [new HealEffect(3)]//или дамаг, или хил
			],
			[
				[new MultiDamageEffect(1,10)]
			]

		];

		nextActions = GetActions(currentActionIndex);
		SetDamageLabel();

		// MouseEntered += MouseEnter;
		// MouseExited += MouseExit;
	}

	public void Init(EnemyResource enemyResource)
	{
		base.Init(enemyResource.MaxHP, enemyResource.Animation);
		actionPatterns = enemyResource.ActionPatterns;
		GetNode<Label>("NameLabel").Text = enemyResource.Name;
	}

	public void MouseEnter()
	{
		level.TargetEnemyChanged(this);
	}
	public void MouseExit()
	{
		level.TargetEnemyChanged(null);
	}

	public async Task ExecuteNextAction()
	{
		//Выполнение действий
		foreach (EffectResource action in nextActions)
		{
			GD.Print(action.GetType().Name);
			await action.Execute(this, [level.Player]);
		}
		//Определение индекса след. действий
		if (++currentActionIndex == actionPatterns.Length)
		{
			currentActionIndex = 0;
		}

		nextActions = GetActions(currentActionIndex);

		SetDamageLabel();

	}

	private EffectResource[] GetActions(int index)
	{
		return actionPatterns[index][GD.Randi() % actionPatterns[index].Length];
	}

	private void SetDamageLabel()
	{
		//Перенести спрайт в класс ResourceEffect
		if (nextActions[0] is DamageEffect)
		{
			attackDamageLabel.Text = (nextActions[0] as DamageEffect).Damage.ToString();
			nextActionSprite.Texture = attackTexture;
			attackDamageLabel.Visible = true;
		}
		else if (nextActions[0] is MultiDamageEffect)
		{
			attackDamageLabel.Text = $"{(nextActions[0] as MultiDamageEffect).Damage} x {(nextActions[0] as MultiDamageEffect).Count}";
			nextActionSprite.Texture = attackTexture;
			attackDamageLabel.Visible = true;

		}
		else if (nextActions[0] is HealEffect)
		{
			nextActionSprite.Texture = healTexture;
			attackDamageLabel.Visible = false;
		}
	}
}
