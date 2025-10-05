using Godot;
using System;

public partial class Card : Control
{
	[Export] public CardResource CardData;

	private bool isHovered;
	private bool isDragging;
	private Vector2 dragOffset;
	private Vector2 originalScale, originalPos;
	private Level level;
	private Label costLabel;
	public int Cost
	{
		get;
		private set;
	}
	public override void _Ready()
	{
		level = GetParent().GetParent<Level>();
		costLabel = GetNode<Label>("CostLabel");
		costLabel.Text = Cost.ToString();
		originalScale = Scale;

		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
	}

	public void Init(CardResource resource)
	{
		CardData = resource;
		Cost = CardData.Cost; //Сделать для других свойств, т.к. CardData - ссылка, улучшения не будут работать
		GetNode<TextureRect>("Sprite").Texture = CardData.Texture;
	}

	private void OnMouseEntered()
	{
		isHovered = true;
		if (!isDragging)
		{
			CreateTween().TweenProperty(this, "scale", originalScale * 1.2f, 0.1f); //Анимации забагованы. Возможно есть смысл использовать AnimatuinPlayer
			CreateTween().TweenProperty(this, "custom_minimum_size", new Vector2(180, 100), 0.1f);
			ZIndex = 1;
		}
	}

	private void OnMouseExited()
	{
		isHovered = false;
		if (!isDragging)
		{
			CreateTween().TweenProperty(this, "scale", originalScale, 0.1f); //Анимации забагованы. Возможно есть смысл использовать AnimatuinPlayer
			CreateTween().TweenProperty(this, "custom_minimum_size", Vector2.Zero, 0.1f);
			ZIndex = 0;
		}
	}

	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.ButtonIndex == MouseButton.Left)
		{
			if (mouseButtonEvent.Pressed)
			{
				StartDrag();
			}
			else
			{
				EndDrag();
			}
		}
		if (@event is InputEventMouseMotion mouseMotionEvent && isDragging)
		{
			GlobalPosition = GetGlobalMousePosition() - dragOffset;
		}
	}

	private void StartDrag()
	{
		isDragging = true;
		dragOffset = GetGlobalMousePosition() - GlobalPosition;

		ZIndex = 100;
		CreateTween().TweenProperty(this, "scale", originalScale * 1.2f, 0.1f);


	}

	private void EndDrag()
	{
		isDragging = false;
		ZIndex = 0; // Возвращаем исходный ZIndex

		if (level.TargetEnemy is not null || (!CardData.IsTargeted && GetGlobalMousePosition().Y < 500))
		{
			PlayCard();
		}
		else
		{
			GetParent<HBoxContainer>().QueueSort();
		}
		level.TargetEnemy = null;
	}

	public void PlayCard()
	{
		if (!level.TryPlay(Cost))
		{
			GetParent<HBoxContainer>().QueueSort();
			return;
		}
		level.Discard(this);

		foreach (EffectResource effect in CardData.Effects)
		{
			effect.Execute(level.Player, CardData.IsTargeted ? [level.TargetEnemy] : level.Enemies.ToArray());
		}
	}
}
