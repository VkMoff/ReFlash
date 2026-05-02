using Godot;
using System;

public partial class SettingsManager : Node
{
    public static SettingsManager Instance;
    
    public Godot.Collections.Dictionary Settings { get; private set; }
    ConfigFile config = new ConfigFile();
    
    public override void _Ready()
    {
        Instance = this;
        ProcessMode = ProcessModeEnum.Always;


        Error err = config.Load("user://settings.cfg");

        if (err == Error.FileNotFound)
        {
            config = new ConfigFile();
            config.Save("user://settings.cfg");
        }
        else if (err != Error.Ok)
        {
            GD.Print(err);
            return;
        }
    }
    
    public void SetSetting(string setting, string value)
    {
        config.SetValue("settings", setting, value);
    }
    public Variant GetSetting(string setting)
    {
        return config.GetValue("settings", setting);
    }
    public void Save()
    {
        config.Save("user://settings.cfg");
    }
}
