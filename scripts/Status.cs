using Godot;
/// <summary>
/// Instance of status
/// </summary>
public partial class Status : Control
{
	private int value = 0;
	public int Value
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
			if (valueLabel is not null)
			{
				valueLabel.Text = this.value.ToString();
			}
		}
	}
	private Label valueLabel;
	private Sprite2D statusSprite;
	private StatusResource statusResource;
	public Character ParentCharacter
	{
		get;
		private set;
	}
	public override void _Ready()
	{
		valueLabel = GetNode<Label>("ValueLabel");
		statusSprite = GetNode<Sprite2D>("StatusSprite");
		ParentCharacter = GetNode<Character>("../..");
		valueLabel.Text = value.ToString();
	}
	public void SetResource(StatusResource statusResource)
	{
		this.statusResource = statusResource;
		statusSprite.Texture = statusResource.StatusTexture;
	}
	public void AddValue(int additionalValue)
	{
		Value += additionalValue;
	}
	public void Remove()
	{
		GD.Print($"Removing status {statusResource.GetType()}");
		ParentCharacter.Statuses.Remove(statusResource.GetType());
		QueueFree();
	}
	public void OnTurnEnd(Character[] targets)
	{
		GD.Print($"Status.OnTurnEnd - {statusResource.GetType()}, {value}");
		statusResource.OnTurnEnd(this, targets);
	}
	public void OnDamageReceive(Character receiver, Character dealer)
		{
		GD.Print($"Status.OnDamageReceive - {statusResource.GetType()}, {value}, {receiver}, {dealer}");
				statusResource.OnDamageReceive(this, receiver, dealer);
		}

}
