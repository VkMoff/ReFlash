using Godot;
using System;
using Hertzole.GameJolt;

public partial class FailMenu : CanvasLayer
{
	LineEdit name;
	int score = 0;
	ConfigFile config = new ConfigFile();
	bool postedToBoard = false;

	public override void _Ready()
	{
		Error err = config.Load("res://secrets.cfg");

		if (err == Error.FileNotFound)
		{
			GD.Print("Secrets not found");
		}
		else if (err != Error.Ok)
		{
			GD.Print(err);
			return;
		}


		name = GetNode<LineEdit>("%Name");
		score = PlayerData.Instance.Score;
		int gameId = config.GetValue("secrets", "game_id").AsInt32();
		string privateKey = config.GetValue("secrets", "api_key").AsString();
		GameJoltAPI.Initialize(gameId, privateKey);

		GetNode<Label>("%ResultMessageLabel").Text += $"\nВы набрали {score} очков";
		

	}
	public void GoToMenu()
	{
		SceneManager.Instance.GoToMenu();
		QueueFree();
	}

	public async void Submit()
	{
		if (score <= 0)
		{
			Message.ShowMessage(this, "Вы ничего не сделали");
			return;
		}
		if (name.Text == "")
		{
			Message.ShowMessage(this, "Заполните имя");
			return;
		}
		foreach (char c in name.Text)
		{
			if (!Char.IsAscii(c))
			{
				Message.ShowMessage(this, "Пока что для никнейма доступна только латиница и цифры");
				return;
			}
		}
		if (postedToBoard)
		{
			Message.ShowMessage(this, "Похоже, пустошь уже помнит Вас");
			return;
		}
		postedToBoard = true;
		GameJoltResult result = await GameJoltAPI.Scores.SubmitScoreAsGuestAsync(config.GetValue("secrets", "table_id").AsInt32(), name.Text, score, score.ToString());
		GD.Print(result.Exception); 
		GameJoltAPI.Shutdown();

	}
}
