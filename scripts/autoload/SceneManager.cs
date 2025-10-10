using Godot;
using Godot.Collections;
using System;

public partial class SceneManager : Node
{
    public static SceneManager Instance
    {
        get;
        private set;
    }

    public override void _Ready()
    {
        if (Instance is null)
        {
            Instance = this;
            ProcessMode = ProcessModeEnum.Always;
        }
    }
    
}
