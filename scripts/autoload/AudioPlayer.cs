using Godot;
using System;

public partial class AudioPlayer : Node
{
    public static AudioPlayer Instance;
    
    AudioStreamPlayer cardShufflePlayer = new();
    AudioStreamPlayer strikePlayer = new();

    public override void _Ready()
    {
        Instance = this;
		ProcessMode = ProcessModeEnum.Always;

        AddChild(cardShufflePlayer);
        AudioStreamRandomizer cardShuffleRandomizer = new();
        cardShuffleRandomizer.AddStream(-1, GD.Load<AudioStream>("res://resources/audio/cards/card_draw_1.wav"));
        cardShuffleRandomizer.AddStream(-1, GD.Load<AudioStream>("res://resources/audio/cards/card_draw_2.wav"));
        cardShuffleRandomizer.AddStream(-1, GD.Load<AudioStream>("res://resources/audio/cards/card_draw_3.wav"));
        cardShuffleRandomizer.RandomPitch = 2;
        cardShuffleRandomizer.RandomVolumeOffsetDb = 3;
        cardShufflePlayer.Stream = cardShuffleRandomizer;

        AddChild(strikePlayer);
        AudioStreamRandomizer strikeRandomizer = new();
        strikeRandomizer.AddStream(-1, GD.Load<AudioStream>("res://resources/audio/fight/punch.wav"));
        // strikeRandomizer.AddStream(-1, GD.Load<AudioStream>("res://resources/audio/fight/swipe.wav"));
        strikeRandomizer.RandomPitch = 2;
        strikeRandomizer.RandomVolumeOffsetDb = 3;
        strikePlayer.Stream = strikeRandomizer;
        // AudioStream strikeStream = GD.Load<AudioStream>("res://resources/audio/fight/punch.wav");
        // strikePlayer.Stream = strikeStream;
    }
    public void PlayCardShuffle()
    {
        cardShufflePlayer.Play();
    }
    public void PlayStrike()
    {
        strikePlayer.Play();
    }
}
