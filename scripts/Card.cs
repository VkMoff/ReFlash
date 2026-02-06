using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class Card : Control
{
	// [Export] public CardResource CardData;

	private bool isHovered;
	private bool isDragging;
	private Vector2 dragOffset;
	private Vector2 originalScale, originalPos, originalSize;
	protected Level level;
	protected Label costLabel, nameLabel;
	protected RichTextLabel descriptionLabel;
	public int Cost	{ get; protected set; }
	public bool IsTargeted { get; protected set; }
	public string CardName { get; protected set; }
	public string Description { get; protected set; }
	public Texture2D CardTexture { get; protected set; }
	public Array<EffectResource> Effects;
	public delegate void Play(Character caster, Character[] targets);
	Play play;
	public override void _Ready()
	{
		level = GetParent().GetParent<Level>();
		costLabel = GetNode<Label>("Panel/Cost/CostLabel");
		descriptionLabel = GetNode<RichTextLabel>("Panel/DescriptionLabel");
		nameLabel = GetNode<Label>("Panel/NameLabel");
		costLabel.Text = Cost.ToString();
		originalScale = Scale;
		originalSize = CustomMinimumSize;


		nameLabel.Text = CardName;
		descriptionLabel.Text = "[outline_size=2]" + Description;
		GetNode<TextureRect>("Panel/Sprite").Texture = CardTexture;

		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
	}

	public Card Init(string cardName, Texture2D texture, Play onplay, string description = "what?", int cost = 0, bool isTargeted = true)
	{
		CardName = cardName;
		CardTexture = texture;
		play += onplay;
		Description = description;
		Cost = cost;
		IsTargeted = isTargeted;

		return this;
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

		if (level.TargetEnemy is not null || (!IsTargeted && GetGlobalMousePosition().Y < 500)) //заглушка
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
		play(level.Player, IsTargeted ? [level.TargetEnemy] : level.Enemies.ToArray());
	}

	public void Discard()
	{
		GetParent().RemoveChild(this);
		level.DiscardPile.Add(this);
	}

	public Card Clone()
	{
		Card clone = GD.Load<PackedScene>(SceneFilePath).Instantiate<Card>().Init(
			CardName,
			CardTexture,
			play,
			Description,
			Cost,
			IsTargeted
		);
		return clone;
	}

}
