using Godot;
public class EnemyFactory
{
    static PackedScene enemyScene = GD.Load<PackedScene>("res://scenes/enemy.tscn");

    public static Enemy CreateEnemy(EnemyResource enemyResource)
    {
        Enemy enemy = enemyScene.Instantiate<Enemy>();
        enemy.Init(enemyResource);
        return enemy;
    }
}