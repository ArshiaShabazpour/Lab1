using UnityEngine;
using System;
public class GameManager : Singleton<GameManager>
{
    public int targetScore = 20; 
    public float loseY = -10f;   

    public FruitSpawner fruitSpawner;

    public EnemySpawner enemySpawner; 
    public int startingEnemyCount = 3;


    public event Action OnWin;
    public event Action OnLose;

    public int Score { get; private set; }
    public bool IsGameOver { get; private set; }

    void Start()
    {
        Score = 0;
        IsGameOver = false;

        UIManager.Instance?.UpdateScore(Score);

        fruitSpawner?.SpawnRandomFruits(10, new Vector2(-65, 1), new Vector2(65, 3));
        enemySpawner?.SpawnEnemies(startingEnemyCount);
    }

    void Update()
    {
        if (!IsGameOver)
        {
            var player = UnityEngine.GameObject.FindGameObjectWithTag("Player");
            if (player != null && player.transform.position.y < loseY)
            {
                Lose();
            }
        }
    }



    public void AddScore(int amount)
    {
        if (IsGameOver) return;
        Score += amount;

        UIManager.Instance.UpdateScore(Score);
        
        if (Score >= targetScore)
        {
            Win();
        }
    }

    public void Win()
    {
        if (IsGameOver) return;
        IsGameOver = true;

        UIManager.Instance.ShowWin();
        OnWin?.Invoke();
        Debug.Log("You win!");
    }

    public void Lose()
    {
        if (IsGameOver) return;
        IsGameOver = true;

            UIManager.Instance.ShowLose();

        OnLose?.Invoke();
        Debug.Log("You lose!");
    }
}
