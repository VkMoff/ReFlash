using Godot;
using Microsoft.VisualBasic;

public partial class HealthBar : PanelContainer
{
	private int maxValue, value;
	public int MaxValue
	{
		get 
		{
			return maxValue;
		}
		set
		{
			maxValue = value;
		}
	}
	public int Value
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
			bar.CustomMinimumSize = new((float)this.value / maxValue * Size.X, 0);
			label.Text = $"{this.value}/{maxValue}";
		}
	}
	public int BeautifulValue
	{
		set
		{
			this.value = value;
			bar.CreateTween().TweenProperty(bar, "custom_minimum_size", new Vector2((float)this.value / maxValue * Size.X, 0), 0.2f);
			label.Text = $"{this.value}/{maxValue}";

		}
	}
	private ColorRect bar;
	private Label label;
	public override void _Ready()
	{
		bar = GetNode<ColorRect>("Bar");
		bar.Color = new("#ff0000");
		label = GetNode<Label>("Label");
		Resized += OnResized;

	}

	private void OnResized()
	{
		if (maxValue > 0 && Size.X > 0)
		{
			Value = value;
			Resized -= OnResized; // только один раз
		}
	}

}
