using Godot;
using System;

public partial class Card : GridContainer
{
	[Export] public CardResource CardData;

	private bool isHovered;
	private bool isDragging;
	private Vector2 dragOffset;
	private Vector2 originalScale, originalPos;
	private Level level;

	public override void _Ready()
	{
		level = GetParent().GetParent<Level>();
		originalScale = Scale;

		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
	}

	public void Init(CardResource resource)
	{
		CardData = resource;
		GetNode<TextureRect>("Sprite").Texture = CardData.Texture;
	}

	private void OnMouseEntered()
	{
		isHovered = true;
		if (!isDragging)
		{
			// Воспроизводим анимацию увеличения при наведении
			CreateTween().TweenProperty(this, "scale", originalScale * 1.2f, 0.1f);
			Size = new(300, 300);
		}
	}

	private void OnMouseExited()
	{
		isHovered = false;
		if (!isDragging)
		{
			// Возвращаем исходный размер, если карту не перетаскивают
			CreateTween().TweenProperty(this, "scale", originalScale, 0.1f);
		}
	}

	public override void _GuiInput(InputEvent @event)
	{
		// Обрабатываем нажатие кнопки мыши
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

		if (GetGlobalMousePosition().Y < 500)
		{
			PlayCard();
		}
		else
		{
			GetParent<HBoxContainer>().QueueSort();
		}
	}

	public void PlayCard()
	{
		level.Hand.RemoveChild(this);
		level.Discard(this);

		foreach (EffectResource effect in CardData.Effects)
		{
			effect.Execute(level.Player, [level.Player]);
		}

		
	}

}
