using Godot;
using System;
using System.Linq;

public partial class Enemy : Character
{
	int currentAction = 0;
	EffectResource[][][] actionPatterns;
	public override void _Ready()
	{
		base._Ready();

		actionPatterns = //ПРОКЛЯТО
		[
			[
				[new DamageEffect(10)]
			],
			[
				[new DamageEffect(5)], [new HealEffect(3)]
			],
			[
				[new DamageEffect(5), new DamageEffect(5)]
			]

		];
	}
	
	public void Init(EnemyResource enemyResource)
	{
		base.Init(enemyResource.MaxHP, enemyResource.Animation);
		actionPatterns = enemyResource.ActionPatterns;
		
	}

	public void MouseEnter()
	{
		level.TargetEnemyChanged(this);
	}

	public void NextAttack()
	{	//Переписать! Определять действие следующего хода на текущем ходу!
		foreach (EffectResource action in actionPatterns[currentAction][GD.Randi() % actionPatterns[currentAction].Length])
		{
			GD.Print(action.GetType().Name);
			action.Execute(this, [level.Player]);
		}

		if (++currentAction == actionPatterns.Length)
		{
			currentAction = 0;
		}
	}
}
