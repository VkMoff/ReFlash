using Godot;
using System;

public static class Message
{
    public static void ShowMessage(this Node parent, string text, float duration = 1.5f)
    {
        Viewport viewport = parent.GetViewport();
        Vector2 mousePos = viewport.GetMousePosition();
        
        ShowMessage(viewport, text, mousePos, duration);
    }
    public static void ShowMessage(this Node parent, string text, Vector2 globalPosition, float duration = 1.5f)
    {
        Label label = new Label();
        label.Text = text;
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        
        label.AddThemeColorOverride("font_shadow_color", Colors.Black);
        label.AddThemeConstantOverride("shadow_offset_x", 1);
        label.AddThemeConstantOverride("shadow_offset_y", 1);
        
        CanvasLayer layer = new CanvasLayer();
        layer.Layer = 100; 
        parent.AddChild(layer);
        layer.AddChild(label);
        label.GlobalPosition = globalPosition - label.Size / 2;

        Tween tween = label.CreateTween();
        tween.TweenProperty(label, "position", label.Position + Vector2.Up * 50, duration);
        tween.Parallel().TweenProperty(label, "modulate:a", 0.0, duration);
        tween.Finished += label.QueueFree;
    }
}

