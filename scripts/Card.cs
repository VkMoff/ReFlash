using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;

[GlobalClass]
public partial class Card : Control
{
	[Export] public CardResource CardData;

	private bool isHovered;
	private bool isDragging;
	private Vector2 dragOffset;
	private Vector2 originalScale, originalPos, originalSize;
	protected Level level;
	protected Label costLabel, nameLabel;
	protected RichTextLabel descriptionLabel;
	public int Cost { get; protected set; }
	public bool IsTargeted { get; protected set; }
	public string CardName { get; protected set; }
	public string Description { get; protected set; }
	public Texture2D CardTexture { get; protected set; }
	public Array<EffectResource> Effects;
	public event Action<Card> Clicked;
	public override void _Ready()
	{
		try
		{
			level = GetParent().GetParent<Level>();
		}
		catch (Exception ex)
		{
			GD.Print($"Ошибка при загрузке карты: {ex.Message}\nВозможно карта находится не в сцене Level");
		}
		costLabel = GetNode<Label>("Cost/CostLabel");
		descriptionLabel = GetNode<RichTextLabel>("DescriptionLabel");
		nameLabel = GetNode<Label>("NameLabel");
		costLabel.Text = Cost.ToString();
		originalScale = Scale;
		originalSize = CustomMinimumSize;


		nameLabel.Text = CardData.Name;
		UpdateDescription();
		descriptionLabel.Text = $"[outline_size=2]{Description}";
		GetNode<TextureRect>("Sprite").Texture = CardTexture;

	}

	public Card Init(CardResource cardResource)
	{
		CardData = cardResource;
		CardName = cardResource.Name;
		CardTexture = cardResource.Texture;
		Effects = cardResource.Effects;
		IsTargeted = cardResource.IsTargeted;
		Cost = cardResource.Cost;

		return this;
	}
	public void UpdateDescription()
	{
		Description = "";
		foreach (EffectResource effectResource in CardData.Effects)
		{
			Description += effectResource.GetDescription();
			if ((effectResource is DamageEffect) && (!CardData.IsTargeted))
			{
				Description += " всем врагам";
			}
			Description += "\n";
		}
		if (descriptionLabel is not null)
		{
			descriptionLabel.Text = $"[outline_size=2]{Description}";
		}
	}

	private void OnMouseEntered()
	{
		if (level is null) return;
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
		if (level is null) return;
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
		if (level is null)
		{
			if (@event is InputEventMouseButton mb && mb.ButtonIndex == MouseButton.Left) //Выглядит отвратительно
			{
				Clicked?.Invoke(this);
			}	
			return;
		}
		;
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

	private async Task EndDrag()
	{
		isDragging = false;
		ZIndex = 0; // Возвращаем исходный ZIndex

		if (level.TargetEnemy is not null || (!IsTargeted && GetGlobalMousePosition().Y < 500)) //заглушка
		{
			Scale = originalScale;
			CustomMinimumSize = originalSize;
			await PlayCard();
		}
		else
		{
			GetParent<HBoxContainer>().QueueSort();
		}
		level.TargetEnemy = null;
	}

	public async Task PlayCard()
	{
		if (!level.TryPlay(Cost))
		{
			GetParent<HBoxContainer>().QueueSort();
			return;
		}

		Character[] targetArray;
		if (CardData.IsTargeted) targetArray = [level.TargetEnemy];
		else targetArray = level.Enemies.ToArray();
		
		Discard();
		foreach (EffectResource effect in CardData.Effects)
		{
			Character[] target;
			if (effect.AppliableToCaster) target = [level.Player];
			else target = targetArray;

			await effect.Execute(level.Player, target);
			await ToSignal(PlayerData.Instance.GetTree().CreateTimer(0.5), SceneTreeTimer.SignalName.Timeout);
		}
	}

	public void Discard()
	{
		GetParent().RemoveChild(this);
		level.DiscardPile.Add(this);
	}

	public Card Clone()
	{
		Card clone = GD.Load<PackedScene>(SceneFilePath).Instantiate<Card>().Init(CardData);
		return clone;
	}

}
