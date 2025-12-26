using Godot;

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
			GD.Print(value);
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
		this.statusResource.SetStatusInstance(this);
	}
	public void AddValue(int additionalValue)
	{
		Value += additionalValue;
	}
	public void OnTurnEnd()
	{
		statusResource.OnTurnEnd();
	}
}
