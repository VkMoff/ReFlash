using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;

public partial class Enemy : Character
{
	int currentActionIndex = 0;
	Array<EffectResource> nextActions = new();
	Array<EffectResource> initialActions = new(); //Array или []?
	Array<ActionResource> actionPatterns = new();
	Sprite2D nextActionSprite;
	Label attackDamageLabel;
	Texture2D attackTexture, healTexture, positiveEffectTexture;
	private bool firstTurnPlayed = false;
	public override void _Ready()
	{
		Level = GetParent().GetParent<Level>();
		nextActionSprite = GetNode<Sprite2D>("NextActionSprite");
		attackTexture = GD.Load<Texture2D>("res://resources/sprites/enemy_actions/action_attack.svg");
		healTexture = GD.Load<Texture2D>("res://resources/sprites/enemy_actions/action_heal.svg");
		positiveEffectTexture = GD.Load<Texture2D>("res://resources/sprites/enemy_actions/action_amplify.svg");
		attackDamageLabel = GetNode<Label>("NextActionSprite/Label");
		base._Ready();

		// actionPatterns = //ПРОКЛЯТО
		// [
		// 	[
		// 		[new DamageEffect(10) {Animation = GD.Load<SpriteFrames>("res://resources/animations/anim_slash_green.tres")}]//дамаг
		// 	],
		// 	[
		// 		[new DamageEffect(5) {Animation = GD.Load<SpriteFrames>("res://resources/animations/anim_slash_green.tres")}], [new HealEffect(3){Animation = GD.Load<SpriteFrames>("res://resources/animations/anim_heal.tres")}]//или дамаг, или хил
		// 	],
		// 	[
		// 		[new MultiDamageEffect(1,10) {Animation = GD.Load<SpriteFrames>("res://resources/animations/anim_slash_green.tres")}]
		// 	]

		// ];

		nextActions = initialActions;
		SetDamageLabel();

		// MouseEntered += MouseEnter;
		// MouseExited += MouseExit;
	}

	public void Init(EnemyResource enemyResource)
	{
		base.Init(enemyResource.MaxHP, enemyResource.Animation);
		actionPatterns = enemyResource.ActionPatterns;
		initialActions = enemyResource.InitialActions;
		GD.Print(initialActions.Count);
		GetNode<Label>("NameLabel").Text = enemyResource.Name;
	}

	public void MouseEnter()
	{
		Level.TargetEnemyChanged(this);
	}
	public void MouseExit()
	{
		Level.TargetEnemyChanged(null);
	}

	public async Task ExecuteNextAction()
	{
		if (!firstTurnPlayed)
		{
			foreach (EffectResource action in initialActions)
			{
				await action.Execute(this, action.AppliableToCaster ? [this] : [Level.Player]); //Перенести проверку наложения эффекта на себя в Effect.Execute?
			}
			firstTurnPlayed = true;
			nextActions = GetActions(currentActionIndex);
			return;
		}
		//Выполнение действий
		foreach (EffectResource action in nextActions)
		{
			GD.Print(action.GetType().Name);
			await action.Execute(this, action.AppliableToCaster ? [this] : [Level.Player]);
		}
		if (!IsAlive) return;
		//Определение индекса след. действий
		if (++currentActionIndex == actionPatterns.Count)
		{
			currentActionIndex = 0;
		}

		nextActions = GetActions(currentActionIndex);

		SetDamageLabel();

	}

	private Array<EffectResource> GetActions(int index)
	{
		return actionPatterns[index].Effects;
	}

	private void SetDamageLabel()
	{
		//Перенести спрайт в класс ResourceEffect
		if (nextActions[0] is DamageEffect)
		{
			attackDamageLabel.Text = $"{(nextActions[0] as DamageEffect).Damage + (nextActions[0] as DamageEffect).StrengthModifier}";
			nextActionSprite.Texture = attackTexture;
			attackDamageLabel.Visible = true;
		}
		else if (nextActions[0] is MultiDamageEffect)
		{
			attackDamageLabel.Text = $"{(nextActions[0] as MultiDamageEffect).Damage + (nextActions[0] as MultiDamageEffect).StrengthModifier} x {(nextActions[0] as MultiDamageEffect).Count}";
			nextActionSprite.Texture = attackTexture;
			attackDamageLabel.Visible = true;

		}
		else if (nextActions[0] is HealEffect)
		{
			nextActionSprite.Texture = healTexture;
			attackDamageLabel.Visible = false;
		}
		else if ((nextActions[0] is ApplyStatusEffect) && nextActions[0].AppliableToCaster)
		{
			nextActionSprite.Texture = positiveEffectTexture;
			attackDamageLabel.Visible = false;
		}
	}

	public override void RecalculateStrength()
	{
		if (!Statuses.ContainsKey(typeof(StrengthStatus))) return;
		foreach (ActionResource action in actionPatterns)
		{		
			foreach (EffectResource effect in action.Effects)
			{
				if (effect is DamageEffect)
				{
					((DamageEffect)effect).StrengthModifier = Statuses[typeof(StrengthStatus)].Value;
				}
				if (effect is MultiDamageEffect)
				{
					((MultiDamageEffect)effect).StrengthModifier = Statuses[typeof(StrengthStatus)].Value;
				}
			}
		}
		SetDamageLabel();
	}
}
