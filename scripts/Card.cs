using Godot;
using System;

public partial class Card : Control, ICloneable
{
	[Export] public CardResource CardData;

	private bool isHovered;
	private bool isDragging;
	private Vector2 dragOffset;
	private Vector2 originalScale, originalPos, originalSize;
	private Level level;
	private Label costLabel, nameLabel;
	private RichTextLabel descriptionLabel;
	public int Cost
	{
		get;
		private set;
	}
	public override void _Ready()
	{
		level = GetParent().GetParent<Level>();
		costLabel = GetNode<Label>("Panel/Cost/CostLabel");
		descriptionLabel = GetNode<RichTextLabel>("Panel/DescriptionLabel");
		nameLabel = GetNode<Label>("Panel/NameLabel");
		costLabel.Text = Cost.ToString();
		originalScale = Scale;
		originalSize = CustomMinimumSize;


		nameLabel.Text = CardData.Name;
		descriptionLabel.Text = "[outline_size=2]";

		foreach (EffectResource effectResource in CardData.Effects)
		{
			if (effectResource is DamageEffect)
			{
				descriptionLabel.Text += effectResource.Description
				.Replace("{DamageValue}", $"[color=red]{(effectResource as DamageEffect).Damage}[/color]");
				if (!CardData.IsTargeted)
				{
					descriptionLabel.Text += " всем врагам";
				}
			}
			if (effectResource is HealEffect)
			{
				descriptionLabel.Text += effectResource.Description
				.Replace("{HealValue}", $"[color=green]{(effectResource as HealEffect).Value}[/color]");
			}
			if (effectResource is ApplyStatusEffect)
			{
				descriptionLabel.Text += effectResource.Description
				.Replace("{StatusValue}", $"[color=yellow]{(effectResource as ApplyStatusEffect).Value}[/color]")
				.Replace("{StatusType}", $"[color=yellow]{(effectResource as ApplyStatusEffect).StatusToApply.Name}[/color]");
			}
			if (effectResource is SelfDamageEffect)
			{
				descriptionLabel.Text += effectResource.Description
				.Replace("{DamageValue}", $"[color=red]{(effectResource as SelfDamageEffect).Damage}[/color]");
			}
			descriptionLabel.Text += "\n";
		}

		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
	}

	public void Init(CardResource resource)
	{
		CardData = resource;
		Cost = CardData.Cost; //Сделать для других свойств, т.к. CardData - ссылка, улучшения не будут работать
		GetNode<TextureRect>("Panel/Sprite").Texture = CardData.Texture;
	}

	private void OnMouseEntered()
	{
		isHovered = true;
		if (!isDragging)
		{
			Tween tween = CreateTween().SetParallel(true);
			tween.TweenProperty(this, "scale", originalScale * 1.2f, 0.1f); //Анимации забагованы. Возможно есть смысл использовать AnimatuinPlayer
			SizeFlagsStretchRatio = 2f;

			// tween.Parallel().TweenProperty(this, "custom_minimum_size", originalSize * 1.2f, 0.1f);
			ZIndex = 1;
		}
	}

	private void OnMouseExited()
	{
		isHovered = false;
		if (!isDragging)
		{
			Tween tween = CreateTween().SetParallel(true);
			tween.TweenProperty(this, "scale", originalScale, 0.1f); //Анимации забагованы. Возможно есть смысл использовать AnimatuinPlayer
			SizeFlagsStretchRatio = 1f;
			// tween.TweenProperty(this, "custom_minimum_size", originalSize, 0.1f);
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

		if (level.TargetEnemy is not null || (!CardData.IsTargeted && GetGlobalMousePosition().Y < 500)) //заглушка
		{
			Scale = originalScale;
			CustomMinimumSize = originalSize;
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
		Discard();

		foreach (EffectResource effect in CardData.Effects)
		{
			//Выбрасывает исключение NullReferenceException
			effect.Execute(level.Player, CardData.IsTargeted ? [level.TargetEnemy] : level.Enemies.ToArray());
		}
	}

	public void Discard()
	{
		GetParent().RemoveChild(this);
		level.DiscardPile.Add(this);
	}

	public object Clone()
	{
		Card card = CardFactory.CreateCard(CardData);
		return card;
	}

}
