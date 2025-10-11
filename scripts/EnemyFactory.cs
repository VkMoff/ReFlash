using Godot;
public class EnemyFactory
{
    PackedScene enemyScene = GD.Load<PackedScene>("res://scenes/enemy.tscn");

    public Enemy CreateEnemy(EnemyResource enemyResource)
    {
        Enemy enemy = enemyScene.Instantiate<Enemy>();
        enemy.Init(enemyResource);
        return enemy;
    }
}